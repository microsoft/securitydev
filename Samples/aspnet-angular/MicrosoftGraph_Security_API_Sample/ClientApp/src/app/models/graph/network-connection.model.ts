export class NetworkConnection {
    public status: ConnectionStatus | null;
    public sourcePort: string;
    public sourceAddress: string;
    public riskScore: string;
    public protocol: SecurityNetworkProtocol | null;
    public natSourcePort: string;
    public natSourceAddress: string;
    public natDestinationPort: string;
    public natDestinationAddress: string;
    public localDnsName: string;
    public domainRegisteredDateTime: Date | string | null;
    public direction: ConnectionDirection | null;
    public destinationUrl: string;
    public destinationPort: string;
    public destinationDomain: string;
    public destinationAddress: string;
    public applicationName: string;
    public urlParameters: string;
    public additionalData: { [key: string]: any };
}

export enum ConnectionStatus {
    Unknown = 0,
    Attempted = 1,
    Succeeded = 2,
    Blocked = 3,
    Failed = 4,
    UnknownFutureValue = 127
}

export enum SecurityNetworkProtocol {
    Unknown = -1,
    Ip = 0,
    Icmp = 1,
    Igmp = 2,
    Ggp = 3,
    Ipv4 = 4,
    Tcp = 6,
    Pup = 12,
    Udp = 17,
    Idp = 22,
    Ipv6 = 41,
    Ipv6RoutingHeader = 43,
    Ipv6FragmentHeader = 44,
    IpSecEncapsulatingSecurityPayload = 50,
    IpSecAuthenticationHeader = 51,
    IcmpV6 = 58,
    Ipv6NoNextHeader = 59,
    Ipv6DestinationOptions = 60,
    Nd = 77,
    Raw = 255,
    Ipx = 1000,
    Spx = 1256,
    SpxII = 1257,
    UnknownFutureValue = 32767
}

export enum ConnectionDirection {
    Unknown = 0,
    Inbound = 1,
    Outbound = 2,
    UnknownFutureValue = 127
}

