import { Component, Input, HostBinding, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
// models
import { Action } from 'src/app/models/graph';
// services
import { AlertFilterService } from 'src/app/services';

@Component({
  selector: 'app-action-list',
  templateUrl: './action-list.component.html',
  styleUrls: ['./action-list.component.css']
})
export class ActionListComponent {
  public title = 'Actions';

  @Input() public actions: Action[];
  @HostBinding('class.page-content-linear-container') hostClass = true;
  @Output() valueClick: EventEmitter<{ key: string, value: string }>;

  constructor(
    private alertFilterService: AlertFilterService,
    private router: Router
  ) {
    this.valueClick = new EventEmitter<{ key: string, value: string }>();
  }

  filterAlert(filterValue: { key: string, value: string }): void {
    if (filterValue.key === 'alert:id') {
      this.router.navigate([`/alerts/${filterValue.value}`]);
    } else {
      // set filters
      this.alertFilterService.SetFilter([filterValue]);
      // and navigate to the Alert list page
      this.router.navigate(['/alerts']);
    }
  }
}
