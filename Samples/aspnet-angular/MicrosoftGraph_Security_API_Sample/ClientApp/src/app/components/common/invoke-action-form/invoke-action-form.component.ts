import { Component, OnInit, Output, EventEmitter, Input, OnChanges, SimpleChanges } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
// models
import { ActionCreateModel } from 'src/app/models/request';
// services
import { AlertValuesService, UserService } from 'src/app/services';

@Component({
    selector: 'app-invoke-action-form',
    templateUrl: './invoke-action-form.component.html',
    styleUrls: ['./invoke-action-form.component.css']
})
export class InvokeActionFormComponent implements OnInit, OnChanges {
    @Input() actionDetails: { name: string, value: string };
    @Output() submit: EventEmitter<ActionCreateModel>;

    public title = 'Remediation Action';
    public form: FormGroup;
    public values: { [key: string]: string[] };

    constructor(
        private valuesService: AlertValuesService,
        private userService: UserService
    ) {
        this.submit = new EventEmitter<ActionCreateModel>();
    }

    ngOnInit(): void {
        this.form = new FormGroup({
            'propertyName': new FormControl('', [Validators.required]),
            'propertyValue': new FormControl('', [Validators.required]),
            'action': new FormControl('', [Validators.required]),
            'provider': new FormControl('', [Validators.required]),
            'reason': new FormControl(''),
        });
        this.values = {
            providers: this.valuesService.Values.alertProviders,
            actions: ['Allow', 'Block']
        };
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (changes['actionDetails'] && changes['actionDetails'].currentValue) {
            const newActionDetails = changes['actionDetails'].currentValue;
            this.form.controls['propertyName'].setValue(newActionDetails.name);
            this.form.controls['propertyValue'].setValue(newActionDetails.value);
        }
    }

    public submitForm(): void {
        const user = this.userService.getCurrentUser();
        if (user && this.form.valid) {
            const formValue = this.form.value;
            // create new action
            const newAction: ActionCreateModel = {
                name: formValue.action,
                target: {
                    name: formValue.propertyName,
                    value: formValue.propertyValue
                },
                vendor: formValue.provider,
                reason: {
                    comment: formValue.reason
                },
                user: user.displayableId
            };
            this.submit.emit(newAction);
        }
    }
}
