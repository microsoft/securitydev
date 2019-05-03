import { Component, Input } from '@angular/core';
// models
import { AlertHistoryState } from 'src/app/models/graph';

@Component({
    selector: 'app-activity-history',
    templateUrl: './activity-history.component.html',
    styleUrls: ['./activity-history.component.css']
})
export class ActivityHistoryComponent {
    public title = 'Activity History';
    @Input() isExpanded = true;
    @Input() items: AlertHistoryState[];
}
