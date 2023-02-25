import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';
import { AuthService, StorageService } from '../../services';

@Injectable({
  providedIn: 'root'
})
export class ForAuthenticatedUserGuard implements CanActivate {

  constructor(
    private authService: AuthService,
    private storageService: StorageService,
    private router: Router
    ){};

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
    ): boolean {
      
    if (this.authService.isUserAuthenticated()) {
      return true;
    }
    this.storageService.clear();
    this.router.navigate(['/auth/login'], { queryParams: { returnUrl: state.url } })
    return false;
  }
  
}
