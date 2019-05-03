import { Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators, AbstractControl } from '@angular/forms';
// models
import { ControlBase } from '../models';

@Injectable()
export class DynamicFormControlService {
    constructor() { }

    toFormGroup(controls: ControlBase<any>[]) {
        const group: { [key: string]: AbstractControl } = {};

        controls.forEach(control => {
            group[control.key] = control.required ? new FormControl(control.value || '', Validators.required)
                : new FormControl(control.value || '');
        });

        return new FormGroup(group);
    }
}
