import { Component, Input, forwardRef, HostBinding } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from '@angular/forms';
// models

@Component({
    selector: 'app-textarea',
    templateUrl: './textarea.component.html',
    styleUrls: ['./textarea.component.css'],
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => TextareaComponent),
            multi: true,
        }
    ]
})
export class TextareaComponent implements ControlValueAccessor {
    private _value: string;

    @Input() public title: string;
    @Input() public rows = 3;
    @Input() public placeholder: string;

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
