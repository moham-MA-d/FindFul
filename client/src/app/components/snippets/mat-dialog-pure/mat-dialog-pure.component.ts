import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-mat-dialog-pure',
  templateUrl: './mat-dialog-pure.component.html',
  styleUrls: ['./mat-dialog-pure.component.css']
})
export class MatDialogPureComponent  {

  message: string;
  dialogMessage: string = "Are you sure?";
  confirmButtonText = "Yes";
  cancelButtonText = "Cancel";
  label: string;
  example: string;

  constructor(
    private dialogRef: MatDialogRef<MatDialogPureComponent>,
    @Inject(MAT_DIALOG_DATA) private data: any) {
    if (data) {
      this.dialogMessage = data.message || this.dialogMessage;
      if (data.buttonText) {
        this.confirmButtonText = data.buttonText.ok || this.confirmButtonText;
        this.cancelButtonText = data.buttonText.cancel || this.cancelButtonText;
        this.label = data.label;
        this.example = data.example;
        this.dialogMessage = data.dialogMessage;
      }
    }
  }

  onConfirmClick(): void {
    this.dialogRef.close(this.message);
  }

}
