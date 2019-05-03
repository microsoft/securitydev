import { Component, HostBinding } from '@angular/core';

@Component({
    selector: 'app-main-sidebar',
    templateUrl: './main-sidebar.component.html',
    styleUrls: ['./main-sidebar.component.css']
})
export class MainSidebarComponent {
    @HostBinding('class.sidebar-container') hostClass = true;
    @HostBinding('class.expanded') isExpanded = false;

    public toggle() {
        this.isExpanded = !this.isExpanded;
    }
}
