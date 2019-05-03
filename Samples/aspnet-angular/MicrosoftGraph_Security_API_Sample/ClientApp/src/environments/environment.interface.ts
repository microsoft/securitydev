export interface IEnvironment {
    production: boolean;
    baseUrl: string;
    MSAL: {
        clientID: string;
        redirectUri: string;
        cacheLocation: 'localStorage' | 'sessionStorage';
        piiLoggingEnabled: boolean;
        authority: string,
        validateAuthority: boolean,
        protectedResourceMap: [[string, string[]]]
    };
}
