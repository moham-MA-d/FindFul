import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { SharedModule } from './modules/shared.module';
import { SnippetComponentsModule } from './modules/snippet-components.module';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { ErrorInterceptor } from './Interceptors/errors/error.interceptor';
import { JwtInterceptor } from './Interceptors/jwt/jwt.interceptor';
import { DefaultModule } from './layouts/default/default.module';
import { FullWidthModule } from './layouts/fullWidth/fullWidth.module';


//Main and necessary Module of Angular project which is called in main.ts 
@NgModule({
  declarations: [
    AppComponent,
    NotFoundComponent,
    TestErrorsComponent    
  ],
  imports: [
    BrowserModule, //basic modulde to show our code in SPA.
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    SharedModule,
    SnippetComponentsModule,
    DefaultModule,
    FullWidthModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }