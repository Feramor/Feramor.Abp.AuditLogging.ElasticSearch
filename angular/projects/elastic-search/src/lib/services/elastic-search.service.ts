import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class ElasticSearchService {
  apiName = 'ElasticSearch';

  constructor(private restService: RestService) {}

  sample() {
    return this.restService.request<void, any>(
      { method: 'GET', url: '/api/ElasticSearch/sample' },
      { apiName: this.apiName }
    );
  }
}
