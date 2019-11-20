import { Component, TemplateRef, OnInit } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AuthService } from '../Services/Auth.service';
import { AlertifyService } from '../Services/alertify.service';
import { Router } from '@angular/router';

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
              private alertifyService: AlertifyService, public router: Router) {}
 
  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  login(){
    this.authService.login(this.model).subscribe(next => {
      this.alertifyService.success('Login succses');
      this.modalRef.hide();
      this.router.navigate(['/admin']);
    },
    error => {
      console.log(error)
      this.alertifyService.error(error);
      this.modalRef.hide();
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
