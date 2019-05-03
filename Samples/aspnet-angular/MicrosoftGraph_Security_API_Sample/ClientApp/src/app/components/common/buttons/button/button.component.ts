import { Component, Input, Output, EventEmitter, HostBinding } from '@angular/core';

@Component({
    selector: 'app-button',
    templateUrl: './button.component.html',
    styleUrls: ['./button.component.css']
})
export class ButtonComponent {
    @Input() title: string;
    @Input() @HostBinding('class.disabled') disabled: boolean;
    @Output() btnClick: EventEmitter<void>;

    public constructor() {
        this.btnClick = new EventEmitter<void>();
    }

    public onClick(): void {
        // tslint:disable-next-line:no-unused-expression
        !this.disabled && this.btnClick.emit();
    }
}
