import { UserEnums } from "src/app/enum/userEnums";

export class User {
    userName: string;
    token: string;
    photoUrl: string;
    gender: UserEnums.Gender;
    sex: UserEnums.Sex;
}
