import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminMainComponent } from './adminMain.component';
import { AdminPanelComponent } from 'src/app/components/admin/admin-panel/admin-panel.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatRolesDialogComponent } from 'src/app/components/snippets/admin/mat-roles-dialog/mat-roles-dialog.component';
import { MaterialModule } from '../../material/materialModule/material.module';


@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MaterialModule
  ],
  declarations: [
    AdminMainComponent,
    AdminPanelComponent,
    MatRolesDialogComponent
  ],
  exports: [
    AdminPanelComponent,
    MatRolesDialogComponent
  ]
})
export class AdminMainModule { }
