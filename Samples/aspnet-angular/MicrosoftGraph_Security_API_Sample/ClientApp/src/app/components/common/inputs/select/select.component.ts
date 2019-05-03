import { Component, Input, forwardRef, OnChanges, SimpleChanges } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
    selector: 'app-select',
    templateUrl: './select.component.html',
    styleUrls: ['./select.component.css'],
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => SelectComponent),
            multi: true,
        }
    ]
})
export class SelectComponent implements ControlValueAccessor, OnChanges {
    public readonly itemHeight = 42;
    @Input() public title: string;
    @Input() placeholder: string;
    @Input() visibleItems = 5;
    @Input() public options: string[] = [];
    @Input() public noLabel: boolean;

    public visibleItemsListHeight: number;

    public isOpen = false;
    public selectedOption: string;

    ngOnChanges(changes: SimpleChanges): void {
        this.visibleItemsListHeight = this.visibleItems * this.itemHeight;
    }

    public optionSelect(option: string): void {
        this.writeValue(option);
        this.onTouched();
        this.isOpen = false;
    }

    public writeValue(value: string): void {
        this.selectedOption = value;
        this.onChange(this.selectedOption);
    }

    public onChange(value: string) { }

    public onTouched() { }

    public registerOnChange(fn): void {
        this.onChange = fn;
    }

    public registerOnTouched(fn): void {
        this.onTouched = fn;
    }

    public toggle(): void {
        this.isOpen = !this.isOpen;
    }
}
