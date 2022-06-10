import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FullWidthComponent } from './fullWidth.component';
import { LandingRegisterComponent } from 'src/app/components/landings/landing-register/landing-register.component';
import { LoginComponent } from 'src/app/components/forms/login/login.component';
import { RegisterComponent } from 'src/app/components/forms/register/register.component';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/modules/shared.module';
import { SocialLoginModule, SocialAuthServiceConfig } from '@abacritt/angularx-social-login';
import { GoogleLoginProvider, FacebookLoginProvider } from '@abacritt/angularx-social-login';
@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    SocialLoginModule
  ],
  declarations: [
    FullWidthComponent,
    LandingRegisterComponent,
    LoginComponent,
    RegisterComponent,
  ],
  exports:[
    FullWidthComponent,
    LandingRegisterComponent,
    LoginComponent,
    RegisterComponent,
  ],
  providers: [
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(
              'clientId'
            )
          },
          {
            id: FacebookLoginProvider.PROVIDER_ID,
            provider: new FacebookLoginProvider('541557987648669')
          }
        ],
        onError: (err) => {
          console.error(err);
        }
      } as SocialAuthServiceConfig,
    }
  ],
})
export class FullWidthModule { }
