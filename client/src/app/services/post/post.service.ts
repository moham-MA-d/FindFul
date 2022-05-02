import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Pagination } from 'src/app/models/helper/pagination';
import { Post } from 'src/app/models/post/post';
import { PostParameters } from 'src/app/models/post/postParameters';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  baseUrl = environment.apiUrl;
  pagination: Pagination = new Pagination();

  constructor(private http: HttpClient) {
   }

  sendPost(post: Post) {
    return this.http.post(this.baseUrl + 'Post/AddPost', post).pipe(
      map((r: any) => {
          console.log("Post: ", r);
      })
    )
  }

  GetAllPosts(postParameters: PostParameters) {

    let params = this.pagination.getPaginationHeaders(postParameters.pageIndex, postParameters.pageSize);
    params = this.getFilteredHeaders(params, postParameters);

    return this.pagination.getPaginationResult<Post[]>(this.baseUrl + 'Post/GetPosts', params, this.http);
  }

  AddLike(postId: number) {
    return this.http.post(this.baseUrl + 'like/' + postId , {});
  }

  AddDisLike(postId: number) {
    return this.http.post(this.baseUrl + 'dislike/' + postId , {});
  }

  private getFilteredHeaders(param:HttpParams, postParameters: PostParameters){

    param = param.append('orderBy', postParameters.orderBy.toString());

    return param;
  }
}

