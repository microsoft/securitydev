import { Component, Input, OnChanges } from '@angular/core';
import { SecureScore } from 'src/app/models/graph';

@Component({
  selector: 'app-doughnut-diagram',
  templateUrl: './doughnut-diagram.component.html',
  styleUrls: ['./doughnut-diagram.component.css']
})
export class DoughnutDiagramComponent implements OnChanges {

  @Input() public doughnutData: SecureScore;
  @Input() public isDashboard = false;
  @Input() public isSecureScore = false;

  public chart = null;

  ngOnChanges() {
    if (this.doughnutData) {
      this.chart = {
        type: 'doughnut',
        data: this.doughnutData && [this.doughnutData.currentScore, this.doughnutData.maxScore - this.doughnutData.currentScore],
        labels: ['Current Score', ''],
        legend: false,
        colors: [
          { backgroundColor: ['#BF0000', '#C4C4C4'] }
        ]
      };
    }
  }
}
