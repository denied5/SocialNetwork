import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { User } from '../_model/User';
import { AlertifyService } from '../Services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { FriendshipService } from '../Services/friendship.service';
import { AuthService } from '../Services/Auth.service';

@Injectable()

export class FriendsResolver implements Resolve<User[]> {
    pageNumber = 1;
    pageSize = 10;


    constructor(private friendshipService: FriendshipService, private router: Router,
        private alertify: AlertifyService, private authService: AuthService) {

    }

    resolve(route: ActivatedRouteSnapshot): Observable<User[]> {
        return this.friendshipService.getFriends(this.authService.decodedToken.nameid).pipe(
            catchError(error => {
                this.alertify.error('Problem Retreiving data');
                return of(null);
            })
        );
    }
}
