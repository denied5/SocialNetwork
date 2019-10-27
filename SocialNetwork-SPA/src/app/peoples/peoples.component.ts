import { Component, OnInit } from '@angular/core';
import { UserService } from '../Services/user.service';
import { User } from '../_model/User';

@Component({
  selector: 'app-peoples',
  templateUrl: './peoples.component.html',
  styleUrls: ['./peoples.component.css']
})
export class PeoplesComponent implements OnInit {
  users: User[];

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.loadUsers();
  }

  loadUsers(){
    this.userService.getUsers().subscribe(res => {
      this.users = res;
    })
  }

}
