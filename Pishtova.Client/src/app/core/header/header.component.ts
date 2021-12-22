import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  public isUserAuthenticated: boolean = false;
  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.authChanged.subscribe( res => this.isUserAuthenticated = res)
  }

}
