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
import { UpdateEmailModel } from 'src/app/models/profile/updateEmail';
import { ProfileModel } from 'src/app/models/profile/profile';
import { SubjectInfo } from "src/app/models/subject/subjectInfo";
import { ConfirmationDialogModel } from 'src/app/shared/confirmation-dialog/confirmation-dialog';
import { BadgesCountModel } from 'src/app/models/badge/badgesCount';
import { HtmlHelper } from "../helpers/subjectHelper";
import { SubjectWithCategories } from 'src/app/models/subjectCategory/subjectWithcategories';
import { CurrentUserModel } from 'src/app/models/user/currentUser';
import { UpdatePictureModel } from 'src/app/models/profile/updatePicture';

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
  public subjectCategories: SubjectWithCategories|null = null;
  public badges: BadgesCountModel[]|null = null;
  public subjectsInfo: SubjectInfo[]|null = null;

  constructor(
    private subjectService: SubjectService,
    private userService: UserService,
    private authService: AuthService,
    private badgeService: BadgesService,
    private pointsService: PointsService,
    private membershipService: MembershipService,
    private dialog: MatDialog,
    private router: Router) { 

      this.subjectService.setSubject(null);
      this.currentUser = this.authService.getCurrentUser();
  }

  ngOnInit(): void {
    if(this.currentUser == null) return;
    this.userService.getUserProfile(this.currentUser.id).subscribe( profile => {
      this.profile = profile;
      this.profileSchoolId = profile.school.id;
      this.pointsService.getPointsBySubjects(profile.id).subscribe(res => this.subjectsInfo = res);
     });
     this.badgeService.getUserBadges(this.currentUser.id).subscribe(res => this.badges = res);
  }

  public badgeCount = (code: number): number => {
    const badgeModel =this.badges?.find(x => x.code == code);
    return  badgeModel == undefined 
                ? 0 
                : badgeModel.count;
  }

  public showSubjectDetails = (subjectId: string, subjectName: string, subjectPoints: number, subjectProblems: number): void => { 
    if (!this.profile) return;
    this.showDetails= true;
    this.pointsService.getPointsBySubjectCategories(this.profile?.id, subjectId)
      .subscribe( res => 
        this.subjectCategories = {
        id: subjectId,
        name: subjectName,
        points: subjectPoints,
        problems: subjectProblems,
        categories: res.map(x => {return {...x, percentage: Math.round(x.points / x.problems * 100)}})
      });
  }

  public closeSubjectDetails = (): void => {
    this.subjectCategories = null;
    this.showDetails = false;
  }

  public getSubjectCode(name: string|undefined): string {
    return HtmlHelper.getCodeBySubjectName(name);
  }

  public changePicture = (): void => {
    this.dialog.open(UploadFileDialogComponent).afterClosed().subscribe( imageUrl => this.updatePicture(imageUrl));
  }

  public changeInfo = (): void => {
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
            this.updateInfo(updatedProfile.name, updatedProfile.grade, updatedProfile.schoolId);
          };

          if(emailChanged)
          { 
            this.changeUserEmail(updatedProfile.email);
          }

        });
  }

  private updatePicture = (imageUrl: string|null) => {

    if(this.currentUser == null || imageUrl == null) return;

    const dataModel: UpdatePictureModel  ={
      id: this.currentUser.id,
      pictureUrl : imageUrl
    }
    this.userService.updatePictureUrl(dataModel).subscribe(() => this.reload())
  }

  private updateInfo = (name: string, grade: number, schoolId: number): void =>  {
    if(this.currentUser == null){
      return;
    }
    const dataModel: ChangeProfileInfoModel = {
      id: this.currentUser.id,
      name: name,
      grade: grade,
      schoolId: schoolId
    };

    this.userService.updateInfo(dataModel).subscribe( () => this.reload());
  }

  private changeUserEmail = (email: string): void =>  {
    this.confirmEmailChange(email).subscribe((confirmed: boolean) => {
        if (confirmed) this.updateEmail(email);
      });
  }

  private confirmEmailChange = (email: string): Observable<boolean> => {
    const dialogData = new ConfirmationDialogModel(`Променяте мейлът си на ${email}`,
    '*ще получите линк за потвърждение');

   const dialogObs =   this.dialog
    .open(ConfirmationDialogComponent, { data: dialogData })
    .afterClosed();

    return dialogObs;
  }

  private updateEmail = (email: string): void => {
    if(this.currentUser == null){
      return;
    }
    const dataModel: UpdateEmailModel = {
      id: this.currentUser.id,
      email: email,
      clientURI: env.CLIENT_URI + `/auth/emailconfirmation`
    };
    this.userService.updateEmail(dataModel)
    .subscribe(() => {
      this.authService.logout();
      this.router.navigate(['/']);
    });
  }

  private reload = () => this.router.navigate(['/']).then(() => this.router.navigate(['/profile']));

}
