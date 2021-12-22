import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';
import { UserService } from '../services';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private userService: UserService,
    private router: Router
    ){};

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
    ) {
      
    if (this.userService.isUserAuthenticated()) {
      return true;
    }
    this.router.navigate(['/'], { queryParams: { returnUrl: state.url } })
    return false;
  }
  
}
