export class HostSecurityState {
    public fqdn: string;
    public isAzureAdJoined: boolean | null;
    public isAzureAdRegistered: boolean | null;
    public isHybridAzureDomainJoined: boolean | null;
    public netBiosName: string;
    public os: string;
    public privateIpAddress: string;
    public publicIpAddress: string;
    public riskScore: string;
    public additionalData: { [key: string]: any };
}
