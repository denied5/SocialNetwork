import { Component, OnInit } from '@angular/core';
import { AuthService } from './Services/Auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from './_model/User';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'SocialNetwork-SPA';
  jwtHelper = new JwtHelperService();

  ngOnInit() {
    const token = localStorage.getItem('token');
    const user: User = JSON.parse(localStorage.getItem('user'));
    if (token) {
      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
    }
    if (user) {
      this.authService.currentUser = user;
      this.authService.changeMemberPhoto(user.photoUrl);
    }
  }

  constructor(private authService: AuthService) { }


}
