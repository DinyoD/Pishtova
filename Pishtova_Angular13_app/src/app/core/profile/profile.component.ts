import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';

import { environment as env } from 'src/environments/environment';
import { EditProfileModel } from 'src/app/models/profile/editProfile';
import { ProfileModel, SubjectsWithPointsByCategory } from 'src/app/models/profile/profile';
import { AuthService, SubjectService, UserService } from 'src/app/services';
import { ConfirmationDialogModel } from 'src/app/shared/confirmation-dialog/confirmation-dialog';
import { ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';
import { UpdateProfileInfoDialogComponent } from 'src/app/shared/update-profile-info-dialog/update-profile-info-dialog.component';
import { UploadFileDialogComponent } from 'src/app/shared/upload-file-dialog/upload-file-dialog.component';
import { ChangeProfileEmailModel } from 'src/app/models/profile/changeProfileEmail';
import { ChangeProfileInfoModel } from 'src/app/models/profile/changeProfileInfo';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  public profile: ProfileModel|null = null;
  public showDetails: boolean = false;
  public subjectDetails: SubjectsWithPointsByCategory|null = null

  constructor(
    private subjectService: SubjectService,
    private userService: UserService,
    private authService: AuthService,
    private dialog: MatDialog,
    private router: Router,
    private cd: ChangeDetectorRef) { 
      
    if (this.subjectService.getCurrentSubject() != null) {
      this.subjectService.settingSubjectModel(null);
    } 
  }

  ngOnInit(): void {
    this.userService.getUserInfo().subscribe( profile => this.profile = profile );
  }

  showDetail(sbj: SubjectsWithPointsByCategory): void{
    this.showDetails= true;
    this.subjectDetails = sbj;
  }

  closeDetails(){
    this.subjectDetails = null;
  }

  updatePicture(){
    this.dialog.open(UploadFileDialogComponent);
  }

  updateInfo(){
    if (this.profile == null) {
      return;
    }

    const profileToEdit: EditProfileModel = {
      name: this.profile.name,
      email: this.profile.email,
      grade: this.profile.grade,
      school: this.profile.school
    }

    this.dialog
      .open(UpdateProfileInfoDialogComponent, {autoFocus: false, data: profileToEdit})
      .afterClosed()
      .subscribe( 
       (updatedProfile) => {
          if (updatedProfile == null) return;

          const infoChanged = profileToEdit.name != updatedProfile.name ||
                              profileToEdit.grade != updatedProfile.grade ||
                              profileToEdit.school.id != updatedProfile.schoolId;

          if (infoChanged) this.changeProfileInfo(updatedProfile.name, updatedProfile.grade, updatedProfile.schoolId);

          const emailChanged = profileToEdit.email.toUpperCase() != updatedProfile.email.toUpperCase();

          if (emailChanged) this.changeUserEmail(updatedProfile.email);           
        }, 
       () => {}, 
       () => this.router.navigate(['/profile']))
    ;
    
  }

  private changeUserEmail = (email: string): void =>  {
    const dialogData = new ConfirmationDialogModel(`Имейлът ви ще се промени на ${email}`,
      '*след промяната трябва да активирате код за потвърждение за да достъпите профила си');

    this.dialog
      .open(ConfirmationDialogComponent, { data: dialogData })
      .afterClosed()
      .subscribe(changeEmail => {
        if (changeEmail == false) {
          return;
        }
        const dataModel: ChangeProfileEmailModel = {
          email: email,
          clientURI: env.CLIENT_URI + `/auth/emailconfirmation`
        } 
        this.userService.updateUserEmail(dataModel).subscribe(() => {
          this.authService.logout();
        });
      });
  }

  private changeProfileInfo = (name: string, grade: number, schoolId: number): void =>  {

    const dataModel: ChangeProfileInfoModel = {
      name: name,
      grade: grade,
      schoolId: schoolId
    } 
    this.userService.updateUserInfo(dataModel)
    .subscribe();
  }
}
