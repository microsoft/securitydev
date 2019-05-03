import { Subscription } from '../graph';
import { Queries } from '.';

export interface SubscriptionSearchResult {
    subscriptions: Subscription[];
    queries: Queries;
}
