import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';
import { UserService } from '../../services';

@Injectable({
  providedIn: 'root'
})

export class ForUnauthenticatedUserGuard implements CanActivate {

  constructor(
    private userService: UserService,
    private router: Router
    ){};

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
    ) {
      
    if (!this.userService.isUserAuthenticated()) {
      return true;
    }
    this.router.navigate(['/main'])
    return false;
  }
  
}