import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/services/account/account.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;
  baseUrl = environment.apiUrl;

  constructor(private fb: FormBuilder, private accountService: AccountService, private router: Router, private toastrService: ToastrService) { }

  ngOnInit(): void {
    this.InitializeForm();
  }

  InitializeForm() {
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(22)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]],
    });
    this.registerForm.controls.password.valueChanges.subscribe(() => {
      this.registerForm.controls.confirmPassword.updateValueAndValidity();
    })
  }

  //ValidatorFn : returns a map of validation errors if present, otherwise null.
  //AbstractControl: All form controls drive from AbstractControl
  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.controls[matchTo].value ? null : {isMatching: true}
    }
  }

  register() {
    return this.accountService.register(this.registerForm.value).subscribe({
      next: (n) => { this.router.navigateByUrl('/home'); },
      error: (e) => { this.toastrService.error(e.error) },
      complete : () => {this.accountService.isLoggedIn = true;}
    });
  }

}
