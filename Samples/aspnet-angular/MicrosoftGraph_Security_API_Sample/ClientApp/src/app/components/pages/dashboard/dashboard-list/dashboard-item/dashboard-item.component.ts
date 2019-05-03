import { Component, Input, Output, EventEmitter } from '@angular/core';
import { ModelWithTheMostAlert } from 'src/app/models/graph/alert-statistic.model';

@Component({
  selector: 'app-dashboard-item',
  templateUrl: './dashboard-item.component.html',
  styleUrls: ['./dashboard-item.component.css']
})
export class DashboardItemComponent {

  public row = 'row';
  @Input() public item: ModelWithTheMostAlert;
  @Output() valueClick: EventEmitter<{value: string}>;

  constructor() {
    this.valueClick = new EventEmitter<{ key: string, value: string }>();
  }

  onClick() {
    if (this.item.specification.filterValue) {
        this.valueClick.emit({
            value: this.item.specification.filterValue
        });
    }
}
}
