import { GlobalConstants } from "src/app/common/global";

export const environment = {
  production: true,
  apiUrl: 'api/v1/',
  hubUrl: 'hubs/',
  memberCache: new Map(),
  useSignalR: GlobalConstants.USESIGNALR
};
