import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PoemDetailsModel } from 'src/app/models/poem/poemDetails';
import { PoemsService } from 'src/app/services/poems/poems.service';

@Component({
  selector: 'app-poem-details-screen',
  templateUrl: './poem-details-screen.component.html',
  styleUrls: ['./poem-details-screen.component.css']
})
export class PoemDetailsScreenComponent implements OnInit {
  
  public poem : PoemDetailsModel|null = null;

  constructor(
    private actRoute: ActivatedRoute,
    private router: Router,
    private poemsService: PoemsService
  ) { }

  ngOnInit(): void {
    const poemId = this.actRoute.snapshot.paramMap.get('id');
    if(poemId == null) {
      this.router.navigate(['/']);
      return;
    };
    this.poemsService.getPoem(poemId).subscribe(poem => this.poem = poem)
  }

}
