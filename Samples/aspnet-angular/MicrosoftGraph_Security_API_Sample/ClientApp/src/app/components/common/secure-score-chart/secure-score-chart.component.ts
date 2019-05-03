import { Component, Input } from '@angular/core';
import { AverageComparativeScore, SecureScore } from 'src/app/models/graph';

@Component({
  selector: 'app-secure-score-chart',
  templateUrl: './secure-score-chart.component.html',
  styleUrls: ['./secure-score-chart.component.css'],
})
export class SecureScoreChartComponent {
  @Input() public barData: AverageComparativeScore[];
  @Input() public doughnutData: SecureScore;
  @Input() public isSecureScore: boolean;
  @Input() public isDashboard: boolean;
}
