import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
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


  constructor(private http: HttpClient) { }

  getMembers() {
    if (this.members.length > 0)
      return of(this.members); // of() methid is used to return data as an obeservable
    return this.http.get<Member[]>(this.baseUrl + 'users').pipe(
      map(mems => {
        this.members = mems;
        return this.members;
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
