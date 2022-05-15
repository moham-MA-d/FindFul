import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { User } from 'src/app/models/user/user';
import { AdminUserService } from 'src/app/services/admin/adminUser.service';
import { MatDialogPureComponent } from '../../snippets/mat-dialog-pure/mat-dialog-pure.component';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {

  users: Partial<User[]>;


    constructor(private adminUserService: AdminUserService, private dialog: MatDialog) { }

  ngOnInit() {
    this.getUsersWithRoles();
  }

  getUsersWithRoles() {
    this.adminUserService.getUsersWithRoles().subscribe(u => this.users = u);
  }


  openDialog() {
    let dialogRef = this.dialog.open(MatDialogPureComponent, {
      data: {
        dialogMessage: 'Choose the roles',
        buttonText: {
          ok: 'Save',
          cancel: 'Cancel'
        },
        label: 'Leave a message',
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {

      }
    });
  }
}
