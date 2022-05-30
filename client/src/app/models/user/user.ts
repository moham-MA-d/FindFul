import { UserEnums } from "src/app/enum/userEnums";

export class User {
  id: number;
  userName: string;
  photoUrl: string;
  gender: UserEnums.Gender;
  sex: UserEnums.Sex;
  roles: string[];
}

export class UserToken {
  token: string;
  success: string;
  refreshToken: string;
  errors: string[];
}
