import { Component, Output, EventEmitter } from '@angular/core';
// models
import { ActionCreateModel } from 'src/app/models/request';
// services
import { ActionService } from 'src/app/services';
import { ModalWindowsService } from 'src/app/services/modal-windows.service';

@Component({
    selector: 'app-invoke-action-form-action-page',
    templateUrl: './invoke-action-form.component.html',
    styleUrls: ['./invoke-action-form.component.css']
})
export class InvokeActionFormComponent {
    @Output() close: EventEmitter<void>;

    constructor(
        private actionService: ActionService,
        private modalWindows: ModalWindowsService
    ) {
        this.close = new EventEmitter<void>();
    }

    public invokeAction(newAction: ActionCreateModel): void {
        // send request to create action
        this.actionService.createAction(newAction).subscribe(result => {
            if (result) {
                // hide form
                this.onClose();
            }
        });
    }

    public onClose(): void {
        // send outside component event about canceling
        this.modalWindows.InvokeActionForm.Hide();
    }
}
