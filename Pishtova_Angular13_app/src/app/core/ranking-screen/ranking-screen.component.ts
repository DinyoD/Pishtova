import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserPointsForSubjectModel } from 'src/app/models/userPointBySubject';
import { AuthService, SubjectService } from 'src/app/services';

@Component({
  selector: 'app-ranking-screen',
  templateUrl: './ranking-screen.component.html',
  styleUrls: ['./ranking-screen.component.css']
})
export class RankingScreenComponent implements OnInit {

  public subjectId: number | null = null;
  public userid: string|null = null;
  public users: UserPointsForSubjectModel[]|null = null
  constructor(
    private subjectService: SubjectService,
    private actRoute: ActivatedRoute,
    private authService: AuthService) {
    if (this.actRoute.snapshot.paramMap.get('id') != null) {
        
      this.subjectId = Number(this.actRoute.snapshot.paramMap.get('id'));
    }
   }

  ngOnInit(): void {
    if (this.subjectId != null) {
      this.subjectService.getSubjectRanking(this.subjectId).subscribe(x => {
        this.users = x.usersPointsForSubject;
        console.log(x);
        
      });
      this.userid = this.authService.getUserId();     
    }
  }
}