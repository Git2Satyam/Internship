import { Component, OnInit } from '@angular/core'
const Chart = require('chart.js');
import 'chartjs-adapter-date-fns';
@Component({
  selector: 'app-monitorchart',
  templateUrl: './monitorchart.component.html',
  styleUrls: ['./monitorchart.component.css']
})
export class MonitorchartComponent implements OnInit {
  myChart: any
  constructor() { }

  ngOnInit(): void {
    this.RenderChart();
  }
  RenderChart() {
    if (this.myChart) {
      this.myChart.destroy();
    }
    //let range_min = moment(this.firstDate).subtract(2, 'days').format("MM/DD/YY HH:mm");
    //let range_max = moment(this.lastDate).add(2, 'days').format("MM/DD/YY HH:mm");
    //console.log(range_min); console.log(range_max)
    var ctx = <HTMLCanvasElement>document.getElementById('lineChart');
    this.myChart = new Chart(ctx, {
      type: 'line',
      data: {
        datasets: [
          {
            label: 'votes',
            data: [
              {
                x: "2015-01-04T18:34:00.000Z",
                y: 2
              },
              {
                x: "2015-01-05T11:56:00.000Z",
                y: 5
              },
              {
                x: "2015-01-06T05:23:00.000Z",
                y: 11
              },
              {
                x: "2015-01-07T07:35:00.000Z",
                y: 0
              },
              {
                x: "2015-01-08T19:12:00.000Z",
                y: 1
              },
              {
                x: "2015-01-09T00:00:00.000Z",
                y: 2
              },
              {
                x: "2015-01-10T00:00:00.000Z",
                y: 10
              },
              {
                x: "2015-01-11T00:00:00.000Z",
                y: 11
              }
            ]
          }
        ]
      },
      options: {
        maintainAspectRatio: false,
        responsive: true,
        scales: {
          yAxes: [{
            ticks: {
                beginAtZero: true
            }
        }],
          xAxes: [{
            position: "bottom",
            type: "time",
            time:{
              unit: 'hour'
            },
            adapters: {
              date: {
                zone: "UTC"
              }
            },
            ticks: {
              major: {
                enabled: true,
                font: 'bold'
              },
              callback: (value: any,ind: any, tick: any) => {
                  return value
              }
            }
          }]
        },
      }
    });
  }
}
