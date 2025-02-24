import { Component, inject, OnInit } from '@angular/core';
import { collapse, DateAdapter, fadeIn } from '@abp/ng.theme.shared';
import { transition, trigger, useAnimation } from '@angular/animations';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { EXTENSIONS_IDENTIFIER } from '@abp/ng.components/extensible';
import { NgbCalendar, NgbDate, NgbDateAdapter, NgbDateParserFormatter } from '@ng-bootstrap/ng-bootstrap';
import { AuditLogDto, AuditLogsService, GetAuditLogsDto } from '@feramor/ng.abp-audit-logging-elastic-search/proxy';
import { HTTP_METHODS, HTTP_STATUS_CODES } from '../../constants';
import { take } from 'rxjs';
import { eElasticSearchComponents } from '@feramor/ng.abp-audit-logging-elastic-search/common';

@Component({
  selector: 'audit-logs',
  templateUrl: './audit-logs.component.html',
  styleUrl: './audit-logs.component.css',
  animations: [collapse, trigger('fadeIn', [transition('* <=> *', useAnimation(fadeIn))])],
  providers: [
    ListService,
    {
      provide: EXTENSIONS_IDENTIFIER,
      useValue: eElasticSearchComponents.AuditLogs,
    },
    { provide: NgbDateAdapter, useClass: DateAdapter },
  ],
})
export class AuditLogsComponent implements OnInit {
  protected readonly service = inject(AuditLogsService);
  protected readonly list = inject(ListService<GetAuditLogsDto>);
  formatter = inject(NgbDateParserFormatter);
  calendar = inject(NgbCalendar);

  data: PagedResultDto<AuditLogDto> = { items: [], totalCount: 0 };
  selected = {} as AuditLogDto;

  pageQuery = {
    maxResultCount: 10,
    skipCount: 0,
    httpMethod: null,
    httpStatusCode: null,
    hasException: null,
  } as GetAuditLogsDto;

  httpMethods = HTTP_METHODS;
  httpStatusCodes = HTTP_STATUS_CODES;

  modalVisible = false;
  collapseActionStates = [true];
  collapseChangeStates = [true];
  sortOrder = '';
  sortKey = '';
  selectedTab = 'audit-logs';

  protected hookToQuery() {
    this.list
      .hookToQuery(query =>
        this.service.getList({
          ...this.pageQuery,
          ...(this.pageQuery.minExecutionDuration === null && {
            minExecutionDuration: undefined,
          }),
          ...(this.pageQuery.maxExecutionDuration === null && {
            maxExecutionDuration: undefined,
          }),
          ...(this.pageQuery.startTime && {
            startTime: this.convertToUTC(this.pageQuery.startTime),
          }),
          ...(this.pageQuery.endTime && {
            endTime: this.convertToUTC(this.pageQuery.endTime),
          }),
          ...query,
        }),
      )
      .subscribe(res => (this.data = res));
  }

  ngOnInit() {
    this.hookToQuery();
  }

  openModal(id: string) {
    this.service
      .get(id)
      .pipe(take(1))
      .subscribe(log => {
        this.selected = log;
        this.modalVisible = true;
      });
  }

  httpCodeClass(httpStatusCode: number): string {
    switch (httpStatusCode?.toString()[0]) {
      case '2':
        return 'bg-success';
      case '3':
        return 'bg-warning';
      case '4':
      case '5':
        return 'bg-danger';
      default:
        return 'bg-light';
    }
  }

  httpMethodClass(httpMethod: string): string {
    switch (httpMethod) {
      case 'GET':
        return 'bg-info';
      case 'POST':
        return 'bg-success';
      case 'DELETE':
        return 'bg-danger';
      case 'PUT':
        return 'bg-warning';
      default:
        return '';
    }
  }

  convertToUTC(localDateTime: string): string {
    return new Date(localDateTime).toISOString();
  }

}
