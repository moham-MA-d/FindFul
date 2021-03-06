// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

import { GlobalConstants } from "src/app/common/global";

//Map() is used like a dictionary with key, value parameters and have set() and get() methods.
export const environment = {
  memberCache: new Map(),
  production: false,
  apiUrl: GlobalConstants.APIURL,
  hubUrl : GlobalConstants.HUBURL,
  useSignalR: GlobalConstants.USESIGNALR
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
