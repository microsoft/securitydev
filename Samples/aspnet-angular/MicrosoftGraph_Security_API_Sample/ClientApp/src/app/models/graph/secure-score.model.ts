import { SecurityVendorInformation } from './security-vendor.model';
import { AverageComparativeScore } from './average-comparative-score.model';
import { ControlScore } from './control-score.model';

export class SecureScore {
  activeUserCount?: number;
  averageComparativeScores: AverageComparativeScore[];
  azureTenantId: string;
  controlScores: ControlScore[];
  createdDateTime?: Date;
  currentScore?: number;
  enabledServices: string[];
  id: string;
  licensedUserCount?: number;
  maxScore?: number;
  vendorInformation: SecurityVendorInformation;
  oDataType: string;
  // additionalData: IDictionary<string, object>;
}
