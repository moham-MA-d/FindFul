import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { ErrorInterceptor } from './Interceptors/errors/error.interceptor';
import { JwtInterceptor } from './Interceptors/jwt/jwt.interceptor';
import { DefaultModule } from './layouts/default/default.module';
import { FullWidthModule } from './layouts/fullWidth/fullWidth.module';
import { NgxSpinnerModule } from 'ngx-spinner';
import { LoadingInterceptor } from './Interceptors/loading.interceptor';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { HasRoleDirective } from './directives/role/has-role.directive';


//Main and necessary Module of Angular project which is called in main.ts
@NgModule({
  declarations: [
    AppComponent,
    NotFoundComponent,
    TestErrorsComponent,
    HasRoleDirective
  ],
  imports: [
    BrowserModule, //basic modulde to show our code in SPA.
    AppRoutingModule,
    HttpClientModule, // Handle HTTPS
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    DefaultModule,
    FullWidthModule,
    NgxSpinnerModule,
    StoreModule.forRoot({}, {}),
    EffectsModule.forRoot([])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
