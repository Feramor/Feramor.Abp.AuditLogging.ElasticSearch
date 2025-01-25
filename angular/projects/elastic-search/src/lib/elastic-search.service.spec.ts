import { TestBed } from '@angular/core/testing';
import { ElasticSearchService } from './services/elastic-search.service';
import { RestService } from '@abp/ng.core';

describe('ElasticSearchService', () => {
  let service: ElasticSearchService;
  const mockRestService = jasmine.createSpyObj('RestService', ['request']);
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        {
          provide: RestService,
          useValue: mockRestService,
        },
      ],
    });
    service = TestBed.inject(ElasticSearchService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
