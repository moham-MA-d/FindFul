import { UserEnums } from "src/app/enum/userEnums";

export class User {
    id: number;
    userName: string;
    token: string;
    photoUrl: string;
    gender: UserEnums.Gender;
    sex: UserEnums.Sex;
    roles: string[];
}
