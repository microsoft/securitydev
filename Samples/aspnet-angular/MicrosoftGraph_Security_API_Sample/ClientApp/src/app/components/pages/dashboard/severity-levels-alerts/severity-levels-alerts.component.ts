import { Component, Input, OnInit } from '@angular/core';
import { ActiveAlert, Value } from 'src/app/models/graph/alert-statistic.model';

@Component({
  selector: 'app-severity-levels-alerts',
  templateUrl: './severity-levels-alerts.component.html',
  styleUrls: ['./severity-levels-alerts.component.css']
})
export class SeverityLevelsAlertsComponent {
  @Input() public direction: string;
  @Input() public values: Value[];
}
