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
  ttt = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJFbWFpbCI6Ik5pY2suZnVyeWZjYkBnbWFpbC5jb20iLCJJZCI6IjIwMCIsIlVzZXJOYW1lIjoiTmljayIsIlNleCI6IjAiLCJHZW5kZXIiOiIwIiwiUGhvdG9VcmwiOiJodHRwczovL3Njb250ZW50LWFtczQtMS54eC5mYmNkbi5uZXQvdi90MS4zMDQ5Ny0xLzg0NjI4MjczXzE3NjE1OTgzMDI3Nzg1Nl85NzI2OTMzNjM5MjI4MjkzMTJfbi5qcGc_c3RwPWMxNS4wLjUwLjUwYV9jcDBfZHN0LWpwZ19wNTB4NTAmX25jX2NhdD0xJmNjYj0xLTcmX25jX3NpZD0xMmIzYmUmX25jX29oYz1EM2w3blhzWjNOZ0FYX1NBYi1zJl9uY19odD1zY29udGVudC1hbXM0LTEueHgmZWRtPUFQNGhMM0lFQUFBQSZvaD0wMF9BVDhWaXl4cXdGeWNwcDVUUVJrU01vTTM0cGl2c21IT2hQWlNWSmZyZFlNUmR3Jm9lPTYyQzQ3MjE5IiwianRpIjoiZTFkMGZkZjktMjg2MS00NzUxLWJlY2ItYzM1MjRhZDY0YWEzIiwibmJmIjoxNjU0Njk2MDA0LCJleHAiOjE2NTQ2OTYxMjQsImlhdCI6MTY1NDY5NjAwNH0.MZRvz57yjCmtkZyCDfGu3RX2LZ3KglnMM3ZzTFE73Ln7TrPyQ5_EgeWr2w0fBLOIObN6KSs9Bwvcoya7gSf7Kg";
  constructor(private accountService: AccountService, private authService: SocialAuthService, private router: Router, private toastrService: ToastrService) {}

  refreshToken(): void {
    this.authService.refreshAuthToken(GoogleLoginProvider.PROVIDER_ID);
  }

  ngOnInit() {
    this.authService.authState.subscribe((user) => {
      this.user = user;
      this.loggedIn = (user != null);
    });


    let u = JSON.parse(atob(this.ttt.replace("_","-").split('.')[1]));
    console.log("U:" ,u);
  }


  signInWithGoogle(): void {
    this.authService.signIn(GoogleLoginProvider.PROVIDER_ID);
  }

  signInWithFB(): void {
    this.authService.signIn(FacebookLoginProvider.PROVIDER_ID).then(x => this.accountService.facebookAuth(x)
    .subscribe({
      next: (n) => { console.log("nnn: ", n); this.router.navigateByUrl('/home'); },
      error: (e) => { console.log("errr : ", e);this.toastrService.error(e.error) },
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
