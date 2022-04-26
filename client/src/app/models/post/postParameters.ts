import { PostEnums } from "src/app/enum/postEnums";
import { PageParameters } from "../page/pageParameters";

export class PostParameters extends PageParameters {
  orderBy: PostEnums.OrderBy;

  constructor() {
    super();
    this.orderBy = PostEnums.OrderBy.Newest;
  }
}
