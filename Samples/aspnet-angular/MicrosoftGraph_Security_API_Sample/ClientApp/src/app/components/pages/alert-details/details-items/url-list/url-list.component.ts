import { Component, Input } from '@angular/core';
import * as isUrl from 'is-url';

@Component({
    selector: 'app-url-list',
    templateUrl: './url-list.component.html',
    styleUrls: ['./url-list.component.css']
})
export class UrlListComponent {
    @Input() title: string;
    @Input() isExpanded = true;
    @Input() items: string[];

    public isUrl(value: string): boolean {
        return isUrl(value);
    }

    public onUrlClick(value: string): void {
        // tslint:disable-next-line:no-unused-expression
        value && window.open(value, '_blank');
    }
}
