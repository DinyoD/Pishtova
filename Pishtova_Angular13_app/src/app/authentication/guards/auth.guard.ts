import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';
import { AuthService, StorageService } from '../../services';

@Injectable({
  providedIn: 'root'
})
export class ForAuthenticatedUserGuard implements CanActivate {

  constructor(
    private userService: AuthService,
    private storageService: StorageService,
    private router: Router
    ){};

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
    ) {
      
    if (this.userService.isUserAuthenticated()) {
      return true;
    }
    this.storageService.clear();
    this.router.navigate(['/'], { queryParams: { returnUrl: state.url } })
    return false;
  }
  
}
