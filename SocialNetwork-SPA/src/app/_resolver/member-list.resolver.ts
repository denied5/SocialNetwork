import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { User } from '../_model/User';
import { UserService } from '../Services/user.service';
import { AlertifyService } from '../Services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()

export class MemberListResolver implements Resolve<User[]> {
    pageNumber = 1;
    pageSize = 10;


    constructor(private userService: UserService, private router: Router,
                private alertify: AlertifyService) {

    }

    resolve(route: ActivatedRouteSnapshot): Observable<User[]> {
        return this.userService.getUsers(this.pageNumber, this.pageSize).pipe(
            catchError(error => {
                this.alertify.error('Problem Retreiving data');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}
