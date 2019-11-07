import { Injectable } from "@angular/core";
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../Services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Message } from '../_model/Message';
import { MessageService } from '../Services/message.service';
import { AuthService } from '../Services/Auth.service';

@Injectable()

export class MessagesResolver implements Resolve<Message[]> {
    pageNumber = 1;
    pageSize = 10;


    constructor(private router: Router, private messageService: MessageService,
        private authService: AuthService, private alertify: AlertifyService) {

    }

    resolve(route: ActivatedRouteSnapshot): Observable<Message[]>  {
        return this.messageService.getMessages(this.authService.decodedToken.nameid, 
                this.pageNumber, this.pageSize).pipe(
            catchError(error => {
                this.alertify.error('Problem Retreiving messages');
                this.router.navigate(['/home']);
                return of (null);
            })
        );
    }
}