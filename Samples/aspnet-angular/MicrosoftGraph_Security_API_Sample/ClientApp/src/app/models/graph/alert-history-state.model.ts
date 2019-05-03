import { User } from '.';

export class AlertHistoryState {
    public appId: string;
    public assignedTo: User;
    public feedback: string;
    public updatedDateTime: Date;
    public user: User;
    public status: string;
    public comments: string[];
}
