import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Member } from 'src/app/models/user/member';
import { environment } from 'src/environments/environment';
  

@Injectable({
  providedIn: 'root'
})

export class MemberService {
  
  baseUrl = environment.apiUrl;
  
  constructor(private http: HttpClient) {}

   getMembers() {
     return this.http.get<Member[]>(this.baseUrl + 'users' );
   }

   getMember(userName: string) {
    return this.http.get<Member>(this.baseUrl + 'users/' + userName);
  }
}
