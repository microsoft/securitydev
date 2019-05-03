import { Device, RiskyUser } from '.';

export class User {
    public displayName: string;
    public email: string;
    public upn: string;
    public jobTitle: string;
    public manager: User;
    public officeLocation: string;
    public contactVia: string;
    public picture: string;
    public emailRole: string;
    public riskScore: string;
    public logonId: string;
    public riskyUser: RiskyUser;
    public domainName: string;
    public registeredDevices: Device[];
    public ownedDevices: Device[];
}
