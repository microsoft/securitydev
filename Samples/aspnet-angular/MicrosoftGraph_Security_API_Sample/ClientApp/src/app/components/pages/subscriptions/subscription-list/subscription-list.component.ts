import { Component, Input } from '@angular/core';
// models
import { Subscription } from 'src/app/models/graph/subscription.model';

@Component({
  selector: 'app-subscription-list',
  templateUrl: './subscription-list.component.html',
  styleUrls: ['./subscription-list.component.css']
})
export class SubscriptionListComponent {
  @Input() public subscriptions: Subscription[];
}
