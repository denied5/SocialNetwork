import { Component, OnInit, TemplateRef } from '@angular/core';
import { User } from 'src/app/_model/User';
import { AdminService } from 'src/app/Services/admin.service';
import { AlertifyService } from 'src/app/Services/alertify.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { RoleModalComponent } from '../role-modal/role-modal.component';

@Component({
  selector: 'app-user-menegment',
  templateUrl: './user-menegment.component.html',
  styleUrls: ['./user-menegment.component.css']
})
export class UserMenegmentComponent implements OnInit {
  users: User[];
  bsModalRef: BsModalRef;

  constructor(private adminService: AdminService, private alertify: AlertifyService,
              private modalService: BsModalService) { }

  ngOnInit() {
    this.getUsersWithRoles();
  }

  getUsersWithRoles() {
    this.adminService.getUsersWithRoles().subscribe((users: User[]) => {
      this.users = users;
    }, error => {
      console.log(error);
      this.alertify.error(error);
    });
  }

  editRolesModal(user: User) {
    const initialState = {
      user,
      roles: this.getRolesArray(user)
    };
    this.bsModalRef = this.modalService.show(RoleModalComponent, { initialState });
    this.bsModalRef.content.updateSelectorRoles.subscribe((values) => {
      const rolesToUpdate = {
        roleNames: [...values.filter(el => el.checked === true).map(el => el.name)]
      };
      if (rolesToUpdate) {
        this.adminService.updateUserRoles(user, rolesToUpdate).subscribe(() => {
          user.roles = [...rolesToUpdate.roleNames];
        }, error => {
          this.alertify.error(error);
        });
      }
    });
  }

  private getRolesArray(user) {
    const roles = [];
    const userRoles = user.roles;
    const avalibleRoles: any[] = [
      { name: 'Admin', value: 'Admin' },
      { name: 'Moderator', value: 'Moderator' },
      { name: 'Member', value: 'Member' }
    ];

    for (let i = 0; i < avalibleRoles.length; i++) {
      let isMatch = false;
      for (let j = 0; j < userRoles.length; j++) {
        if (avalibleRoles[i].name === userRoles[j]) {
          isMatch = true;
          avalibleRoles[i].checked = true;
          roles.push(avalibleRoles[i]);
          break;
        }
      }
      if (!isMatch) {
        avalibleRoles[i].checked = false;
        roles.push(avalibleRoles[i]);
      }
    }

    return roles;
  }
}
