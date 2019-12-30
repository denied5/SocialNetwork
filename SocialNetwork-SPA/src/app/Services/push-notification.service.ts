import { Injectable } from '@angular/core';
import { AngularFireMessaging } from '@angular/fire/messaging';
import { mergeMapTo } from 'rxjs/operators';
import { take } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs'
import { UserService } from './user.service';
import { User } from '../_model/User';
import { AlertifyService } from './alertify.service';
import { PayloadNotification } from '../_model/payloadNotification';

@Injectable({
  providedIn: 'root'
})
export class PushNotificationService {
  user: User;
  currentMessage = new BehaviorSubject(null);

  constructor(private angularFireMessaging: AngularFireMessaging, private userService: UserService,
    private alertify: AlertifyService) {
    this.angularFireMessaging.messaging.subscribe(
      (_messaging) => {
        _messaging.onMessage = _messaging.onMessage.bind(_messaging);
        _messaging.onTokenRefresh = _messaging.onTokenRefresh.bind(_messaging);
      }
    )
  }


  updateToken(userId, token) {
    debugger;
    this.user = JSON.parse(localStorage.getItem('user'));
    this.user.fairbaseToken = token;
    this.userService.putUser(userId, this.user).subscribe( () => {
      console.log("set token");
    });
  }

  
  requestPermission(userId) {
    this.angularFireMessaging.requestToken.subscribe(
      (token) => {
        this.updateToken(userId, token);
      },
      (err) => {
        console.error('Unable to get permission to notify.', err);
      }
    );
  }

  /**
   * hook method when new notification received in foreground
   */
  receiveMessage() {
    this.angularFireMessaging.messages.subscribe(
      (payload : PayloadNotification) => {
        console.log(payload);
        this.alertify.message(payload.notification.body);
        this.currentMessage.next(payload);
      })
  }
}
