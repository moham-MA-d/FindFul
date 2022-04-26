import { Component, OnInit } from '@angular/core';
import { Pagination } from 'src/app/models/helper/pagination';
import { Post } from 'src/app/models/post/post';
import { PostParameters } from 'src/app/models/post/postParameters';
import { PostService } from 'src/app/services/post/post.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  postParameters!: PostParameters;
  posts: Post[] | null = [];
  pagination: Pagination = new Pagination();

  ngOnInit(): void {
    this.GetAllPosts();
  }

  constructor(private postService: PostService) {
    this.postParameters = new PostParameters();
  }

  GetAllPosts(){
    return this.postService.GetAllPosts(this.postParameters).subscribe(r => {
      this.posts = r.result;
      this.pagination = r.pagination;
    });
  }
}
