import { Component, EventEmitter, Inject, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { User } from 'src/app/models/user/user';

@Component({
  selector: 'app-mat-roles-dialog',
  templateUrl: './mat-roles-dialog.component.html',
  styleUrls: ['./mat-roles-dialog.component.css']
})
export class MatRolesDialogComponent implements OnInit {

  @Input() UpdateSelectedRoles = new EventEmitter();
  showClassGrp: FormGroup;
  roles: any[];
  user: User;

  message: string;
  dialogMessage: string = "Are you sure?";
  confirmButtonText = "Yes";
  cancelButtonText = "Cancel";
  label: string;
  example: string;

  ngOnInit() {
    this.showClassGrp = new FormGroup({
      'Admin': new FormControl(false),
      'Moderator': new FormControl(false),
      'Member': new FormControl(false)
    });
  }

  constructor(
    private dialogRef: MatDialogRef<MatRolesDialogComponent>,
    @Inject(MAT_DIALOG_DATA) private data: any) {
    if (data) {
      this.roles = data.roles;
      this.dialogMessage = data.message || this.dialogMessage;
      if (data.buttonText) {
        this.confirmButtonText = data.buttonText.ok || this.confirmButtonText;
        this.cancelButtonText = data.buttonText.cancel || this.cancelButtonText;
        this.label = data.label;
        this.example = data.example;
      }
    }
  }

  onConfirmClick(): void {
    this.dialogRef.close(this.roles);
  }
}

