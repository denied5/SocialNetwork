import {  Injectable } from "@angular/core";
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { AuthService } from '../Services/Auth.service';
import { AlertifyService } from '../Services/alertify.service';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    constructor(private authService: AuthService, private router: Router,
                private alertify: AlertifyService){}


    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):
     boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        const roles = route.data.roles as Array<string>;
        if (roles) {
            const match = this.authService.roleMatch(roles);
            if (match) {
                return true;
            } else {
                this.alertify.error('You aren\'t an admin!');
                this.router.navigate(['/home']);
                return false;
            }
        }

        if (this.authService.loggedIn()) {
            return true;
        }

        this.alertify.error('You aren\'t allowed to be here!');
        this.router.navigate(['/home']);
        return false;
    }
}