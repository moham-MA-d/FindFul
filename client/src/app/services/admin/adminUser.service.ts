import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { User } from 'src/app/models/user/user';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdminUserService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getUsersWithRoles() {
    return this.http.get<Partial<User[]>>(this.baseUrl + 'adminmain/GetUsersWithRoles').pipe(map(response => {
      return response;
    }));
  }

  updateRoles(username: string, roles: string[]) {
    return this.http.post(this.baseUrl, 'adminmain/editroles/' + username + '?roles=' + roles, {});
  }
}
