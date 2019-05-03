import { Component, Input, AfterContentInit } from '@angular/core';
// models
import { SecureScoreControlProfile } from 'src/app/models/graph';

@Component({
    selector: 'app-secure-score-expandable-list',
    templateUrl: './secure-score-expandable-list.component.html',
    styleUrls: ['./secure-score-expandable-list.component.css']
})
export class SecureScoreExpandableListComponent implements AfterContentInit {
    @Input() public action: SecureScoreControlProfile;
    @Input() public isExpanded = false;

    public secureStateUpdates = [];

    ngAfterContentInit() {
        this.secureStateUpdates = this.action && this.action.secureStateUpdates;
    }

    public toggle(): void {
        this.isExpanded = !this.isExpanded;
    }
}
