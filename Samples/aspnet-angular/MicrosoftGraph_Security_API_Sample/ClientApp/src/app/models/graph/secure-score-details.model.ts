import { SecureScore } from './secure-score.model';
import { SecureScoreControlProfile } from './secure-score-control-profile.model';

export class SecureScoreDetails {
  topSecureScore: SecureScore;
  secureScoreControlProfiles: SecureScoreControlProfile[];
}
