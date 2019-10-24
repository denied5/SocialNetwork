import { Injectable } from '@angular/core';
import {map} from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { User } from '../_model/User';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  decodedToken: any;
  currentUser: User;
  baseUrl = 'http://localhost:5000/api/auth/';
  constructor(private http: HttpClient, private router: Router) { }

  login(model: any){
    return this.http.post(this.baseUrl + "login", model)
    .pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          this.currentUser = user.user;
          localStorage.setItem('token', user.userToken);
          localStorage.setItem('user', JSON.stringify(user.userFromDb));
        }
      }
    ));
  }

  logout(){
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.currentUser =null;
    this.decodedToken = null;
    this.router.navigate(['/home']);
  }

  loggedIn(){
    return !!localStorage.getItem('token');
  }

  register(user: User) {
    return this.http.post(this.baseUrl + 'register', user);
  }
}
