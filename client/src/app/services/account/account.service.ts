import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { User, UserSocialToken, UserToken } from 'src/app/models/user/user';
import { ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { SocialUser } from '@abacritt/angularx-social-login';
import { OnlineService } from '../hub/Online.service';

// Default Angular services are singleton. When we injected them into a component and it's initialized
//    it's available until our app disposed off
// @Injectable: It means service can be injected in other services or components in the application.
// Srvices are used to make request to API
@Injectable({
  // it is a meta data
  // in older version of Angular services were declared in providers : [] in app.module.ts
  // providedIn: makes an service as singleton
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl;
  isLoggedIn = false;
  // ReplaySubject: special type of Observables that act as buffer to store data.
  // the number passed into the method is used for how many previous values we want to store.
  // it can be observe by other classes or components.
  // whenever sth subscribe to this then it's going to be notified if anything changes.
  private currentUserTokenSource = new ReplaySubject<UserToken>(1);
  currentUserToken$ = this.currentUserTokenSource.asObservable();

  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private onlineService: OnlineService) {}

  login(model: User) {
    return this.http.post(this.baseUrl + 'account/login', model)
    .pipe(map((response: UserToken) => {
      const user = response;
      if(user){
        this.setCurrentUser(user);
      }
    }))
    ;
  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((user: UserToken) => {
        if (user) {
          this.setCurrentUser(user);
        }
      })
    )
  }


  logout() {
    localStorage.removeItem('user');
    this.currentUserTokenSource.next(null);
    this.currentUserSource.next(null);
    this.isLoggedIn = false;

    this.onlineService.stopHubConnection();

    /////environment.memberCache.clear();
  }

  facebookAuth(model: SocialUser) {

    let newModel = new UserSocialToken();
    newModel.accessToken = model.authToken;

    return this.http.post(this.baseUrl + 'account/facebookAuth', newModel)
    .pipe(map((response: UserToken) => {
      console.log("RRR: ", response);
      const user = response;
      if(user){
        this.setCurrentUser(user);
      }
    }))
    ;
  }

  //set user to observable
  setCurrentUser(authResult: UserToken) {
    const user: User = this.getDecodedToken(authResult.token);
    const userRoles = user.roles;
    user.roles = [];
    Array.isArray(userRoles) ? user.roles = userRoles : user.roles.push(userRoles);
    this.isLoggedIn = true;
    localStorage.setItem('user', JSON.stringify(authResult));
    this.currentUserTokenSource.next(authResult);
    this.currentUserSource.next(user);
    this.onlineService.createHubConnection(authResult);

  }

  getDecodedToken(token: string) : User {
    let u = JSON.parse(atob(token.split('.')[1]));
    let usr = new User();
    usr.userName = u.UserName;
    usr.sex = u.Sex;
    usr.gender = u.Gender;
    usr.photoUrl = u.PhotoUrl;
    usr.roles = u.Roes;
    usr.id = u.Id;
    return usr;
  }


}
