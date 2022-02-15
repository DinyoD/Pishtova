import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { EditProfileModel } from 'src/app/models/profile/editProfile';
import { ProfileToUpdateModel } from 'src/app/models/profile/profileToUpdate';
import { CustomValidators } from 'src/app/authentication/helpers/custom-validators';
import { UpdateSchoolInfoDialogComponent } from 'src/app/shared/update-school-info-dialog/update-school-info-dialog.component';

@Component({
  selector: 'app-update-profile-info-dialog',
  templateUrl: './update-profile-info-dialog.component.html',
  styleUrls: ['./update-profile-info-dialog.component.css','../upload-file-dialog/upload-file-dialog.component.css']
})
export class UpdateProfileInfoDialogComponent implements OnInit{

  public editedProfile: EditProfileModel;
  public profileToUpdate: ProfileToUpdateModel|null = null;
  public haveChanges: boolean = false;

  public form: FormGroup;


  constructor(
    private dialog: MatDialog,
    @Inject(MAT_DIALOG_DATA) public data: EditProfileModel,
    public dialogRef: MatDialogRef<UpdateProfileInfoDialogComponent>,
    private cdr: ChangeDetectorRef) {

      this.editedProfile = data;
      this.form = new FormGroup({
        name: new FormControl(this.editedProfile.name, [Validators.required]),
        email: new FormControl(this.editedProfile.email, [Validators.required, Validators.email]),
        grade: new FormControl(this.editedProfile.grade, [Validators.required]),
      },
        { validators: [
          CustomValidators.nameMatchingRegEx,
          CustomValidators.gradeMatching
        ] }
      );
    }

  ngOnInit(): void {}
  
  public cancel  = (): void => {
    this.dialogRef.close(false);
  }

  public confirm = (): void => {
    var result: ProfileToUpdateModel|null = this.getProfileToUpdate();
    this.dialogRef.close(result)
  }

  public updateSchool = (): void => {
    this.dialog.open(UpdateSchoolInfoDialogComponent, {autoFocus: false}).afterClosed().subscribe( res => {
      if(res && res.id != this.editedProfile.school.id ){
        this.editedProfile.school = {
          name: res.name,
          id: res.id
        }
        this.haveChanges = true;
      }
    });
  }

  get email(): FormControl {
    return this.form?.get('email') as FormControl;
  }
  get name(): FormControl {
    return this.form?.get('name') as FormControl;
  }
  get grade(): FormControl {
    return this.form?.get('grade') as FormControl;
  }

  
  private getProfileToUpdate = (): ProfileToUpdateModel|null => {
    this.haveChanges = this.email.value != this.editedProfile.email ||
      this.name.value != this.editedProfile.name ||
      this.grade.value != this.editedProfile.grade ||
      this.haveChanges == true ? true : false;

    if (this.haveChanges) {
      this.profileToUpdate = {
        name: this.name.value,
        email: this.email.value,
        grade: this.grade.value,
        schoolId: this.editedProfile.school.id
      };
    }
    console.log(this.haveChanges);
    
    return this.profileToUpdate;
  }
}
