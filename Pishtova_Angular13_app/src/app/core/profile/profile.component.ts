import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { NavigationEnd, Router } from '@angular/router';

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
import { ProfileToUpdateModel } from 'src/app/models/profile/profileToUpdate';
import { Observable } from 'rxjs';

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
    private router: Router) { 
      
    if (this.subjectService.getCurrentSubject() != null) {
      this.subjectService.settingSubjectModel(null);
    } 
  }

  ngOnInit(): void {
    this.userService.getUserInfo().subscribe( profile => this.profile = profile );
  }

  public showDetail = (sbj: SubjectsWithPointsByCategory): void => { 
    this.showDetails= true;
    this.subjectDetails = sbj;
  }

  public closeDetails = (): void => {
    this.subjectDetails = null;
  }

  public updatePicture = (): void => {
    this.dialog.open(UploadFileDialogComponent).afterClosed().subscribe( res => this.reload(res));
  }

  public updateInfo = (): void => {
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

          const emailChanged = profileToEdit.email.toUpperCase() != updatedProfile.email.toUpperCase();

          if(infoChanged)
          {
            this.changeProfileInfo(updatedProfile.name, updatedProfile.grade, updatedProfile.schoolId);
          };

          if(emailChanged)
          { 
            this.changeUserEmail(updatedProfile.email);
          }

        });
  }

  private changeProfileInfo = (name: string, grade: number, schoolId: number): void =>  {
    
    const dataModel: ChangeProfileInfoModel = {
      name: name,
      grade: grade,
      schoolId: schoolId
    };

    this.userService.updateUserInfo(dataModel)
    .subscribe( () => this.reload(true));
  }

  private changeUserEmail = (email: string): void =>  {
    this.confirmEmailChange(email).subscribe((confirmed: boolean) => {
        if (confirmed) this.updateEmail(email);
      });
  }

  private confirmEmailChange = (email: string): Observable<boolean> => {
    const dialogData = new ConfirmationDialogModel(`Имейлът ви ще се промени на ${email}`,
    '*след промяната трябва го активирате за да достъпите профила си');

   const dialogObs =   this.dialog
    .open(ConfirmationDialogComponent, { data: dialogData })
    .afterClosed();

    return dialogObs;
  }

  private updateEmail = (email: string): void => {
    const dataModel: ChangeProfileEmailModel = {
      email: email,
      clientURI: env.CLIENT_URI + `/auth/emailconfirmation`
    };
    this.userService.updateUserEmail(dataModel)
    .subscribe(() => {
      this.authService.logout();
      this.router.navigate(['/']);
    });
  }

  private reload = (reload: boolean) => {
    if (reload){
      this.router.navigate(['/']).then(() => this.router.navigate(['/profile']));
    };
  } 
}
