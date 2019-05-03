export class Subscription {
    id: string;
    changeType: string;
    clientState: string;
    expirationDateTime: string | null;
    notificationUrl: string;
    resource: string;
    error: string;
}
