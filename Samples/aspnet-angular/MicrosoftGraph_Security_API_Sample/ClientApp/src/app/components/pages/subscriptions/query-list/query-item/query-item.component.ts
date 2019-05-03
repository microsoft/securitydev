import { Component, Input, ViewEncapsulation } from '@angular/core';
// services
import { ClipboardService } from 'ngx-clipboard';

@Component({
    selector: 'app-subscription-query-item',
    templateUrl: './query-item.component.html',
    styleUrls: ['./query-item.component.css'],
})
export class SubscriptionQueryItemComponent {
    @Input() title: string;
    @Input() value: string;

    public constructor(private clipboardService: ClipboardService) { }

    public copyToClipboard(text: string): void {
        this.clipboardService.copyFromContent(text);
    }
}
