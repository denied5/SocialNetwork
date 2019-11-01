import { Injectable } from '@angular/core';
import {map} from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { User } from '../_model/User';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt'
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  decodedToken: any;
  currentUser: User;
  baseUrl = 'http://localhost:5000/api/auth/';
  jwtHelper = new JwtHelperService();
  photoUrl = new BehaviorSubject<string>('../../assets/user.png');//many to many 
  currentPhotoUrl = this.photoUrl.asObservable();

  constructor(private http: HttpClient, private router: Router) { }

  changeMemberPhoto(photoUrl: string) {
    this.photoUrl.next(photoUrl);
  }

  login(model: any){
    return this.http.post(this.baseUrl + "login", model)
    .pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.userToken);
          localStorage.setItem('user', JSON.stringify(user.userFromDb));
          this.decodedToken = this.jwtHelper.decodeToken(user.userToken);
          this.currentUser = user.userFromDb;
          this.changeMemberPhoto(this.currentUser.photoUrl);
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
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  register(user: User) {
    return this.http.post(this.baseUrl + 'register', user);
  }
}
