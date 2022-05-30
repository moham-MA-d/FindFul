import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccountService } from 'src/app/services/account/account.service';

@Injectable({
  providedIn: 'root'
})

// Guards authomatically subscribes to any observables.
export class AuthGuard implements CanActivate {

constructor(private accountService: AccountService, private toastService: ToastrService, private router: Router) {}

  canActivate(): Observable<boolean> {
    if (this.accountService.isLoggedIn) {

      return this.accountService.currentUserToken$.pipe(
        map(user => {
          if(user){

            if(this.router.url == '/') {
              this.router.navigated = true;
            }
            return true;
          }
          this.toastService.error("Access is denied!");
          this.router.navigateByUrl('/welcome');
          return false;
        })
        );
    }
    else{
      this.toastService.error("Access is denied!");
      this.router.navigateByUrl('/welcome');
      return new BehaviorSubject<boolean>(false);
    }

  }

}
