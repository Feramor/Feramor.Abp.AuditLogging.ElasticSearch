import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { ElasticSearchComponent } from './components/elastic-search.component';
import { ElasticSearchService } from '@feramor.Abp.Audit-logging/elastic-search';
import { of } from 'rxjs';

describe('ElasticSearchComponent', () => {
  let component: ElasticSearchComponent;
  let fixture: ComponentFixture<ElasticSearchComponent>;
  const mockElasticSearchService = jasmine.createSpyObj('ElasticSearchService', {
    sample: of([]),
  });
  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ElasticSearchComponent],
      providers: [
        {
          provide: ElasticSearchService,
          useValue: mockElasticSearchService,
        },
      ],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ElasticSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
