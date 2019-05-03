import { Component, Input, OnChanges, HostBinding, SimpleChanges } from '@angular/core';

export interface ILogoUser {
    displayName: string;
    picture?: string;
}

@Component({
    selector: 'app-user-logo',
    templateUrl: './user-logo.component.html',
    styleUrls: ['./user-logo.component.css']
})
export class UserLogoComponent implements OnChanges {
    @Input() user: ILogoUser;

    @Input()
    @HostBinding('style.width.px')
    @HostBinding('style.height.px')
    size = 28;

    public fontSize = 14;

    ngOnChanges(changes: SimpleChanges): void {
        this.fontSize = this.size / 2;
    }

    public getInitials(): string {
        if (this.user && this.user.displayName) {
            const nameParts = this.user.displayName.split(' ');
            if (nameParts && nameParts.length > 1) {
                return `${nameParts[0][0]}${nameParts[1][0]}`;
            } else {
                return `${nameParts[0][0]}${nameParts[0][0]}`;
            }
        } else {
            return 'Un'; // Unknown User
        }
    }
}
