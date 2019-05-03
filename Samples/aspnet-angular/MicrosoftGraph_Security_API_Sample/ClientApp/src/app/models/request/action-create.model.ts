import { ActionTarget, ActionReason } from '../graph/action.model';

export class ActionCreateModel {
    public name: string;
    public target: ActionTarget;
    public reason: ActionReason;
    public vendor: string;
    public user: string;
}
