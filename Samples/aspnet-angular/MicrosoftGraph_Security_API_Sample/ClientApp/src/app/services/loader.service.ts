import { Injectable, EventEmitter } from '@angular/core';
// models
import { LoaderState } from '../models/app';

@Injectable({ providedIn: 'root' })
export class LoaderService {
    public Event: EventEmitter<LoaderState>;

    constructor() {
        this.Event = new EventEmitter<LoaderState>();
    }

    public Show(message: string): void {
        this.Event.emit(new LoaderState(true, message));
    }

    public Hide(): void {
        this.Event.emit({ IsVisible: false, Message: '' });
    }
}
