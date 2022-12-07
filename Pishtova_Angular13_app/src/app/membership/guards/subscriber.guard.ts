import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from "@angular/router";
import { AuthService } from "src/app/services";

@Injectable({
    providedIn: 'root'
})

export class ForSubscribersGuard implements CanActivate{
    constructor(
        private authService: AuthService,
        private router: Router
    ){}
    
    canActivate(
    next: ActivatedRouteSnapshot, 
    stte: RouterStateSnapshot
    ): boolean {
        let suscriber = this.authService.getCurrentUser()?.isSubscriber;
    if (suscriber) {    
        return true;
    }
    this.router.navigate(['/memberships']);
        return false;
    };

}