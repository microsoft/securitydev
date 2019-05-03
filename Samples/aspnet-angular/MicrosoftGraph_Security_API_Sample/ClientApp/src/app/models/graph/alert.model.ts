export class Alert {
    id: string;
    title: string;
    severity: string;
    category: string;
    status: string;
    createdDateTime: Date | string | null;
    provider: string;
    assignedTo: string;
}
