import { UserEnums } from "src/app/enum/userEnums";
import { userPhoto } from "./userPhoto";

export class Member {

  constructor ()
  {
    this.sex = UserEnums.Sex.Male;
  }
  guId: string;
  userName: string;
  email: string;
  phone: string;
  firstName: string;
  lastName: string;
  profilePhotoUrl: string;
  coverPhotoUrls: string;
  sex: UserEnums.Sex;
  gender: number;
  isActive: boolean;
  info: string;
  city: string;
  country: string;
  createDateTime: Date;
  age: number;
  lastActivity: string;
  dateOfBirth: Date;
  theUserPhotos: userPhoto[];

}


