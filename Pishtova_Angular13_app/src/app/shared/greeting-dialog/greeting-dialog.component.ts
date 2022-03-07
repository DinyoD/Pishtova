import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-greeting-dialog',
  templateUrl: './greeting-dialog.component.html',
  styleUrls: ['../confirmation-dialog/confirmation-dialog.component.css']
})
export class GreetingDialogComponent {

  public percentage: number;

  constructor(public dialogRef: MatDialogRef<GreetingDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public value: number) {
    this.percentage = value;
  }

  public close = ():void => {
    this.dialogRef.close();
  }

}
