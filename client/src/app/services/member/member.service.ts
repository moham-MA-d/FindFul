import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of, pipe } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { Pagination } from 'src/app/models/helper/pagination';
import { Member } from 'src/app/models/user/member';
import { User } from 'src/app/models/user/user';
import { UserParameters } from 'src/app/models/user/userParameters';
import { userPhoto } from 'src/app/models/user/userPhoto';
import { environment } from 'src/environments/environment';
import { AccountService } from '../account/account.service';


@Injectable({
  providedIn: 'root'
})

export class MemberService {

  baseUrl = environment.apiUrl;
  currentMember!: Member;
  members: Member[] = [];
  userPhotos: userPhoto[] = [];

  //Map() is used like a dictionary with key, value parameters and have set() and get() methods.
  //memberCache = environment.memberCache;
  memberCache: any;
  user: User;
  private _userParams: UserParameters;
  public get userParams(): UserParameters {
    return this._userParams;
  }
  public set userParams(value: UserParameters) {
    this._userParams = value;
  }

  constructor(private http: HttpClient, private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      this.user = user;
      this.userParams = new UserParameters(user);
    })
  }

  pagination: Pagination = new Pagination();

  getMembers(userParameters: UserParameters) {

    let memberCacheKey = Object.values(userParameters).join('-');
    let cacheResponse = this.memberCache.get(memberCacheKey);
    if (cacheResponse) {
      return of(cacheResponse);
    }

    let params = this.pagination.getPaginationHeaders(userParameters.pageIndex, userParameters.pageSize);
    params = this.getFilteredHeaders(params, userParameters);

    return this.pagination.getPaginationResult<Member[]>(this.baseUrl + 'users/GetAll', params, this.http)
      .pipe(map(response => {
        this.memberCache.set(memberCacheKey, response);
        return response;
      }))
  }

  getFollowing(userParameters: UserParameters) {
    let params = this.pagination.getPaginationHeaders(userParameters.pageIndex, userParameters.pageSize);
    params = this.getFilteredHeaders(params, userParameters);

    return this.pagination.getPaginationResult<Member[]>(this.baseUrl + 'follow/getfollowing', params, this.http);
  }

  getFollowers(userParameters: UserParameters) {
    let params = this.pagination.getPaginationHeaders(userParameters.pageIndex, userParameters.pageSize);
    params = this.getFilteredHeaders(params, userParameters);

    return this.pagination.getPaginationResult<Member[]>(this.baseUrl + 'follow/getfollowers', params, this.http);
  }

  getMember(userName: string) {

    const member = [...this.memberCache.values()]
      .reduce((arr, elm) => arr.concat(elm.result), [])
      .find((mem: Member) => mem.userName === userName);

    if (member) {
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

  follow(username: string) {
    return this.http.post(this.baseUrl + "Follow/FollowUser/" + username, {});
  }

  unFollow(username: string) {
    return this.http.post(this.baseUrl + "/Follow/UnFollowUser/" + username, {});
  }

  private getFilteredHeaders(param: HttpParams, userParamms: UserParameters) {

    param = param.append('minAge', userParamms.minAgeSlider.toString());
    param = param.append('maxAge', userParamms.maxAgeSlider.toString());
    param = param.append('sex', userParamms.sex.toString());
    param = param.append('gender', userParamms.gender.toString());
    param = param.append('orderBy', userParamms.orderBy.toString());
    if (userParamms.username !== "") {
      param = param.append('username', userParamms.username.toString());
    }

    return param;
  }


}
