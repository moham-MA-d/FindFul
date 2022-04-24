import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Post } from 'src/app/models/post/post';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  sendForm(post: Post) {
    return this.http.post(this.baseUrl + 'Home/AddPost', post).pipe(
      map((r: any) => {
          console.log("Post: ", r);
      })
    )
  }


}

