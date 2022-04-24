import { DateSelectionModelChange } from "@angular/material/datepicker";
import { Member } from "../user/member";

export class Post{
  body: string;
  createDateTime: Date;

  theAppUser: Member;
}
