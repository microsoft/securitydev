import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
// models
import { SecureScore, ActiveAlert, ModelWithTheMostAlert, AlertByEntity } from 'src/app/models/graph/alert-statistic.model';
import { AverageComparativeScore } from 'src/app/models/graph';
import { BreadcrumbItem } from '../../common/content-header/breadcrumbs/breadcrumbs.component';
// services
import { DashboardService } from 'src/app/services/dashboard.service';
import { AlertFilterService } from 'src/app/services';

import { LoaderService } from 'src/app/services/loader.service';

@Component({
    selector: 'app-dashboard-page',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
    public title = 'Dashboard';
    public breadcrumbItems: BreadcrumbItem[] =
        [
            { Title: 'Dashboard', URL: '/dashboard' }
        ];

    public secureScore: SecureScore;
    public alertsByStatus: ActiveAlert[];
    public alertsByEntity: AlertByEntity;
    public alertsByProvider: ModelWithTheMostAlert[];

    // secure score profiles details
    public barData: AverageComparativeScore[];
    // public barData: AverageComparativeScore;
    public doughnutData: object;

    constructor(
        private dashboardService: DashboardService,
        private alertFilterService: AlertFilterService,
        private router: Router,
        private loader: LoaderService
    ) {
        this.loader.Show('');
    }

    ngOnInit(): void {
        this.getAlertStatistics();
    }

    getAlertStatistics(): void {
        this.loader.Show('');
        this.dashboardService.getAlertStatistics().subscribe(response => {
            this.secureScore = response.secureScore;
            this.alertsByStatus = response.alertsByStatus;
            this.alertsByEntity = response.alertsByEntity;
            this.alertsByProvider = response.alertsByProvider;
            this.loader.Hide();
            this.barData = this.secureScore.values;
            this.doughnutData = {
                maxScore: this.secureScore.max,
                currentScore: this.secureScore.current,
            };
        });
    }

    filterAlert(filterValue: { key: string, value: string }): void {
        // set filters
        this.alertFilterService.SetFilter([filterValue]);
        // and navigate to the Alert list page
        this.router.navigate(['/alerts']);
    }
}
