// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.
import { IEnvironment } from './environment.interface';

export const environment: IEnvironment = {
  production: false,
  baseUrl: 'http://localhost:5000/api',
  MSAL: {
    clientID: 'Enter_Your_Appid',
    redirectUri: 'http://localhost:55065',
    cacheLocation: 'localStorage',
    piiLoggingEnabled: true,
    authority: 'https://login.microsoftonline.com/common',
    validateAuthority: true,
    protectedResourceMap: [['http://localhost:5000/api', ['Enter_Your_Appid']]]
  }
};

/*
 * In development mode, to ignore zone related error stack frames such as
 * `zone.run`, `zoneDelegate.invokeTask` for easier debugging, you can
 * import the following file, but please comment it out in production mode
 * because it will have performance impact when throw error
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
