import { Component, OnInit } from '@angular/core';
import { ElasticSearchService } from '../services/elastic-search.service';

@Component({
  selector: 'lib-elastic-search',
  template: ` <p>elastic-search works!</p> `,
  styles: [],
})
export class ElasticSearchComponent implements OnInit {
  constructor(private service: ElasticSearchService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
