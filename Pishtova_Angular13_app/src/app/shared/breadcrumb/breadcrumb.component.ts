import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BreadCrumbElement } from 'src/app/models/breadcrumb/breadcrumbElement';
import { StorageService } from 'src/app/services';
import { Storage } from 'src/app/utilities/constants/storage';

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
    if (!urlPart.includes('subjects')) {
      return;
    }
    this.breadcrumb = [
      {
        name: this.storageService.getItem(Storage.SUBJECT_NAME)||'', 
        url: '/subjects/' + this.storageService.getItem(Storage.SUBJECT_ID)||''
      }
    ];
    if (urlPart.length > 3) {
      const sbjArea = urlPart[3];
      let sbjAreaBg = '';
      switch (sbjArea) {
        case 'test':
          sbjAreaBg = 'Тест'
          break;
        case 'themes':
          sbjAreaBg = 'Теми'
          break;
        case 'ranking':
          sbjAreaBg = 'Класиране'
          break;
        case 'result':
          sbjAreaBg = 'Тест резултат'
          break;
      }

      this.breadcrumb = [...this.breadcrumb, {name: sbjAreaBg, url: '/subjects/' + this.storageService.getItem(Storage.SUBJECT_ID) + '/' + sbjArea}]
    }

    if(urlPart.length > 4 && urlPart[3] == 'themes'){

      var themeName = this.storageService.getItem<string>(Storage.THEME_NAME);

      if(themeName == null) return; 
      this.breadcrumb = [...this.breadcrumb, {name: themeName, url: '/subjects/' + this.storageService.getItem(Storage.SUBJECT_ID) + '/themes' +this.storageService.getItem(Storage.THEME_ID)}]
    }
  }

}
