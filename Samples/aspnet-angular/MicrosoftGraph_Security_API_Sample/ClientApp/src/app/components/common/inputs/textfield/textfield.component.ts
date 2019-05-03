import { Component, Input, forwardRef, HostBinding } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from '@angular/forms';
// models

@Component({
    selector: 'app-textfield',
    templateUrl: './textfield.component.html',
    styleUrls: ['./textfield.component.css'],
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => TextfieldComponent),
            multi: true,
        }
    ]
})
export class TextfieldComponent implements ControlValueAccessor {
    private _value: string;

    @Input() public title: string;
    @Input() public type: string;
    @Input() public placeholder: string;
    @Input() public isSearch: boolean;
    @Input() public noLabel: boolean;

    @Input() @HostBinding('class.vertical') vertical = false;

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
