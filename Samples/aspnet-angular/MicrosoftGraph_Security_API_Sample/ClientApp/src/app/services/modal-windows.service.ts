import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
// models
import { ModalWindow } from '../models/app';

@Injectable({ providedIn: 'root' })
export class ModalWindowsService {

    constructor(private router: Router) { }

    public InvokeActionForm = {
        State: new ModalWindow('app-invoke-action-form'),
        Show: () => this.NavigateTo(this.InvokeActionForm.State.Route),
        Hide: () => {
            this.InvokeActionForm.State.Hide();
            this.CloseAll();
        }
    };

    private NavigateTo(componentName: string): void {
        if (componentName) {
            const outlets = { modal: componentName };
            this.router.navigate([{ outlets: outlets }]);
        }
    }

    public CloseAll(): void {
        this.router.navigate([{ outlets: { modal: null } }]);
    }

    public Back(): void {
        window.history.back();
    }
}
