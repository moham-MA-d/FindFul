import { Component, Input, OnInit } from '@angular/core';
import { post } from 'jquery';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { Post } from 'src/app/models/post/post';
import { User } from 'src/app/models/user/user';
import { AccountService } from 'src/app/services/account/account.service';
import { PostService } from 'src/app/services/post/post.service';

@Component({
  selector: 'app-post-content',
  templateUrl: './post-content.component.html',
  styleUrls: ['./post-content.component.css']
})
export class PostContentComponent implements OnInit {

  @Input() posts: Post;

  user: User;
  currentUser$!: Observable<User>;
  textColor: string = '';
  faBeat: string = 'text-gray';

  constructor(public accountService: AccountService, private postService: PostService) {
    accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;
  }


  AddLike(post: Post) {
    this.postService.AddLike(post.id).subscribe((r: any) => {
      if (r.data == "like") {
        post.isLiked = true;
        ++post.likesCount;
      } else {
        post.isLiked = false;
        --post.likesCount;
      }
    })
  }

}
