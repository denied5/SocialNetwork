import { Component, TemplateRef, OnInit } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AuthService } from '../Services/Auth.service';
import { AlertifyService } from '../Services/alertify.service';
import { Router } from '@angular/router';
import { PushNotificationService } from '../Services/push-notification.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  modalRef: BsModalRef;
  photoUrl: string;
  model: any = {};
  userId: number;

  constructor(private modalService: BsModalService, public authService: AuthService,
              private alertifyService: AlertifyService, public router: Router,
              private pushNot: PushNotificationService) { }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.alertifyService.success('Login succses');
      this.modalRef.hide();
    },
      error => {
        console.log(error);
        this.alertifyService.error(error);
        this.modalRef.hide();
      });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    this.authService.logout();
  }

  ngOnInit() {
    this.pushNot.receiveMessage();
    this.authService.currentPhotoUrl.subscribe(photoUrl => {
      this.photoUrl = photoUrl;
    });
    this.userId = this.authService.decodedToken.nameid;
  }

}
