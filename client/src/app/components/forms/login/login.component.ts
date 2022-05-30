import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserToken } from 'src/app/models/user/user';
import { AccountService } from 'src/app/services/account/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  model: any = {};
  loginForm: FormGroup;
  constructor(private accountService: AccountService, private router: Router, private toastrService: ToastrService, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.setCurrentUser()
    this.InitializeForm();
  }

  InitializeForm() {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(22)]],
    });
  }

  login() {
    this.accountService.login(this.loginForm.value)
    .subscribe({
      next: (n) => { this.router.navigateByUrl('/home'); },
      error: (e) => { this.toastrService.error(e.error) },
      complete : () => {this.accountService.isLoggedIn = true;}
    });
  }


  setCurrentUser() {
    var localUser = localStorage.getItem('user');
    if (localUser) {
      const user: UserToken = JSON.parse(localUser);
      this.accountService.setCurrentUser(user);
      this.accountService.isLoggedIn = true;
      }
  }
}
