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
  request: User[];
  constructor(private friendshipService: FriendshipService, private authService: AuthService,
    private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      debugger;
      this.friends = data.users.friends;
      this.request = data.users.requests;
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
  addFriend(userid){
    this.friendshipService.addFriend(this.authService.decodedToken.nameid , userid)
    .subscribe( () => {
      debugger;
      this.friends.push(this.request[this.request.findIndex(m => m.id === userid)]);
      this.request.splice(this.request.findIndex(m => m.id === userid), 1);
      this.alertify.success("You add friend");
    }, error => {
      this.alertify.error(error);
    });
  }


 deleteFriend(userid){
  this.alertify.confirm('Are you sure', () => {
    this.friendshipService.deleteFriend(this.authService.decodedToken.nameid, userid).subscribe(() => {
      this.request.push(this.friends[this.friends.findIndex(m => m.id === userid)]);
      this.friends.splice(this.friends.findIndex(m => m.id === userid), 1);
      this.alertify.success('message deleted');
    }, error => {
      this.alertify.error(error);
    })
  })
   ;
 }
}
