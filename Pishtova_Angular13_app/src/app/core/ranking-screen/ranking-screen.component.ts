import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserPointsForSubjectModel } from 'src/app/models/userPointBySubject';
import { AuthService, SubjectService } from 'src/app/services';

@Component({
  selector: 'app-ranking-screen',
  templateUrl: './ranking-screen.component.html',
  styleUrls: ['./ranking-screen.component.css']
})
export class RankingScreenComponent implements OnInit {

  //public subjectId: number | null = null;
  public userId: string|null = null;
  public users: UserPointsForSubjectModel[]|null = null;
  public logedUser: UserPointsForSubjectModel|null = null;
  public logedUserPlace: number|null = null;

  constructor(
    private subjectService: SubjectService,
    private actRoute: ActivatedRoute,
    private authService: AuthService,
    private router: Router
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
          .sort((a,b) => b.percentage - a.percentage);

        this.logedUser = this.calculatePercentageUsersProperty(x.usersPointsForSubject.filter(x => x.userId == this.userId)[0]);

        this.logedUserPlace = x.usersPointsForSubject
          .map(u => this.calculatePercentageUsersProperty(u))
          .sort((a,b) => b.percentage - a.percentage)
          .findIndex(u => u.userId == this.userId) + 1;
      });
    
  }
  private calculatePercentageUsersProperty = (user: UserPointsForSubjectModel): UserPointsForSubjectModel => {
      return {
        ...user,
        percentage: Math.round((user.points/user.problemsCount)*100)
      } 
  }
}