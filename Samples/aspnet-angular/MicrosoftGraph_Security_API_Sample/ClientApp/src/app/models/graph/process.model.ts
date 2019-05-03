import { FileHash } from '.';

export class Process {
    public accountName: string;
    public commandLine: string;
    public createdDateTime: Date | string | null;
    public fileHash: FileHash;
    public integrityLevel: ProcessIntegrityLevel | null;
    public isElevated: boolean | null;
    public name: string;
    public parentProcessCreatedDateTime: Date | string | null;
    public parentProcessId: number | null;
    public parentProcessName: string;
    public path: string;
    public processId: number | null;
    public additionalData: { [key: string]: any };
}

export enum ProcessIntegrityLevel {
    Unknown = 0,
    Untrusted = 1,
    Low = 2,
    Medium = 3,
    High = 4,
    System = 5,
    UnknownFutureValue = 127
}
