import { Component, OnInit } from '@angular/core';
import { User } from '../_model/User';
import { FriendshipService } from '../Services/friendship.service';
import { AuthService } from '../Services/Auth.service';
import { AlertifyService } from '../Services/alertify.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-friends',
  templateUrl: './friends.component.html',
  styleUrls: ['./friends.component.css']
})
export class FriendsComponent implements OnInit {
  friends: User[];
  followers: User[];
  followings: User[];
  constructor(private friendshipService: FriendshipService, private authService: AuthService,
    private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.friends = data.users.friends;
      this.followers = data.users.followers;
      this.followings = data.users.followings;
    })
  }

  // loadFriends() {
  //   this.friendshipService.getFriends(this.authService.decodedToken.nameid).subscribe(
  //     res => {
  //       this.friends = res;
  //     },
  //     error => {
  //       this.alertify.error(error);
  //     }
  //   );
  //  }
  addFollower(userid){
    this.friendshipService.addFriend(this.authService.decodedToken.nameid , userid)
    .subscribe( () => {
      this.friends.push(this.followers[this.followers.findIndex(m => m.id === userid)]);
      this.followers.splice(this.followers.findIndex(m => m.id === userid), 1);
      this.alertify.success("You add friend");
    }, error => {
      this.alertify.error(error);
    });
  }

  unfollow(userid){
    this.alertify.confirm('Are you sure', () => {
      this.friendshipService.deleteFriend(this.authService.decodedToken.nameid, userid).subscribe(() => {
        this.followings.splice(this.followings.findIndex(m => m.id === userid), 1);
        this.alertify.success('yyou unfollow');
      }, error => {
        this.alertify.error(error);
      })
    })
     ;
   }

 deleteFriend(userid){
  this.alertify.confirm('Are you sure', () => {
    this.friendshipService.deleteFriend(this.authService.decodedToken.nameid, userid).subscribe(() => {
      this.followers.push(this.friends[this.friends.findIndex(m => m.id === userid)]);
      this.friends.splice(this.friends.findIndex(m => m.id === userid), 1);
      this.alertify.success('message deleted');
    }, error => {
      this.alertify.error(error);
    })
  })
   ;
 }
}
