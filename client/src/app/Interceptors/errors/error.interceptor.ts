import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toast: ToastrService) { }

  // interceptors are going to be initialized when we start the application because they are part of app module 
  //    and we add them the Providers and they're always going to be around until we close our application
  // we can intercept the request that goes out
  // next: is response that is backed
  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(error => {
        if (error) {
          switch (error.status) {
            case 400:
              if (error.error.errors) {
                const modalStateErrors = [];
                for (const key in error.error.errors) {
                  if (error.error.errors[key]) {
                    modalStateErrors.push(error.error.errors[key]);
                  }
                }
                throw modalStateErrors;
              } else {
                this.toast.error(error.statusText, error.status);
              }
              break;
              
              case 401:
                this.toast.error(error.statusText, error.status);
                break;

                case 404:
                  this.router.navigateByUrl('/not-found');
                  break;
                  
                case 500:
                  const navigationExtras: NavigationExtras = {state: {error: error.error}}
                  this.router.navigateByUrl('/server-error', navigationExtras);
                  break;

            default:
              this.toast.error("Something went wrong!");
              console.log(error);
              break;
          }
        }
        return throwError(error);
      })
    );
  }
}
