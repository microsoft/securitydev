export class RegistryKeyState {
    public hive: RegistryHive | null;
    public key: string;
    public oldKey: string;
    public oldValueData: string;
    public oldValueName: string;
    public operation: RegistryOperation | null;
    public processId: number | null;
    public valueData: string;
    public valueName: string;
    public valueType: RegistryValueType | null;
    public additionalData: { [key: string]: any };
}

export enum RegistryHive {
    Unknown = 0,
    CurrentConfig = 1,
    CurrentUser = 2,
    LocalMachineSam = 3,
    LocalMachineSamSoftware = 4,
    LocalMachineSystem = 5,
    UsersDefault = 6,
    UnknownFutureValue = 127
}

export enum RegistryOperation {
    Unknown = 0,
    Create = 1,
    Modify = 2,
    Delete = 3,
    UnknownFutureValue = 127
}

export enum RegistryValueType {
    Unknown = 0,
    Binary = 1,
    Dword = 2,
    DwordLittleEndian = 3,
    DwordBigEndian = 4,
    ExpandSz = 5,
    Link = 6,
    MultiSz = 7,
    None = 8,
    Qword = 9,
    QwordlittleEndian = 10,
    Sz = 11,
    UnknownFutureValue = 127
}
