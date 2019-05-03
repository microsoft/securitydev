import { Component, OnInit, Input } from '@angular/core';
import { ActiveAlert } from 'src/app/models/graph/alert-statistic.model';

@Component({
  selector: 'app-operation-statuses-alerts',
  templateUrl: './operation-statuses-alerts.component.html',
  styleUrls: ['./operation-statuses-alerts.component.css']
})
export class OperationStatusesAlertsComponent {

  @Input() public alertsByStatus: ActiveAlert[];
}
