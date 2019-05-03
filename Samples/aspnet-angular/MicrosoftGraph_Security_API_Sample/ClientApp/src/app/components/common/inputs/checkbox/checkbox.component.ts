import { Component, Input, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
    selector: 'app-checkbox',
    templateUrl: './checkbox.component.html',
    styleUrls: ['./checkbox.component.css'],
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => CheckboxComponent),
            multi: true
        }
    ]
})
export class CheckboxComponent implements ControlValueAccessor {
    @Input() title: string;
    private _checked: boolean;
    private onChange: (m: any) => void = () => { };
    private onTouched: (m: any) => void = () => { };

    get checked(): boolean {
        return this._checked;
    }

    set checked(value: boolean) {
        this._checked = value;
        this.onChange(value);
    }

    toggle() {
        this.checked = !this.checked;
    }

    writeValue(checked: boolean): void {
        this.checked = checked;
    }

    registerOnChange(fn: any): void { this.onChange = fn; this.onChange(this.checked); }

    registerOnTouched(fn: any): void { this.onTouched = fn; }
}
