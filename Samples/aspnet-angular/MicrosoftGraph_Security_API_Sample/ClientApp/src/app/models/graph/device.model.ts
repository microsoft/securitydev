export class Device {
    public deviceId: string;
    public displayName: string;
    public deviceMetadata: string;
    public deviceVersion: string;
    public isCompliant: boolean | null;
    public operatingSystem: string;
    public operatingSystemVersion: string;
    public isManaged: boolean | null;
    public accountEnabled: boolean | null;
    public approximateLastSignInDateTime: Date | string | null;
}
