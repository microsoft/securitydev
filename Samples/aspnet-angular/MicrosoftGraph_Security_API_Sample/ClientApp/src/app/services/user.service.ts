import { Injectable, EventEmitter, OnDestroy } from '@angular/core';
import { Observable, of, Subscription } from 'rxjs';

// models
import { MsalService, BroadcastService } from '@azure/msal-angular';
import { User } from 'msal';

@Injectable({ providedIn: 'root' })
export class UserService implements OnDestroy {
    private toggleUserPanelEvent: EventEmitter<void>;
    private loginSuccessSubscription: Subscription;
    private acquireTokenFailureSubscription: Subscription;

    constructor(
        private msal: MsalService,
        private broadcastService: BroadcastService
    ) {
        this.toggleUserPanelEvent = new EventEmitter<void>();
        this.loginSuccessSubscription = this.broadcastService.subscribe(
            'msal:loginSuccess',
            (payload) => {
                // clear previous user info, saved in cache
                localStorage.removeItem('alertFilters');
                localStorage.removeItem('actionFilters');
                localStorage.removeItem('subscriptionFilters');
            }
        );

        this.acquireTokenFailureSubscription = this.broadcastService.subscribe(
            'msal:acquireTokenFailure',
            (error) => {
                // clear previous user info, saved in cache
                localStorage.removeItem('alertFilters');
                localStorage.removeItem('actionFilters');
                localStorage.removeItem('subscriptionFilters');
                // if error is occur during refresh token process, login again
                let graphScopes = ["IdentityRiskyUser.Read.All"];
                this.msal.loginRedirect(graphScopes);
            }
        );
    }

    getCurrentUser(): User {
        return this.msal.getUser();
    }

    logout(): void {
        this.msal.logout();
    }

    get Events() {
        return { ToggleUserPanel: this.toggleUserPanelEvent };
    }

    toggleUserPanel(): void { this.toggleUserPanelEvent.emit(); }

    ngOnDestroy() {
        this.broadcastService.getMSALSubject().next(1);
        if (this.loginSuccessSubscription) {
            this.loginSuccessSubscription.unsubscribe();
        }
        if (this.acquireTokenFailureSubscription) {
            this.acquireTokenFailureSubscription.unsubscribe();
        }
    }
}
