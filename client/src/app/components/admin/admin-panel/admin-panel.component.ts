import { Component, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { Roles } from 'src/app/models/admin/roles';
import { User } from 'src/app/models/user/user';
import { AdminUserService } from 'src/app/services/admin/adminUser.service';
import { MatRolesDialogComponent } from '../../snippets/admin/mat-roles-dialog/mat-roles-dialog.component';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {

  users: Partial<User[]>;
  @Output() roles;

  constructor(private adminUserService: AdminUserService, private dialog: MatDialog, private toast: ToastrService) { }

  ngOnInit() {
    this.getUsersWithRoles();
  }

  getUsersWithRoles() {
    this.adminUserService.getUsersWithRoles().subscribe(u => this.users = u);
  }

  private getRolesArray(user: User) {
    const roles = [];
    const userRoles = user.roles;
    const avilableRoles = new Roles().roles;

    avilableRoles.forEach(role => {
      let isMatch = false;
      for (const userRole of userRoles) {
        if (role.name == userRole) {
          isMatch = true;
          role.checked = true;
          roles.push(role);
          break;
        }
      }
      if (!isMatch) {
        role.checked = false;
        roles.push(role);
      }
    });
    return roles;
  }

  openDialog(user: User) {
    let dialogRef = this.dialog.open(MatRolesDialogComponent, {
      data: {
        dialogMessage: 'Choose the roles',
        buttonText: {
          ok: 'Save',
          cancel: 'Cancel'
        },
        roles: this.getRolesArray(user),
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const selectedRoles = [...result.filter(x => x.checked == true).map(x => x.name)];
        if (selectedRoles) {
          this.adminUserService.updateRoles(user.userName, selectedRoles).subscribe((response: string[]) => {
            if (response) {
              user.roles = response;
              this.toast.success("Roled added for " + user.userName);
            }
          });
        }
      }
    });
  }
}
