import {
    User,
    SecurityVendorInformation,
    Trigger,
    UserSecurityState,
    HostSecurityState,
    NetworkConnection,
    FileSecurityState,
    Process,
    VulnerabilityState,
    RegistryKeyState,
    MalwareState,
    CloudAppSecurityState,
    AlertHistoryState,
} from '.';

export class AlertDetails {
    public id: string;
    public title: string;
    public createdDateTime: Date | string | null;
    public comments: string[];
    public status: string;
    public severity: string;
    public feedback: string;
    public assignedTo: User;
    public description: string;
    public recommendedActions: string[];
    public userAccountDevices: User[];
    public vendor: SecurityVendorInformation;
    public triggers: Trigger[];
    public users: UserSecurityState[];
    public hosts: HostSecurityState[];
    public networkConnections: NetworkConnection[];
    public files: FileSecurityState[];
    public processes: Process[];
    public registryKeyUpdates: RegistryKeyState[];
    public malwareStates: MalwareState[];
    public sourceMaterials: string[];
    public tags: string[];
    public vulnerabilityStates: VulnerabilityState[];
    public cloudAppStates: CloudAppSecurityState[];
    public detectionIds: string[];
    public historyStates: AlertHistoryState[];
}
