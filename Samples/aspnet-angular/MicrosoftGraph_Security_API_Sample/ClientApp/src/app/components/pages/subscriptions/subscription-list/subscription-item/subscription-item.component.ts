import { Component, Input } from '@angular/core';
// models
import { Subscription } from 'src/app/models/graph/subscription.model';

@Component({
  selector: 'app-subscription-item',
  templateUrl: './subscription-item.component.html',
  styleUrls: ['./subscription-item.component.css']
})
export class SubscriptionItemComponent {
  @Input() public item: Subscription;
}
