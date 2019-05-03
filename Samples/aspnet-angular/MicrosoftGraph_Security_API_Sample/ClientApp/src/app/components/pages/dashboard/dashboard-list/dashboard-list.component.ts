import { Component, Input, Output, EventEmitter } from '@angular/core';
import { ModelWithTheMostAlert } from 'src/app/models/graph/alert-statistic.model';

@Component({
  selector: 'app-dashboard-list',
  templateUrl: './dashboard-list.component.html',
  styleUrls: ['./dashboard-list.component.css']
})
export class DashboardListComponent {
  @Input() public key: string;
  @Input() public items: ModelWithTheMostAlert[];
  @Output() valueClick: EventEmitter<{ key: string, value: string }>;

  constructor() {
    this.valueClick = new EventEmitter<{ key: string, value: string }>();
  }

  public onValueClick(filterValue: {value: string }): void {
    if (filterValue.value) {
        this.valueClick.emit({
          key: this.key,
          value: filterValue.value
        });
    }
  }
}
