import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/services/account/account.service';
import { User, UserToken } from 'src/app/models/user/user';
import { take } from 'rxjs/operators';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private accountService: AccountService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let currentUser: User;
    let userToken: UserToken;
    // take(): with using take() method we don't need to unsubscribe, after the observable being completed
    // we will automatically unsubscribe from it.
    this.accountService.currentUserToken$.pipe(take(1)).subscribe(uToken => {
      userToken = uToken;
      this.accountService.currentUser$.pipe(take(1)).subscribe(u => {
        currentUser = u;
      })
    });
    if (currentUser) {
      // we want to clone `request` and add authentication header into it.
      // it will attach our token to every request when we are logged in
      request = request.clone(
        {
          setHeaders: {
            Authorization: `Bearer ${userToken.token}`
          }
        });
    }
    return next.handle(request);
  }
}
