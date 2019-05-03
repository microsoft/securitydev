import { Component, Input, OnChanges } from '@angular/core';
import { AverageComparativeScore } from 'src/app/models/graph';

interface Values {
  value: number;
  style: string;
}

@Component({
  selector: 'app-bar-diagram',
  templateUrl: './bar-diagram.component.html',
  styleUrls: ['./bar-diagram.component.css']
})
export class BarDiagramComponent implements OnChanges {

  @Input() public barData: AverageComparativeScore[];
  @Input() public isDashboard = false;
  @Input() public isSecureScore = false;

  public chart = null;

  public values: Values[] = [];

  ngOnChanges() {
    if (this.barData) {
      this.chart = {
        type: 'BarChart',
        data: this.barData ? this.barData.map((element, i) => {
          if (this.values) {
            this.values[i] = {
              value: element.averageScore ? element.averageScore : element.amount,
              style: `calc(10px + 39px * ${i})`,
            };
          }
          return element.comparedBy ? [element.comparedBy, element.averageScore] : [element.name, element.amount];
        }) : [],
        columnNames: this.barData ? this.barData.map(element => {
          return element.comparedBy ? element.comparedBy : element.name;
        }) : [],
        options: {
          height: 39 * this.barData.length,
          width: this.isSecureScore ? 400 : 300,
          legend: { position: 'none' },
          fontName: 'SegoeUI',
          tooltip: {
            trigger: 'none'
          },
          chartArea: {
            left: 0,
            width: '100%',
            height: '100%'
          },
          vAxis: {
            textPosition: 'in',
            textStyle: {
              color: 'black',
              fontSize: 14,
            },
            baseline: 1,
            baselineColor: 'red'
          },
          hAxis: {
            gridlines: {
              color: 'black',
              count: 0,
            }
          },
        },
      };
    }
  }
}
