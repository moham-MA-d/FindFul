import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { Pagination } from 'src/app/models/helper/pagination';
import { Member } from 'src/app/models/user/member';
import { UserParameters } from 'src/app/models/user/userParameters';
import { userPhoto } from 'src/app/models/user/userPhoto';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})

export class MemberService {

  baseUrl = environment.apiUrl;
  currentMember!: Member;
  members: Member[] = [];
  userPhotos: userPhoto[] = [];


  constructor(private http: HttpClient) {
  }

  pagination: Pagination = new Pagination();

  getMembers(userParameters: UserParameters) {
    console.log("userParameters : ", userParameters);
    let params = this.pagination.getPaginationHeaders(userParameters.pageIndex, userParameters.pageSize);
    params = this.getFilteredHeaders(params, userParameters);

    return this.pagination.getPaginationResult<Member[]>(this.baseUrl + 'users', params, this.http);
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

  private getFilteredHeaders(param:HttpParams, userParamms: UserParameters){

    param = param.append('minAge', userParamms.minAge.toString());
    param = param.append('maxAge', userParamms.maxAge.toString());
    param = param.append('sex', userParamms.sex.toString());
    param = param.append('gender', userParamms.gender.toString());
    param = param.append('orderBy', userParamms.orderBy.toString());

    return param;
  }

}
