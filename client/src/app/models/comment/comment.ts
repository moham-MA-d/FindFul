import { Post } from "../post/post";
import { Member } from "../user/member";

export class Comment {

  text: String;
  CreateDateTime: Date;

  theAppUser: Member;
  thePost: Post;
}
