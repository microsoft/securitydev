export class UserSecurityState {
    public aadUserId: string;
    public accountName: string;
    public domainName: string;
    public emailRole: EmailRole | null;
    public isVpn: boolean | null;
    public logonDateTime: Date | string | null;
    public logonId: string;
    public logonIp: string;
    public logonLocation: string;
    public logonType: LogonType | null;
    public onPremisesSecurityIdentifier: string;
    public riskScore: string;
    public userAccountType: UserAccountSecurityType | null;
    public userPrincipalName: string;
    public additionalData: { [key: string]: any };
}

export enum EmailRole {
    Unknown = 0,
    Sender = 1,
    Recipient = 2,
    UnknownFutureValue = 127
}

export enum LogonType {
    Unknown = 0,
    Interactive = 1,
    RemoteInteractive = 2,
    Network = 3,
    Batch = 4,
    Service = 5,
    UnknownFutureValue = 127
}

export enum UserAccountSecurityType {
    Unknown = 0,
    Standard = 1,
    Power = 2,
    Administrator = 3,
    UnknownFutureValue = 127
}
