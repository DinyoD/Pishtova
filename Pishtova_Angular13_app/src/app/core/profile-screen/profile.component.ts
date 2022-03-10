import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

import { environment as env } from 'src/environments/environment';
import { UploadFileDialogComponent } from 'src/app/shared/upload-file-dialog/upload-file-dialog.component';
import { ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';
import { UpdateProfileInfoDialogComponent } from 'src/app/shared/update-profile-info-dialog/update-profile-info-dialog.component';
import { AuthService, SubjectService, UserService, BadgesService } from 'src/app/services';
import { EditProfileModel } from 'src/app/models/profile/editProfile';
import { ChangeProfileInfoModel } from 'src/app/models/profile/changeProfileInfo';
import { ChangeProfileEmailModel } from 'src/app/models/profile/changeProfileEmail';
import { SubjectCategoriesWithPercentageModel } from 'src/app/models/subjectCategory';
import { ProfileModel, SubjectsWithPointsByCategory } from 'src/app/models/profile/profile';
import { ConfirmationDialogModel } from 'src/app/shared/confirmation-dialog/confirmation-dialog';
import { BadgesModel } from 'src/app/models/badge';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  public profile: ProfileModel|null = null;
  public profileSchoolId: number|null = null; 
  public showDetails: boolean = false;
  public subjectDetails: SubjectCategoriesWithPercentageModel[]|null = null;
  public badges: BadgesModel[]|null = null;

  constructor(
    private subjectService: SubjectService,
    private userService: UserService,
    private authService: AuthService,
    private badgeService: BadgesService,
    private dialog: MatDialog,
    private router: Router) { 
      
    if (this.subjectService.getCurrentSubject() != null) {
      this.subjectService.settingSubjectModel(null);
    } 
  }

  ngOnInit(): void {
    this.userService.getUserInfo().subscribe( profile => {
      this.profile = profile;
      this.profileSchoolId = profile.school.id;
     });
     this.badgeService.getUserBadges().subscribe(res => this.badges = res.badges)
  }

  public showDetail = (sbj: SubjectsWithPointsByCategory): void => { 
    this.showDetails= true;
    this.subjectDetails = sbj.subjectCategories.map(c => {
      return {
        name: c.categoryName,
        percentage: Math.round(c.points / c.problemsCount * 100)
      }
    });
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
                              this.profileSchoolId != updatedProfile.schoolId;

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

  public badgeIsOwned = (code: number): boolean => {
    return  this.badges?.find(x => x.code == code) != undefined ? true : false;
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
