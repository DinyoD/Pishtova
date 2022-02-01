import { Component, OnInit } from '@angular/core';
import { ProfileModel } from 'src/app/models/profile';
import { SubjectService, UserService } from 'src/app/services';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  public profile: ProfileModel|null = null

  constructor(
    private subjectService: SubjectService,
    private userService: UserService) { 
      
    if (this.subjectService.getCurrentSubject() != null) {
      this.subjectService.settingSubjectModel(null);
    } 
  }

  ngOnInit(): void {
    this.userService.getUserInfo().subscribe( profile => this.profile = profile);
  }

}
