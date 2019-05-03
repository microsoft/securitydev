import { Component, OnInit, OnDestroy } from '@angular/core';
// models
import { LoaderState } from 'src/app/models/app';
// services
import { LoaderService } from 'src/app/services/loader.service';

@Component({
    selector: 'app-modal-container',
    templateUrl: 'modal-container.component.html',
    styleUrls: ['modal-container.component.css']
})
export class ModalContainerComponent implements OnInit, OnDestroy {
    // Loader
    public loaderState: LoaderState;

    constructor(private loader: LoaderService) { }

    ngOnInit(): void {
        // Subscribe to the show/hide windows events
        this.loader.Event.subscribe((newState: LoaderState) => {
            this.loaderState = newState;
        });
    }

    ngOnDestroy(): void {
        // Unsubscribe to the show/hide windows events
        this.loader.Event.unsubscribe();
    }
}
