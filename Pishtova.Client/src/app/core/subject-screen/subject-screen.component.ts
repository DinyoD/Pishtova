import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SubjectModel } from 'src/app/models/subject';
import { SubjectService} from '../../services';

@Component({
  selector: 'app-subject-screen',
  templateUrl: './subject-screen.component.html',
  styleUrls: ['./subject-screen.component.css', '../main-screen/main-screen.component.css']
})
export class SubjectScreenComponent implements OnInit {
  public subject: SubjectModel|null = null;

  constructor(
    private actRoute: ActivatedRoute,
    private subjectService: SubjectService,
    private router: Router) { }

  ngOnInit(): void {
    this.subjectService.getSubjectById(this.actRoute.snapshot.params.id)
    .subscribe(s => this.subject = s, () => this.router.navigate(['main']));
  }

}
