import { SecureScore } from '../graph/secure-score.model';

export class SecureScoreResult {
  oDataContext: string;
  oDataNextLink: string;
  value: SecureScore[];
}
