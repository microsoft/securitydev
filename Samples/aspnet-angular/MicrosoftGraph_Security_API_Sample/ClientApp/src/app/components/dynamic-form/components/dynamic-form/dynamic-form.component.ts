import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { ControlBase } from '../../models';
import { DynamicFormControlService } from '../../services/dynamic-form-control.service';

@Component({
    selector: 'app-dynamic-form',
    templateUrl: './dynamic-form.component.html',
    styleUrls: ['./dynamic-form.component.css'],
    providers: [DynamicFormControlService]
})
export class DynamicFormComponent implements OnInit {
    @Input() controls: ControlBase<any>[] = [];
    @Input() actions: any[] = [];
    form: FormGroup;

    constructor(private controlService: DynamicFormControlService) { }

    ngOnInit() {
        this.form = this.controlService.toFormGroup(this.controls);
    }

    onSubmit() {
        console.log(this.form.valid);
        console.log(this.form.value);
    }

    onClearFilters() {
        this.form = this.controlService.toFormGroup(this.controls);
    }
}
