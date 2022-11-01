import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { CurrentUserModel } from 'src/app/models/user/currentUser';
import { UserInfoModel } from 'src/app/models/user/userInfo';
import { UserPointsForSubjectModel } from 'src/app/models/user/userPointBySubject';
import { AuthService, BadgesService, StatsService, SubjectService, UserService } from 'src/app/services';
import { UserInfoDialogComponent } from 'src/app/shared/user-info-dialog/user-info-dialog.component';

@Component({
  selector: 'app-ranking-screen',
  templateUrl: './ranking-screen.component.html',
  styleUrls: ['./ranking-screen.component.css']
})
export class RankingScreenComponent implements OnInit {

  public currentUser: CurrentUserModel|null = null;
  public users: UserPointsForSubjectModel[]|null = null;
  public logedUser: UserPointsForSubjectModel|null = null;
  public logedUserPlace: number|null = null;
  public navigationName: string = 'Класиране';
  public navigationUrl: string = '';

  constructor(
    private subjectService: SubjectService,
    private actRoute: ActivatedRoute,
    private authService: AuthService,
    private userService: UserService,
    private badgesService: BadgesService,
    private statsService: StatsService,
    private router: Router,
    private dialog: MatDialog,
  ) {}
    
  ngOnInit(): void {
    const urlId = Number(this.actRoute.snapshot.paramMap.get('id'));
    if (isNaN(urlId)) {
      this.router.navigate(['/main']);
      return;
    }
    this.currentUser = this.authService.getCurrentUser();     
    this.statsService.getSubjectRanking(urlId)
      .subscribe(x => {
        // this.users = ([
        //   {userName: 'Д Димитров', userId: '66676649-6ec5-483c-82ea-36a1c0599e56', points: 300, problemsCount: 1620, percentage: 50},          
        //   {userName: 'Валентин Андреев', userId: '038340b2-567e-4f7f-8194-df1ea09d0bfa', points: 6, problemsCount: 20, percentage: 10},
        //   {userName: 'Д Димитров', userId: '66676649-6ec5-483c-82ea-36a1c0599e56', points: 709, problemsCount: 1620, percentage: 20},          
        //   {userName: 'Валентин Андреев', userId: '038340b2-567e-4f7f-8194-df1ea09d0bfa', points: 10, problemsCount: 20, percentage: 35},
        //   {userName: 'Д Димитров', userId: '66676649-6ec5-483c-82ea-36a1c0599e56', points: 400, problemsCount: 1620, percentage: 82},          
        //   {userName: 'Валентин Андреев', userId: '038340b2-567e-4f7f-8194-df1ea09d0bfa', points: 2, problemsCount: 20, percentage: 84},
        //   {userName: 'Д Димитров', userId: '66676649-6ec5-483c-82ea-36a1c0599e56', points: 500, problemsCount: 1620, percentage: 76},          
        //   {userName: 'Валентин Андреев', userId: '038340b2-567e-4f7f-8194-df1ea09d0bfa', points: 20, problemsCount: 20, percentage: 75},
        //   {userName: 'Д Димитров', userId: '66676649-6ec5-483c-82ea-36a1c0599e56', points: 379, problemsCount: 1620, percentage: 11},          
        //   {userName: 'Валентин Андреев', userId: '038340b2-567e-4f7f-8194-df1ea09d0bfa', points: 15, problemsCount: 20, percentage: 44},
        // ])
        console.log(x);
        
        this.users = x.map(u => this.calculatePercentageUsersProperty(u))
                      .sort((a,b) => b.percentage - a.percentage);

        this.logedUser = this.calculatePercentageUsersProperty(x.filter(x => x.userId == this.currentUser?.id)[0]);

        this.logedUserPlace = x.map(u => this.calculatePercentageUsersProperty(u))
                              .sort((a,b) => b.percentage - a.percentage)
                              .findIndex(u => u.userId == this.currentUser?.id) + 1;
      });
    
  }

  public getUserInfo = (userId: string):void => {
    this.userService.getUserInfo(userId).subscribe(user => {
      this.badgesService.getUserBadges(userId).subscribe(userBadges => {
        const userInfo: UserInfoModel = {...user, badges: userBadges};
        this.dialog.open(UserInfoDialogComponent,  {autoFocus: false, data: userInfo});
        console.log(userInfo);
        
      })
    });
  
  }

  private calculatePercentageUsersProperty = (user: UserPointsForSubjectModel): UserPointsForSubjectModel => {
      return {
        ...user,
        percentage: Math.round((user.points/user.problemsCount)*100)
      } 
  }
}