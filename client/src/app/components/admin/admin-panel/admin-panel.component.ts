import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user/user';
import { AdminUserService } from 'src/app/services/admin/adminUser.service';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {

  users: Partial<User[]>;

  constructor(private adminUserService: AdminUserService) { }

  ngOnInit() {
    this.getUsersWithRoles();
  }

  getUsersWithRoles() {
    this.adminUserService.getUsersWithRoles().subscribe(u => this.users = u);
  }

}
