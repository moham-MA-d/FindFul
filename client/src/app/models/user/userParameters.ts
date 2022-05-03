import { UserEnums } from "src/app/enum/userEnums";
import { PageParameters } from "../page/pageParameters";
import { User } from "./user";

export class UserParameters extends PageParameters {

  gender: UserEnums.Gender;
  sex: UserEnums.Sex;
  orderBy: UserEnums.OrderBy;
  username: string = "";

  minAgeSlider = 10;
  maxAgeSlider = 99;
  valueSlider = 18;

  constructor(user: User) {
    super();
    this.gender = UserEnums.Gender.All;
    this.orderBy = UserEnums.OrderBy.Newest;

    switch (user.sex)
    {
        case UserEnums.Sex.Female:
          this.sex = UserEnums.Sex.Male;
            break;
        case UserEnums.Sex.Male:
          this.sex = UserEnums.Sex.Female;
            break;
        default:
          this.sex = UserEnums.Sex.All;
            break;
    }
  }
}


