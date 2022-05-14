import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminMainComponent } from './adminMain.component';
import { AdminPanelComponent } from 'src/app/components/admin/admin-panel/admin-panel.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    AdminMainComponent,
    AdminPanelComponent,
  ],
  exports: [
    AdminPanelComponent
  ]
})
export class AdminMainModule { }
