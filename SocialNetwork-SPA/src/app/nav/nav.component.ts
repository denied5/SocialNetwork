import { Component, TemplateRef, OnInit } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AuthService } from '../Services/Auth.service';
import { AlertifyService } from '../Services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  modalRef: BsModalRef;
  photoUrl: string;
  model: any = {};
  
  constructor(private modalService: BsModalService, public authService: AuthService,
              private alertifyService: AlertifyService) {}
 
  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  login(){
    this.authService.login(this.model).subscribe(next => {
      this.alertifyService.success('Login succses');
    },
    error => {
      console.log(error)
      this.alertifyService.error(error);
    });
  }

  loggedIn()
  {
    return this.authService.loggedIn();
  }

  logout(){
    this.authService.logout();
  }

  ngOnInit() {
    this.authService.currentPhotoUrl.subscribe(photoUrl => {
      this.photoUrl = photoUrl;
    });
  }

}
