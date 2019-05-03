import { Component, Input } from '@angular/core';
// models
import { AlertHistoryState } from 'src/app/models/graph';

@Component({
    selector: 'app-activity-history-item',
    templateUrl: './activity-history-item.component.html',
    styleUrls: ['./activity-history-item.component.css']
})
export class ActivityHistoryItemComponent {
    @Input() item: AlertHistoryState;
}
