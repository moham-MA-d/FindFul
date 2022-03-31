import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;

  constructor() { }

  ngOnInit(): void {
    this.InitializeForm();
  }


  InitializeForm() {
    this.registerForm = new FormGroup({
      username: new FormControl('', Validators.required),
      email: new FormControl('', Validators.required),
      password: new FormControl('', [Validators.required, Validators.minLength(6), Validators.maxLength(22)]),
      confirmPassword: new FormControl('', [Validators.required, this.matchValues('password')]),
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



}
