import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
// models
import { ControlBase, ControlType } from '../../models';

@Component({
    selector: 'app-dynamic-form-control',
    templateUrl: './dynamic-form-control.component.html'
})
export class DynamicFormControlComponent {
    @Input() control: ControlBase<any>;
    @Input() form: FormGroup;
    public controlType = ControlType;

    get isValid() { return this.form.controls[this.control.key].valid; }
}
