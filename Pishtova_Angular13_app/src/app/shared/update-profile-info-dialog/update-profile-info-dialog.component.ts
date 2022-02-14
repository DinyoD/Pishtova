import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CustomValidators } from 'src/app/authentication/helpers/custom-validators';
import { EditProfileModel as EditedProfileModel } from 'src/app/models/editProfile';
import { MunicipalityModel } from 'src/app/models/municipality';
import { SchoolModel } from 'src/app/models/school';
import { TownModel } from 'src/app/models/town';
import { UserService } from 'src/app/services';
import { UpdateSchoolInfoDialogComponent } from '../update-school-info-dialog/update-school-info-dialog.component';

@Component({
  selector: 'app-update-profile-info-dialog',
  templateUrl: './update-profile-info-dialog.component.html',
  styleUrls: ['./update-profile-info-dialog.component.css','../upload-file-dialog/upload-file-dialog.component.css']
})
export class UpdateProfileInfoDialogComponent implements OnInit{

  public errorMessage: string = '';
  public showError: boolean = false;
  
  public municipalities?: Array<MunicipalityModel>;
  public towns?: Array<TownModel>;
  public schools?: Array<SchoolModel> | null;
  
  public editedProfile: EditedProfileModel|null = null;

  public form: FormGroup = new FormGroup({
    email: new FormControl(this.editedProfile?.email, [Validators.required, Validators.email]),
    grade: new FormControl(this.editedProfile?.grade, [Validators.required]),
    name: new FormControl(this.editedProfile?.name, [Validators.required]),
  },
    { validators: [
      CustomValidators.nameMatchingRegEx
    ] }
  );


  constructor(
    public dialogRef: MatDialogRef<UpdateProfileInfoDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: EditedProfileModel,
    private userService: UserService,
    private dialog: MatDialog) {
      this.editedProfile = data;
    }

  ngOnInit(): void {}
    


  public cancel  = (): void => {
    this.dialogRef.close(true);
  }

  public confirm = (): void => {

    this.dialogRef.close(true);
  }

  updateSchool(){
    this.dialog.open(UpdateSchoolInfoDialogComponent, {autoFocus: false})
  }

  get email(): FormControl {
    return this.form.get('email') as FormControl;
  }
  get name(): FormControl {
    return this.form.get('name') as FormControl;
  }
  get grade(): FormControl {
    return this.form.get('grade') as FormControl;
  }

}
