import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../../services';

@Injectable({
  providedIn: 'root'
})

export class ForUnauthenticatedUserGuard implements CanActivate {

  constructor(
    private userService: AuthService,
    private router: Router
    ){};

  canActivate() {
      
    if (!this.userService.isUserAuthenticated()) {
      return true;
    }
    this.router.navigate(['/'])
    return false;
  }
}
