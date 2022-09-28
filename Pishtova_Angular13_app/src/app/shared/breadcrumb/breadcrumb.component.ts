import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BreadCrumbElement } from 'src/app/models/breadcrumb/breadcrumbElement';
import { StorageService } from 'src/app/services';

@Component({
  selector: 'app-breadcrumb',
  templateUrl: './breadcrumb.component.html',
  styleUrls: ['./breadcrumb.component.css']
})
export class BreadcrumbComponent implements OnInit {
  
  public breadcrumb: BreadCrumbElement[]|[] = [];
  public showNavigation: boolean = false;

  constructor(
    private route: Router,
    private storageService: StorageService) { }

  ngOnInit(): void {
    console.log(this.route.url.split('/'));
    const urlPart = this.route.url.split('/');
    if (!urlPart.includes('subject')) {
      return;
    }
    this.breadcrumb = [
      {
        name: this.storageService.getItem('subjectName')||'', 
        url: '/subject/' + this.storageService.getItem('subjectId')||''
      }
    ];
    if (urlPart.length > 3) {
      const sbjArea = urlPart[3];
      let sbjAreaBg = '';
      switch (sbjArea) {
        case 'test':
          sbjAreaBg = 'Тест'
          break;
        case 'materials':
          sbjAreaBg = 'Материали'
          break;
        case 'ranking':
          sbjAreaBg = 'Класиране'
          break;
      }

      this.breadcrumb = [...this.breadcrumb, {name: sbjAreaBg, url: '/subject/' + this.storageService.getItem('subjectId') + '/' + sbjArea}]
    }
  }

}
