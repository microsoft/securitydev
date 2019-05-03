import { Component, forwardRef, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
    selector: 'app-checkbox-group',
    templateUrl: './checkbox-group.component.html',
    styleUrls: ['checkbox-group.component.css'],
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => CheckboxGroupComponent),
            multi: true
        }
    ]
})
export class CheckboxGroupComponent implements ControlValueAccessor, OnChanges {
    private onChange: (m: any) => void;
    private onTouched: (m: any) => void;
    public isExpanded = false;
    // 'any' checkbox item
    public anyItem: CheckboxItem = {
        title: 'Any',
        value: 'any',
        isLevel: false
    };

    @Input() public title = '';
    @Input() public items: CheckboxItem[];
    @Input() public anySupport = true;
    @Input() public minimize = true;

    ngOnChanges(changes: SimpleChanges) {
        if (this.anySupport) {
            // if any items are not checked already, check 'any' item
            if (this.items && !this.items.find(item => item.checked)) {
                this.anyItem.checked = true;
            }
        }
    }

    writeValue(value: CheckboxItem[]): void { }

    registerOnChange(fn: any): void { this.onChange = fn; this.onChange(this.items); }

    registerOnTouched(fn: any): void { this.onTouched = fn; }

    toggle(key: string): void {
        // find item by key
        const checkedItem = this.items.find(item => item.value === key);
        if (checkedItem) {
            // uncheck 'any' item
            this.anyItem.checked = false;
            // toggle item
            checkedItem.checked = !checkedItem.checked;
            if (checkedItem.checked) {
                // if all items are checked
                if (!this.items.find(item => !item.checked)) {
                    this.checkAny(this.anyItem.value);
                }
            } else {
                // if all items are unchecked
                if (!this.items.find(item => item.checked)) {
                    this.checkAny(this.anyItem.value);
                }
            }
            this.onChange(this.items);
        }
    }

    checkAny(key: string): void {
        if (key === this.anyItem.value) {
            if (!this.anyItem.checked) {
                this.anyItem.checked = true;
                // uncheck other items
                this.items.forEach(item => item.checked = false);
            }
        }
    }

    expand(): void {
        this.isExpanded = true;
    }
}

export interface CheckboxItem {
    title: string;
    value: string;
    isLevel?: boolean;
    checked?: boolean;
}
