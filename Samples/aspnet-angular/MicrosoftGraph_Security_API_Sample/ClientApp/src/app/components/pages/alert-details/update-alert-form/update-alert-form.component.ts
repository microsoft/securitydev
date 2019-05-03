import { Component, OnInit, Output, EventEmitter, Input, OnChanges, SimpleChanges } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
// models
import { AlertDetails } from 'src/app/models/graph';
import { AlertUpdateModel } from 'src/app/models/request';
// services
import { AlertValuesService, UserService } from 'src/app/services';

@Component({
    selector: 'app-update-alert-form',
    templateUrl: './update-alert-form.component.html',
    styleUrls: ['./update-alert-form.component.css']
})
export class UpdateAlertFormComponent implements OnInit, OnChanges {
    @Input() alert: AlertDetails;
    @Output() update: EventEmitter<AlertUpdateModel>;

    public title = 'Manage alert';
    public form: FormGroup;
    public values: { [key: string]: string[] };

    constructor(
        private valuesService: AlertValuesService,
        private userService: UserService
    ) {
        this.update = new EventEmitter<AlertUpdateModel>();
    }

    ngOnInit(): void {
        this.form = new FormGroup({
            'id': new FormControl(this.alert.id),
            'status': new FormControl(this.alert.status),
            'feedback': new FormControl(this.alert.feedback),
            'assignedTo': new FormControl(this.alert.assignedTo ? this.alert.assignedTo.upn : ''),
            'newComment': new FormControl(''),
        });
        this.values = {
            statuses: this.valuesService.Values.alertStatuses,
            feedbacks: this.valuesService.Values.alertFeedbacks
        };
    }

    ngOnChanges(changes: SimpleChanges): void {
        // clear form fields
        if (this.form && this.form.controls['newComment']) {
            this.form.controls['newComment'].setValue('');
        }
    }

    public submit(): void {
        // emit event
        if (this.form.valid) {
            // add all comments include new
            const updates = this.form.value;
            updates.comments = this.alert.comments.concat([updates.newComment]);
            delete updates.newComment;
            // add author info (current authenticated user)
            updates.userUpn = this.userService.getCurrentUser().displayableId;
            // submit form
            this.update.emit(this.form.value as AlertUpdateModel);
        }
    }
}
