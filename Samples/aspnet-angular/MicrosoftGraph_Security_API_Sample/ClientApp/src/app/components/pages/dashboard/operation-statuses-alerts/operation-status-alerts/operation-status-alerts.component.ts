import { Component, OnInit, Input } from '@angular/core';
import { Value, ActiveAlert } from 'src/app/models/graph/alert-statistic.model';

@Component({
  selector: 'app-operation-status-alerts',
  templateUrl: './operation-status-alerts.component.html',
  styleUrls: ['./operation-status-alerts.component.css']
})
export class OperationStatusAlertsComponent implements OnInit {

  public statusValues: string[];
  public values: Value[];
  public sumValue: number;
  public column = 'column';

  @Input() public alertByStatus: ActiveAlert;

  ngOnInit(): void {
    this.statusValues = this.alertByStatus.values.map(el => el.amount);
    this.values = this.alertByStatus.values;
    this.sumValue = this.statusValues.reduce((a, b) => (+a) + (+b), 0);
  }
}
