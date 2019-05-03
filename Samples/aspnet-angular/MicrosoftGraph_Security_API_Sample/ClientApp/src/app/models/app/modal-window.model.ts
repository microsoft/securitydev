import { EventEmitter } from '@angular/core';

export class ModalWindow {
    public ShowEvent: EventEmitter<boolean>;

    constructor(public Route?: string) {
        this.ShowEvent = new EventEmitter<boolean>();
    }

    public Show(): void {
        this.ShowEvent.emit(true);
    }

    public Hide(): void {
        this.ShowEvent.emit(false);
    }
}

export class StateModal<T> {
    public ShowEvent: EventEmitter<ModalValue<T>>;

    constructor(public Route: string) {
        this.ShowEvent = new EventEmitter<ModalValue<T>>();
    }

    public Show(event: EventEmitter<T>, selected?: T): void {
        this.ShowEvent.emit(new ModalValue(true, event, selected));
    }

    public Hide(): void {
        this.ShowEvent.emit(new ModalValue(false, undefined));
    }
}

export class RouteModal<T> {
    public ResponseEvent: EventEmitter<T>;

    constructor(public Route: string) {
        this.ResponseEvent = new EventEmitter<T>();
    }
}

export class ModalValue<T> {
    constructor(public IsVisible: boolean, public Event: EventEmitter<T>, public Selected?: T) { }
}
