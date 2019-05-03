import { Component, Input, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Moment } from 'moment';

@Component({
    selector: 'app-datetime-picker',
    templateUrl: './datetime-picker.component.html',
    styleUrls: ['./datetime-picker.component.css'],
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => DateTimePickerComponent),
            multi: true,
        }
    ]
})
export class DateTimePickerComponent implements ControlValueAccessor {
    private _value: string;

    @Input() public title: string;
    @Input() public placeholder: string;
    @Input() public min: Moment;
    @Input() public max: Moment;

    get value() {
        return this._value;
    }

    set value(value: string) {
        this._value = value;
        this.onChange(value);
        this.onTouched();
    }

    public writeValue(value: string): void {
        this.value = value;
        this.onChange(this.value);
    }

    public onChange(value: string) { }

    public onTouched() { }

    public registerOnChange(fn): void {
        this.onChange = fn;
    }

    public registerOnTouched(fn): void {
        this.onTouched = fn;
    }
}
