import { Component, Input } from '@angular/core';

@Component({
    selector: 'app-expandable-list',
    templateUrl: './expandable-list.component.html',
    styleUrls: ['./expandable-list.component.css']
})
export class ExpandableListComponent {
    @Input() public title: string;
    @Input() public isExpanded = false;

    public toggle(): void {
        this.isExpanded = !this.isExpanded;
    }
}
