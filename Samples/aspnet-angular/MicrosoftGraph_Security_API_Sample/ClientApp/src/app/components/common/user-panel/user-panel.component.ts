import { Component, HostBinding, OnInit } from '@angular/core';
// models
import { User } from 'msal';
import { ILogoUser } from '../user-logo/user-logo.component';
// services
import { UserService, AlertFilterService, ActionFilterService, SubscriptionFilterService } from 'src/app/services';

@Component({
    selector: 'app-user-panel',
    templateUrl: './user-panel.component.html',
    styleUrls: ['./user-panel.component.css']
})
export class UserPanelComponent implements OnInit {
    public user: User & ILogoUser;

    @HostBinding('class.show') isVisible = false;

    public constructor(
        private userService: UserService,
        private alertFilterService: AlertFilterService,
        private actionFilterService: ActionFilterService,
        private subscriptionFilterService: SubscriptionFilterService
    ) {
        const msalUser = this.userService.getCurrentUser();
        if (msalUser) {
            // add displayName property from IUserLogo interface
            this.user = { ...msalUser, displayName: msalUser.name };
        }
    }

    ngOnInit(): void {
        this.userService.Events.ToggleUserPanel.subscribe(() => {
            this.isVisible = !this.isVisible;
        });
    }

    public close(): void {
        this.userService.toggleUserPanel();
    }

    public LogOut(): void {
        // clear filters of current user
        this.alertFilterService.AlertFilters = null;
        this.actionFilterService.ActionFilters = null;
        this.subscriptionFilterService.SubscriptionFilters = null;
        // log out
        this.userService.logout();
    }
}
