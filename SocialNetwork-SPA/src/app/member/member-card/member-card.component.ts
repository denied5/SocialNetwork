import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/_model/User';
import { FriendshipService } from 'src/app/Services/friendship.service';
import { AuthService } from 'src/app/Services/Auth.service';
import { AlertifyService } from 'src/app/Services/alertify.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {
  @Input() user: User;
  constructor(private friendshipService: FriendshipService, private authService: AuthService,
              private alertyfy: AlertifyService) { }

  ngOnInit() {
  }

  addFriend() {
    this.friendshipService.addFriend(this.authService.decodedToken.nameid, this.user.id)
      .subscribe(() => {
        this.alertyfy.success('You add ' + this.user.knownAs);
      }, error => {
        this.alertyfy.error(error);
      });
  }

}
