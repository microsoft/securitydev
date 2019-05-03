import { ControlStateUpdate } from './control-state-update.model';

export class SecureScoreControlProfile {
  controlCategory: string;
  rank: number;
  title: string;
  maxScore?: number;
  userImpact: string;
  implementationCost: string;
  lastModifiedDateTime?: Date;
  actionUrl: string;
  deprecated?: boolean;
  remediation: string;
  remediationImpact: string;
  service: string;
  tier: string;
  azureTenantId: string;
  tenantSetState: string;
  tenantNote: string;
  threats: string[];
  secureStateUpdates: ControlStateUpdate[];
}
