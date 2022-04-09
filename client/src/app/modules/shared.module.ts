import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { NavComponent } from '../components/nav/nav.component';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { TextInputComponent } from '../components/snippets/text-input/text-input.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {MatAutocompleteModule} from '@angular/material/autocomplete';

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
    ReactiveFormsModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatFormFieldModule,
    MatInputModule,
    MatAutocompleteModule
  ],
  exports:[
    ToastrModule,
    NavComponent,
    TextInputComponent
  ]
})
export class SharedModule { }
