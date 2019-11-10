import { Injectable } from "@angular/core";
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../Services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../Services/Auth.service';
import { Post } from '../_model/Post';
import { PostService } from '../Services/post.service';

@Injectable()

export class FeedResolver implements Resolve<Post[]> {
    pageNumber = 1;
    pageSize = 10;


    constructor(private router: Router, private postService: PostService,
        private authService: AuthService, private alertify: AlertifyService) {

    }

    resolve(route: ActivatedRouteSnapshot): Observable<Post[]>  {
        return this.postService.getFeed(this.authService.decodedToken.nameid,
                this.pageNumber, this.pageSize).pipe(
            catchError(() => {
                this.alertify.error('Problem Retreiving feed');
                this.router.navigate(['/home']);
                return of (null);
            })
        );
    }
}