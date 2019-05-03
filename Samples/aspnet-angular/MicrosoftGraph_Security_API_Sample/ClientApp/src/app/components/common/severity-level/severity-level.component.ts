import { Component, Input } from '@angular/core';

@Component({
    selector: 'app-severity-level',
    templateUrl: './severity-level.component.html',
    styleUrls: ['./severity-level.component.css']
})
export class SeverityLevelComponent {
    @Input() level: 'low' | 'medium' | 'high' | 'informational';
    @Input() size: 'large' | 'small' = 'small';
}
