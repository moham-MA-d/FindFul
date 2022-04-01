import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { NavComponent } from '../components/nav/nav.component';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { TextInputComponent } from '../components/snippets/text-input/text-input.component';

@NgModule({
  declarations: [
    NavComponent,
    TextInputComponent
  ],
  imports: [
    CommonModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-center'
    }),
    RouterModule,
    ReactiveFormsModule
  ],
  exports:[
    ToastrModule,
    NavComponent,
    TextInputComponent
  ]
})
export class SharedModule { }
