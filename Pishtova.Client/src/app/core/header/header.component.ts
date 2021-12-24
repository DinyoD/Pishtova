import { ChangeDetectorRef, Component, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { UserService } from 'src/app/services';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit, OnChanges{
  showModale: boolean = false;
  isAuth: boolean = false;

  constructor(private cd: ChangeDetectorRef, private userService : UserService ) {}

  ngOnChanges(): void {
    this.cd.detectChanges();
  }

  ngOnInit(): void {
    this.isAuth = this.userService.isUserAuthenticated();
  }

  changesChowModal(){
    this.showModale = !this.showModale;
    console.log(this.showModale);   
  }

  hideModal(){
    this.showModale = false;
  }
}
