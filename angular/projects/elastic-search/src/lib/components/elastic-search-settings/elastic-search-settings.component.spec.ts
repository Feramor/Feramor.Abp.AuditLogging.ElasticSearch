import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ElasticSearchSettingsComponent } from './elastic-search-settings.component';

describe('ElasticSearchSettingsComponent', () => {
  let component: ElasticSearchSettingsComponent;
  let fixture: ComponentFixture<ElasticSearchSettingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ElasticSearchSettingsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ElasticSearchSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
