import { Component, OnInit } from '@angular/core';
import { User } from '../_model/User';
import { FriendshipService } from '../Services/friendship.service';
import { AuthService } from '../Services/Auth.service';
import { AlertifyService } from '../Services/alertify.service';

@Component({
  selector: 'app-friends',
  templateUrl: './friends.component.html',
  styleUrls: ['./friends.component.css']
})
export class FriendsComponent implements OnInit {
  friends: User[];
  constructor(private friendshipService: FriendshipService, private authService: AuthService,
    private alertify: AlertifyService) { }

  ngOnInit() {
    this.loadFriends();
  }

  loadFriends() {
    this.friendshipService.getFriends(this.authService.decodedToken.nameid).subscribe(
      res => {
        this.friends = res;
      },
      error => {
        this.alertify.error(error);
      }
    );
 }

 deleteFriend(userid){
  this.alertify.confirm('Are you sure', () => {
    this.friendshipService.deleteFriend(this.authService.decodedToken.nameid, userid).subscribe(() => {
      this.friends.splice(this.friends.findIndex(m => m.id === userid), 1);
      this.alertify.success('message deleted');
    }, error => {
      this.alertify.error(error);
    })
  })
   ;
 }
}
