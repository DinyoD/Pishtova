import { Component, OnInit } from '@angular/core';
import { ProfileModel, SubjectsWithPointsByCategory } from 'src/app/models/profile';
import { SubjectService, UserService } from 'src/app/services';

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
    private userService: UserService) { 
      
    if (this.subjectService.getCurrentSubject() != null) {
      this.subjectService.settingSubjectModel(null);
    } 
  }

  ngOnInit(): void {
    this.userService.getUserInfo().subscribe( profile => this.profile = profile );

  }

  showDetail(sbj: SubjectsWithPointsByCategory): void{
    console.log(sbj.subjectCategories);
    this.showDetails= true;
    this.subjectDetails = sbj;
  }

  closeDetails(){
    this.subjectDetails = null;
  }
}
