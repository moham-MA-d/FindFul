import { HttpClient, HttpHeaders } from '@angular/common/http';
import { jitOnlyGuardedExpression } from '@angular/compiler/src/render3/util';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { Member } from 'src/app/models/user/member';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})

export class MemberService {

  baseUrl = environment.apiUrl;
  members: Member[] = [];

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
    if(member !== undefined) return of(member);
    return this.http.get<Member>(this.baseUrl + 'users/' + userName);
  }

  updateMember(member: Member) {
    return this.http.put(this.baseUrl + 'users', member).pipe(
      map(() => {
        const index = this.members.indexOf(member);
        this.members[index] = member;
      })
    );
  }
}
