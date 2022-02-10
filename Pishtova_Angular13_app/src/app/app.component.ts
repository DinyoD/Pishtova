import { Component, OnInit } from '@angular/core';
import { AuthService } from './services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  public Auth: boolean;

  constructor(private authService: AuthService){
    this.Auth = authService.isUserAuthenticated();
  }

  ngOnInit(): void {
    this.authService.isAuthChange.subscribe( isAuth => this.Auth = isAuth);
  }
}
