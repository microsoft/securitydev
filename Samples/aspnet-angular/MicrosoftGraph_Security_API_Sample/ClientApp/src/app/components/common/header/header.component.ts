import { Component, HostBinding } from '@angular/core';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.css']
})
export class HeaderComponent {
    public title = 'Microsoft Graph Security Demo';

    @HostBinding('class.header-container') hostClass = true;
}
