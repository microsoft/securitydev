import { SecurityVendorInformation } from '.';

export class Action {
    public name: string;
    public submittedDateTime: Date | string | null;
    public target: ActionTarget;
    public reason: ActionReason;
    public securityVendorInformation: SecurityVendorInformation;
    public status: OperationStatus;
    public statusUpdateTime: Date | string | null;
}

export class ActionTarget {
    public name: string;
    public value: string;
}

export class ActionReason {
    public comment: string;
    public alertId?: string;
}

export enum OperationStatus {
    Pending = 'Pending',
    Complete = 'Complete'
}
