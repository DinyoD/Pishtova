import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit{
  showModale: boolean = false;
  isAuth: boolean = false;

  constructor(private cd: ChangeDetectorRef, private userService : UserService ) {}

  ngOnInit(): void {
    this.isAuth = this.userService.isUserAuthenticated();
  }

  changesChowModal(){
    this.showModale = !this.showModale;
    this.cd.detectChanges();
    console.log(this.showModale);
    
  }
}
