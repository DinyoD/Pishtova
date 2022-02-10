import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ProfileModel, SubjectsWithPointsByCategory } from 'src/app/models/profile';
import { SubjectService, UserService } from 'src/app/services';
import { UploadFileDialogComponent } from 'src/app/shared/upload-file-dialog/upload-file-dialog.component';

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
    private userService: UserService, 
    private dialog: MatDialog,) { 
      
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

  updatePicture(){
    this.dialog.open(UploadFileDialogComponent);
  }
}
