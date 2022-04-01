import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FullWidthComponent } from './fullWidth.component';
import { LandingRegisterComponent } from 'src/app/components/landings/landing-register/landing-register.component';
import { LoginComponent } from 'src/app/components/forms/login/login.component';
import { RegisterComponent } from 'src/app/components/forms/register/register.component';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/modules/shared.module';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule
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
  ]
})
export class FullWidthModule { }
