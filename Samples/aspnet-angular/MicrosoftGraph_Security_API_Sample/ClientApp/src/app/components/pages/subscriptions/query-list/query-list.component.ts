import { Component, Input, HostBinding } from '@angular/core';
import { Queries } from 'src/app/models/response';

@Component({
    selector: 'app-subscription-query-list',
    templateUrl: './query-list.component.html',
    styleUrls: ['./query-list.component.css']
})
export class SubscriptionQueryListComponent {
    @Input() queries: Queries;
    @HostBinding('class.page-content-linear-container') hostClass = true;

    // have Object.keys accessible in the template and use it in *ngFor
    public objectKeys = Object.keys;
}
