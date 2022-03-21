import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserInfoModel } from 'src/app/models/user/userInfo';

@Component({
  selector: 'app-user-info-dialog',
  templateUrl: './user-info-dialog.component.html',
  styleUrls: ['./user-info-dialog.component.css']
})
export class UserInfoDialogComponent implements OnInit {

  public userInfo: UserInfoModel|null = null;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: UserInfoModel,
    public dialogRef: MatDialogRef<UserInfoDialogComponent>
  ) { }

  ngOnInit(): void {
  }

  public close = (): void => {
    this.dialogRef.close();
  }
}
