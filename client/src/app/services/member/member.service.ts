import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { PaginatedResult } from 'src/app/models/helper/pagination';
import { Member } from 'src/app/models/user/member';
import { userPhoto } from 'src/app/models/user/userPhoto';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})

export class MemberService {

  baseUrl = environment.apiUrl;
  currentMember: Member;
  members: Member[] = [];
  userPhotos: userPhoto[] = [];
  paginatedResult: PaginatedResult<Member[]> = new PaginatedResult<Member[]>();

  constructor(private http: HttpClient) { }

  getMembers(pageNumber?: number, pageSize?: number) {

    // HttpParams: gives us the ability to serialize the parameters and add then to query string.
    let params = new HttpParams();

    if (pageNumber !== null && pageSize !== null) {
      //query string:
      params = params.append('pageNumber', pageNumber.toString());
      params = params.append('pageSize', pageSize.toString());
    }
    // in normal usage of get() this will give us the response body
    // when we observing the response and use this to pass the params to it the we get the Full response back
    return this.http.get<Member[]>(this.baseUrl + 'users', {observe: 'response', params}).pipe(
      map(response => {
        this.paginatedResult.result = response.body; // response.body = Member[]
        if (response.headers.get("Pagination") !== null) {
          this.paginatedResult.pagination = JSON.parse(response.headers.get("Pagination"));
        }
        return this.paginatedResult;
      })
    );
  }

  getMember(userName: string) {
    const member = this.members.find(x => x.userName = userName);
    if (member !== undefined) {
      return of(member);
    }
    if (this.currentMember !== undefined) {
      return of(this.currentMember);
    }
      return this.http.get<Member>(this.baseUrl + 'users/GetUser/' + userName);

  }

  updateMember(member: Member) {
    return this.http.put(this.baseUrl + 'users', member).pipe(
      map(() => {
        const index = this.members.indexOf(member);
        this.members[index] = member;
      })
    );
  }

  getMemberPhotos(userName: string) {
    return this.http.get<userPhoto[]>(this.baseUrl + 'userAlbum/GetUserPhotos/' + userName).pipe(
      map(photos => {
        this.userPhotos = photos;
        return this.userPhotos;
      })
    );
  }

  deleteAlbumPhoto(photoId: number) {
    return this.http.delete(this.baseUrl + 'UserAlbum/DeleteAlbumPhoto/' + photoId);
  }

}
