import { Alert, AlertDetails } from '../graph';
import { Queries } from '.';

export interface AlertSearchResult {
    alerts: Alert[];
    queries: Queries;
}

export interface AlertDetailsResult {
    alertDetails: AlertDetails;
    queries: Queries;
}
