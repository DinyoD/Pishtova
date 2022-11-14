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
        console.log(this.authService.getCurrentUser());
        
        let suscriber = this.authService.getCurrentUser()?.isSubscriber;
    if (!suscriber) {
        console.log(123);      
        return true;
    }
    this.router.navigate(['/memberships/plan']);
    return false;
    };

}