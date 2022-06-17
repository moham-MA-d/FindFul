import { Component, OnInit } from '@angular/core';
import { SocialAuthService } from "@abacritt/angularx-social-login";
import { FacebookLoginProvider, GoogleLoginProvider } from "@abacritt/angularx-social-login";
import { SocialUser } from "@abacritt/angularx-social-login";
import { AccountService } from 'src/app/services/account/account.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-landing-register',
  templateUrl: './landing-register.component.html',
  styleUrls: ['./landing-register.component.css']
})
export class LandingRegisterComponent implements OnInit {

  user: SocialUser;
  loggedIn: boolean;
  constructor(private accountService: AccountService, private authService: SocialAuthService, private router: Router, private toastrService: ToastrService) {}

  refreshToken(): void {
    //this.authService.refreshAuthToken(GoogleLoginProvider.PROVIDER_ID);
  }

  ngOnInit() {
    this.authService.authState.subscribe((user) => {
      this.user = user;
      this.loggedIn = (user != null);
    });


  }


  // signInWithGoogle(): void {
  //   this.authService.signIn(GoogleLoginProvider.PROVIDER_ID);
  // }

  signInWithFB(): void {
    this.authService.signIn(FacebookLoginProvider.PROVIDER_ID).then(x => this.accountService.facebookAuth(x)
    .subscribe({
      next: (n) => { this.router.navigateByUrl('/home'); },
      error: (e) => { this.toastrService.error(e.error) },
      complete : () => {this.accountService.isLoggedIn = true;}
    })
    );
  }

  signOut(): void {
    this.authService.signOut();
  }


  aaa() {


    const fbLoginOptions = {
      scope: 'pages_messaging,pages_messaging_subscriptions,email,pages_show_list,manage_pages',
      return_scopes: true,
      enable_profile_selector: true
    }; // https://developers.facebook.com/docs/reference/javascript/FB.login/v2.11
    this.authService.signIn(FacebookLoginProvider.PROVIDER_ID, fbLoginOptions);

    const googleLoginOptions = {
      scope: 'profile email'
    }; // https://developers.google.com/api-client-library/javascript/reference/referencedocs#gapiauth2clientconfig

    const vkLoginOptions = {
      fields: 'photo_max,contacts', // Profile fields to return, see: https://vk.com/dev/objects/user
      version: '5.124', // https://vk.com/dev/versions
    }; // https://vk.com/dev/users.get

    let config = [
      {
        id: GoogleLoginProvider.PROVIDER_ID,
        provider: new GoogleLoginProvider("Google-OAuth-Client-Id", googleLoginOptions)
      },
      {
        id: FacebookLoginProvider.PROVIDER_ID,
        provider: new FacebookLoginProvider("541557987648669", fbLoginOptions)
      }
      // {
      //   id: VKLoginProvider.PROVIDER_ID,
      //   provider: new VKLoginProvider("VK-App-Id", vkLoginOptions)
      // },
    ];
  }

}
