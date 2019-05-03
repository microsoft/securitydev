import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-doughnut-chart',
  templateUrl: './doughnut-chart.component.html',
  styleUrls: ['./doughnut-chart.component.css']
})
export class DoughnutChartComponent implements OnInit {
  public labels: string[] = ['Hight', 'Medium', 'Low', 'Informational'];
  public type = 'doughnut';
  public legend = false;
  public colors: Array<any> =
    [
      {
        backgroundColor: ['#C00000', '#FFC700', '#F2994A', '#C4C4C4']
      }
    ];
  public options: any = {
    tooltips: {
      enabled: true
    }
  };

  @Input() public data: string[];

  ngOnInit(): void {
    if (this.data.every(el => el.toString() === '0') === true) {
      this.options.tooltips.enabled = false;
      this.data = ['1'];
      this.colors = [
        {
          backgroundColor: ['grey']
        }
      ];
    }
  }
}
