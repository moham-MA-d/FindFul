import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { PaginatedResult } from 'src/app/models/helper/pagination';
import { Member } from 'src/app/models/user/member';
import { UserParameters } from 'src/app/models/user/userParameters';
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

  constructor(private http: HttpClient) { }

  getMembers(userParameters: UserParameters) {

    let params = this.getPaginationHeaders(userParameters.pageIndex, userParameters.pageSize);
    params = this.getFilteredHeaders(params, userParameters);

    return this.getPaginationResult<Member[]>(this.baseUrl + 'users', params);
  }

  private getPaginationResult<T>(url: string, params: HttpParams) {
    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();

    // in normal usage of get() this will give us the response body
    // when we observing the response and use this to pass the params to it the we get the Full response back
    return this.http.get<T>(url, { observe: 'response', params }).pipe(
      map(response => {
        paginatedResult.result = response.body; // response.body = Member[]
        if (response.headers.get("Pagination") !== null) {
          paginatedResult.pagination = JSON.parse(response.headers.get("Pagination"));
        }
        return paginatedResult;
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

  private getPaginationHeaders(pageIndex: number, pageSize: number) {

    // HttpParams: gives us the ability to serialize the parameters and add them to query string.
    let params = new HttpParams();

    if (pageIndex !== null && pageSize !== null) {
      //query string:
      params = params.append('pageIndex', pageIndex.toString());
      params = params.append('pageSize', pageSize.toString());
    }
    return params;
  }

  private getFilteredHeaders(param:HttpParams, userParamms: UserParameters){

    param = param.append('minAge', userParamms.minAge.toString());
    param = param.append('maxAge', userParamms.maxAge.toString());
    param = param.append('sex', userParamms.sex.toString());
    param = param.append('gender', userParamms.gender.toString());
    param = param.append('orderBy', userParamms.orderBy.toString());

    return param;
  }

}
