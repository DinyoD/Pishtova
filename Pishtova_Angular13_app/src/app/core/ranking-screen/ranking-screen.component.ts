import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { CurrentUserModel } from 'src/app/models/user/currentUser';
import { UserInfoModel } from 'src/app/models/user/userInfo';
import { UserPointsForSubjectModel } from 'src/app/models/user/userPointBySubject';
import { BadgesService, StatsService, UserService } from 'src/app/services';
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
    private actRoute: ActivatedRoute,
    private userService: UserService,
    private badgesService: BadgesService,
    private statsService: StatsService,
    private router: Router,
    private dialog: MatDialog,
  ) {}
    
  ngOnInit(): void {
    const subjectId = this.actRoute.snapshot.paramMap.get('id');
    if (subjectId == null) {
      this.router.navigate(['/main']);
      return;
    }
    this.currentUser = this.userService.getCurrentUser();     
    this.statsService.getSubjectRanking(subjectId)
      .subscribe(x => {
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