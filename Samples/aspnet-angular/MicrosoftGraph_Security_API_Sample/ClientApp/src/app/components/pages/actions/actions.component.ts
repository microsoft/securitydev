import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
// models
import { BreadcrumbItem } from '../../common/content-header/breadcrumbs/breadcrumbs.component';
import { Action } from 'src/app/models/graph/action.model';
// services
import { ActionService } from 'src/app/services/action.service';
import { ActionFilterService } from 'src/app/services';
import { ModalWindowsService } from 'src/app/services/modal-windows.service';
import { LoaderService } from 'src/app/services/loader.service';

@Component({
    selector: 'app-actions-page',
    templateUrl: './actions.component.html',
    styleUrls: ['./actions.component.css']
})
export class ActionsComponent implements OnInit {
    public title = 'Actions';
    public breadcrumbItems: BreadcrumbItem[] = [];
    public actions: Action[];

    constructor(
        private activateRoute: ActivatedRoute,
        private actionService: ActionService,
        private actionFilterService: ActionFilterService,
        private modalWindows: ModalWindowsService,
        private loader: LoaderService
    ) {
        this.loader.Show('');
        // immediately get alert id from route params and create breadcrumbs
        this.breadcrumbItems = this.getBreadcrumbsByAlertId(this.activateRoute.snapshot.params['id']);
        // subscribe to route params changes
        activateRoute.params.subscribe(params => {
            // get new alert id and update breadcrumbs
            this.breadcrumbItems = this.getBreadcrumbsByAlertId(params['id']);
        });

        // subscribe to closing invoke action form
        this.modalWindows.InvokeActionForm.State.ShowEvent.subscribe(isOpen => {
            // reload actions
            this.LoadActions();
        });
    }

    private getBreadcrumbsByAlertId(alertId?: string): BreadcrumbItem[] {
        return alertId
            ? [
                { Title: 'Dashboard', URL: '/dashboard' },
                { Title: 'Alerts', URL: '/alerts' },
                { Title: 'Alert Details', URL: `/alerts/${alertId}` },
                { Title: 'Actions', URL: `/actions/${alertId}` }
            ]
            : [
                { Title: 'Dashboard', URL: '/dashboard' },
                { Title: 'Actions', URL: '/actions' }
            ];
    }

    ngOnInit(): void {
        this.LoadActions();
    }

    public newActionButtonClick(): void {
        // navigate to the invoke action form
        this.modalWindows.InvokeActionForm.Show();
    }

    public LoadActions(): void {
        this.loader.Show('');
        // get filters adopted to query
        const filtersForQuery = this.actionFilterService.ToFilterQuery();
        // get alerts by filters
        this.actionService.getActionsByFilter(filtersForQuery).subscribe(response => {
            if (response && Array.isArray(response)) {
                // sort by expiration date time
                this.actions = response.sort((s1, s2) => s1.submittedDateTime > s2.submittedDateTime
                    ? -1 : s1.submittedDateTime < s2.submittedDateTime
                        ? 1
                        : 0);
            }
            this.loader.Hide();
        });
    }
}
