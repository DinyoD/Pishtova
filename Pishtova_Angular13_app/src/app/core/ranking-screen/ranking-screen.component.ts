import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { UserBadgesModel } from 'src/app/models/user/userBadges';
import { UserInfoModel } from 'src/app/models/user/userInfo';
import { UserPointsForSubjectModel } from 'src/app/models/user/userPointBySubject';
import { AuthService, BadgesService, SubjectService, UserService } from 'src/app/services';
import { UserInfoDialogComponent } from 'src/app/shared/user-info-dialog/user-info-dialog.component';

@Component({
  selector: 'app-ranking-screen',
  templateUrl: './ranking-screen.component.html',
  styleUrls: ['./ranking-screen.component.css']
})
export class RankingScreenComponent implements OnInit {

  public userId: string|null = null;
  public users: UserPointsForSubjectModel[]|null = null;
  public logedUser: UserPointsForSubjectModel|null = null;
  public logedUserPlace: number|null = null;

  constructor(
    private subjectService: SubjectService,
    private actRoute: ActivatedRoute,
    private authService: AuthService,
    private userService: UserService,
    private badgesService: BadgesService,
    private router: Router,
    private dialog: MatDialog,
  ) {}
    
  ngOnInit(): void {
    const urlId = Number(this.actRoute.snapshot.paramMap.get('id'));
    if (isNaN(urlId)) {
      this.router.navigate(['/main']);
      return;
    }
    this.userId = this.authService.getUserId();     
    this.subjectService.getSubjectRanking(urlId)
      .subscribe(x => {
        this.users = x.usersPointsForSubject
          .map(u => this.calculatePercentageUsersProperty(u))
          .sort((a,b) => b.points - a.points);

        this.logedUser = this.calculatePercentageUsersProperty(x.usersPointsForSubject.filter(x => x.userId == this.userId)[0]);

        this.logedUserPlace = x.usersPointsForSubject
          .map(u => this.calculatePercentageUsersProperty(u))
          .sort((a,b) => b.points - a.points)
          .findIndex(u => u.userId == this.userId) + 1;
      });
    
  }

  public getUserInfo = (userId: string):void => {
    this.userService.getUserInfo(userId).subscribe(user => {
      this.badgesService.getUserBadges(userId).subscribe(userBadges => {
        const userInfo: UserInfoModel = {...user, badges: userBadges.badges};
        this.dialog.open(UserInfoDialogComponent,  {autoFocus: false, data: userInfo})
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