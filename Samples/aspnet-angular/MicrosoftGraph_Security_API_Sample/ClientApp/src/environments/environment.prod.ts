import { IEnvironment } from './environment.interface';

export const environment: IEnvironment = {
  production: true,
  baseUrl: '/api',
  MSAL: {
    clientID: 'Enter_Your_Appid',
    redirectUri: 'Enter_Your_URL',
    cacheLocation: 'localStorage',
    piiLoggingEnabled: true,
    authority: 'https://login.microsoftonline.com/common',
    validateAuthority: true,
    protectedResourceMap: [['/api', ['Enter_Your_Appid']]]
  }
};
