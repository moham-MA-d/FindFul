import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/models/user/user';
import { AccountService } from 'src/app/services/account/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  model: any = {};

  constructor(private accountService: AccountService, private router: Router, private toastrService: ToastrService) { }

  ngOnInit(): void {
    this.setCurrentUser()
  }

  login() {
    this.accountService.login(this.model)
    .subscribe({
      next: (n) => { this.router.navigateByUrl('/home'); },
      error: (e) => { this.toastrService.error(e.error) },
      complete : () => {this.accountService.isLoggedIn = true;}
    });
  }

  
  setCurrentUser() {
    var localUser = localStorage.getItem('user');
    if (localUser) {
      const user: User = JSON.parse(localUser);
      this.accountService.setCurrentUser(user);
      this.accountService.isLoggedIn = true;
      }
  }
}
