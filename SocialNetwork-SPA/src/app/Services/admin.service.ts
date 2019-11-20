import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { User } from '../_model/User';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiURL + 'admin/';
  constructor(private http: HttpClient) { }

  getUsersWithRoles() {
    return this.http.get(this.baseUrl + 'usersWithRoles');
  }

  updateUserRoles(user: User, roles: {}) {
    return this.http.post(this.baseUrl + 'editRoles/' + user.userName, roles);
  }
}
