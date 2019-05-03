import { Component, Input, OnInit } from '@angular/core';
import { Value } from 'src/app/models/graph/alert-statistic.model';

@Component({
  selector: 'app-severity-level-alerts',
  templateUrl: './severity-level-alerts.component.html',
  styleUrls: ['./severity-level-alerts.component.css']
})
export class SeverityLevelAlertsComponent implements OnInit {
  public name: string;
  public value: string;

  @Input() public Value: Value;

  ngOnInit(): void {
    this.name = this.Value.name;
    this.value = this.Value.amount;
  }
}
