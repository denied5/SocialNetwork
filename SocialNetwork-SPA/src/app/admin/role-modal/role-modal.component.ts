import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { User } from 'src/app/_model/User';

@Component({
  selector: 'app-role-modal',
  templateUrl: './role-modal.component.html',
  styleUrls: ['./role-modal.component.css']
})
export class RoleModalComponent implements OnInit {
  @Output() updateSelectorRoles = new EventEmitter();
  user: User;
  roles: any[];
  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit() {
  }

  updateRoles() {
    debugger;
    this.updateSelectorRoles.emit(this.roles);
    this.bsModalRef.hide();
  }
}
