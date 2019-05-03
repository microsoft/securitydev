import { Component, OnInit } from '@angular/core';
// models
import { BreadcrumbItem } from '../../common/content-header/breadcrumbs/breadcrumbs.component';
import { SecureScoreDetails, SecureScoreControlProfile, AverageComparativeScore, SecureScore, ControlScore } from '../../../models/graph';
import { Queries } from 'src/app/models/response';
// services
import { SecureScoreService } from 'src/app/services/secure-score.service';
import { LoaderService } from 'src/app/services/loader.service';

@Component({
    selector: 'app-secure-score-page',
    templateUrl: './secure-score.component.html',
    styleUrls: ['./secure-score.component.css']
})
export class SecureScoreComponent implements OnInit {

    public secureScoreDetails: SecureScoreDetails;
    public queries: Queries;
    public actions: SecureScoreControlProfile[];
    public barData: AverageComparativeScore[];
    public doughnutData: SecureScore;
    public infoPanelData: ControlScore[];
    public isExpandedAll = false;

    public title = 'secure score';
    public breadcrumbItems: BreadcrumbItem[] = [
        { Title: 'Dashboard', URL: '/dashboard' },
        { Title: 'Secure Score', URL: '/secure-score' }
    ];

    constructor(
        private secureScoreService: SecureScoreService,
        private loader: LoaderService
    ) {
        this.loader.Show('');
    }

    ngOnInit(): void {
        this.getSecureScoreDetails();
    }

    expandAll(): void {
        this.isExpandedAll = !this.isExpandedAll;
    }

    getSecureScoreDetails(): void {
        this.loader.Show('');
        this.secureScoreService.getSecureScoreDetails().subscribe(response => {
            if (response) {
                this.secureScoreDetails = response.secureScoreDetails;
                this.queries = response.queries;
                this.actions = this.secureScoreDetails.secureScoreControlProfiles;
                this.barData = this.secureScoreDetails.topSecureScore.averageComparativeScores;
                this.doughnutData = this.secureScoreDetails.topSecureScore;
                this.infoPanelData = this.doughnutData.controlScores;
            }
            this.loader.Hide();
        });
    }

}
