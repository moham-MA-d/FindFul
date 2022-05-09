import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { User } from 'src/app/models/user/user';
import { ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';

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
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) {}

  login(model: User) {
    return this.http.post(this.baseUrl + 'account/login', model)
    .pipe(map((response: User) => {
      const user = response;
      if(user){
        this.setCurrentUser(user);
      }
    }))
    ;
  }

  register(model: any) {
    console.log("model R: ", model);
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((user: User) => {
        if (user) {
          this.setCurrentUser(user);
        }
      })
    )
  }


  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    this.isLoggedIn = false;
    environment.memberCache.clear();
  }

  //set user to observable
  setCurrentUser(user:User) {
    this.isLoggedIn = true;
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }


}
