export class FileSecurityState {
    public fileHash: FileHash;
    public name: string;
    public path: string;
    public riskScore: string;
    public additionalData: { [key: string]: any };
}

export class FileHash {
    hashType: FileHashType | null;
    hashValue: string;
    additionalData: { [key: string]: any };
}

export enum FileHashType {
    Unknown = 0,
    Sha1 = 1,
    Sha256 = 2,
    Md5 = 3,
    AuthenticodeHash256 = 4,
    LsHash = 5,
    Ctph = 6,
    PeSha1 = 7,
    PeSha256 = 8,
    UnknownFutureValue = 127
}
