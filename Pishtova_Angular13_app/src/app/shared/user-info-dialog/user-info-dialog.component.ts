import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserInfoModel } from 'src/app/models/user/userInfo';

@Component({
  selector: 'app-user-info-dialog',
  templateUrl: './user-info-dialog.component.html',
  styleUrls: ['./user-info-dialog.component.css']
})
export class UserInfoDialogComponent {

  public userInfo: UserInfoModel|null = null;

  constructor(public dialogRef: MatDialogRef<UserInfoDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: UserInfoModel) {
    this.userInfo = data;
   }

  public close = (): void => {
    this.dialogRef.close();
  }
  public badgeCount = (code: number): number => {
    const badgeModel =this.userInfo?.badges?.find(x => x.code == code);
    return  badgeModel == undefined 
                ? 0 
                : badgeModel.count;
  }

}
