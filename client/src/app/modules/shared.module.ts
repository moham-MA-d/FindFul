import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { NavComponent } from '../components/nav/nav.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    NavComponent,
  ],
  imports: [
    CommonModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-center'
    }),
    RouterModule,
  ],
  exports:[
    ToastrModule,
    NavComponent,
  ]
})
export class SharedModule { }
