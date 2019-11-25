import { Component, OnInit } from '@angular/core';
import { UserService } from '../Services/user.service';
import { User } from '../_model/User';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../Services/alertify.service';
import { Pagination, PaginatedResult } from '../_model/pagination';

@Component({
  selector: 'app-peoples',
  templateUrl: './peoples.component.html',
  styleUrls: ['./peoples.component.css']
})
export class PeoplesComponent implements OnInit {
  users: User[];
  pagination: Pagination;
  userParams: any = {};
  user: User = JSON.parse(localStorage.getItem('user'));
  genderList = [{ value: 'male', display: 'Males' }, { value: 'female', display: 'Females' }, { value: 'any', display: 'any' }];


  constructor(private userService: UserService, private route: ActivatedRoute,
              private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.users = data.users.result;
      this.pagination = data.users.pagination;
    });

    this.userParams.gender = 'any';
    this.userParams.minAge = 14;
    this.userParams.maxAge = 99;
    this.userParams.name = '';
  }

  resetFilters() {
    this.userParams.gender = 'any';
    this.userParams.minAge = 14;
    this.userParams.maxAge = 99;
    this.loadUsers();
  }

  pageChanged(event: any): void {
    this.pagination.CurrentPage = event.page;
    this.loadUsers();
  }

  loadUsers() {
    this.userService.getUsers(this.pagination.CurrentPage, this.pagination.ItemsPerPage, this.userParams)
      .subscribe((res: PaginatedResult<User[]>) => {
        this.users = res.result;
        this.pagination = res.pagination;
      }, error => {
        this.alertify.error(error);
      });
  }



}
