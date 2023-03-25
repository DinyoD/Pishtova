import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { map } from 'rxjs/operators';
import { UserService } from 'src/app/services';

@Injectable({
  providedIn: 'root',
})
export class ForSubscribersGuard implements CanActivate {
  constructor(private userService: UserService, private router: Router) {}

  canActivate(next: ActivatedRouteSnapshot, stte: RouterStateSnapshot) {
    return this.chekUserIsSubscriber();
  }

  public chekUserIsSubscriber() {
    return this.userService.isSubscriber().pipe(
      map((x) => {
        if (x) {
          return true;
        } else {
          this.router.navigate(['/memberships']);
          return false;
        }
      })
    );
  }
}
