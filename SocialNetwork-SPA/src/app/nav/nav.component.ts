import { Component, TemplateRef, OnInit } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AuthService } from '../Services/Auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  modalRef: BsModalRef;
  model: any = {};
  constructor(private modalService: BsModalService, private authService: AuthService) {}
 
  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  login(){
    this.authService.login(this.model).subscribe(next => {
      console.log('Login succses');
    },
    error => {
      console.log(error);
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
  }

}
