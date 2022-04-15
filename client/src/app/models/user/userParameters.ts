import { UserEnums } from "src/app/enum/userEnums";
import { User } from "./user";

export class UserParameters {

  gender: UserEnums.Gender;
  sex: UserEnums.Sex;
  minAge = 10;
  maxAge = 99;
  pageIndex = 0;
  pageSize = 5;


  constructor(user: User) {

    this.gender = user.gender;
    switch (user.sex)
    {
        case UserEnums.Sex.Female:
          this.sex = UserEnums.Sex.Male;
            break;
        case UserEnums.Sex.Male:
          this.sex = UserEnums.Sex.Female;
            break;
        default:
          this.sex = UserEnums.Sex.None;
            break;
    }
  }
}


