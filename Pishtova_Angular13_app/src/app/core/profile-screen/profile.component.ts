import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

import { environment as env } from 'src/environments/environment';
import { UploadFileDialogComponent } from 'src/app/shared/upload-file-dialog/upload-file-dialog.component';
import { ConfirmationDialogComponent } from 'src/app/shared/confirmation-dialog/confirmation-dialog.component';
import { UpdateProfileInfoDialogComponent } from 'src/app/shared/update-profile-info-dialog/update-profile-info-dialog.component';
import { AuthService, SubjectService, UserService, BadgesService, PointsService, MembershipService } from 'src/app/services';
import { EditProfileModel } from 'src/app/models/profile/editProfile';
import { ChangeProfileInfoModel } from 'src/app/models/profile/changeProfileInfo';
import { ChangeProfileEmailModel } from 'src/app/models/profile/changeProfileEmail';
import { ProfileModel } from 'src/app/models/profile/profile';
import { ProfilePointsBySubjectModel } from "src/app/models/profile/profilePointsBySubject";
import { ConfirmationDialogModel } from 'src/app/shared/confirmation-dialog/confirmation-dialog';
import { BadgesCountModel } from 'src/app/models/badge/badgesCount';
import { CategoryWithPointsModel } from 'src/app/models/subjectCategory/categoryPoints';
import { CurrentUserModel } from 'src/app/models/user/currentUser';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  public currentUser: CurrentUserModel|null = null;
  public profile: ProfileModel|null = null;
  public profileSchoolId: number|null = null; 
  public showDetails: boolean = false;
  public subjectCategories: CategoryWithPointsModel[]|null = null;
  public badges: BadgesCountModel[]|null = null;
  public pointsBySubjects: ProfilePointsBySubjectModel[]|null = null;

  constructor(
    private subjectService: SubjectService,
    private userService: UserService,
    private authService: AuthService,
    private badgeService: BadgesService,
    private pointsService: PointsService,
    private membershipService: MembershipService,
    private dialog: MatDialog,
    private router: Router) { 
      
    if (this.subjectService.getCurrentSubject() != null) {
      this.subjectService.settingSubjectModel(null);
    } 
  }

  ngOnInit(): void {
    this.userService.getUserProfile().subscribe( profile => {
      this.profile = profile;
      this.profileSchoolId = profile.school.id;
     });
     this.pointsService.getPointsBySubjects().subscribe(res => this.pointsBySubjects = res);
     this.badgeService.getProfileBadges().subscribe(res => this.badges = res.badges);
     this.currentUser = this.authService.getCurrentUser();
  }

  public showSubjectDetails = (subjectId: number): void => { 
    this.showDetails= true;
    this.pointsService.getPointsBySubjectCategories(subjectId)
      .subscribe( res => this.subjectCategories = res.map(x => {return {...x, percentage: Math.round(x.points / x.problems * 100)}}));
  }

  public closeSubjectDetails = (): void => {
    this.subjectCategories = null;
    this.showDetails = false;
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

  public badgeCount = (code: number): number => {
    const badgeModel =this.badges?.find(x => x.code == code);
    return  badgeModel == undefined 
                ? 0 
                : badgeModel.count;
  }

  public badgeIsOwned = (code: number): boolean => { return this.badgeCount(code) > 0}

  public goToBillingPortal():void {
    this.membershipService.redirectToCustomerPortal().subscribe((data) => { window.location.href = data.url });
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
    const dialogData = new ConfirmationDialogModel(`?????????????? ???? ???? ???? ?????????????? ???? ${email}`,
    '*???????? ?????????????????? ???????????? ???? ???????????????????? ???? ???? ?????????????????? ?????????????? ????');

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
