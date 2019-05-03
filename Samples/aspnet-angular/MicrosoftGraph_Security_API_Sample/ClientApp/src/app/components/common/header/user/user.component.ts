import { Component } from '@angular/core';
// models
import { ILogoUser } from '../../user-logo/user-logo.component';
// services
import { UserService } from 'src/app/services/user.service';

@Component({
    selector: 'app-header-user-info',
    templateUrl: './user.component.html',
    styleUrls: ['./user.component.css']
})
export class UserComponent {
    public user: ILogoUser;

    public constructor(private userService: UserService) {
        const msalUser = this.userService.getCurrentUser();
        if (msalUser) {
            this.user = { displayName: msalUser.name };
        }
    }

    public userClick(): void {
        this.userService.toggleUserPanel();
    }
}
