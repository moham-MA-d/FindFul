import { DateSelectionModelChange } from "@angular/material/datepicker";
import { Member } from "../user/member";

export class Post{
  id: number;
  body: string;
  isLiked: boolean;
  createDateTime: Date;
  likesCount: number;
  theUser: Member;
}
