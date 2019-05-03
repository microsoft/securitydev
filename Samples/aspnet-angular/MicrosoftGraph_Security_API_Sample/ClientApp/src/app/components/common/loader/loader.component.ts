import { Component, Input } from '@angular/core';
// animations
import { FadeInOut } from './animation';

@Component({
    selector: 'app-loader',
    templateUrl: 'loader.component.html',
    styleUrls: ['loader.component.css'],
    animations: [FadeInOut]
})
export class LoaderComponent {
    @Input() message: string;
}
