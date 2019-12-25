import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../Services/Auth.service';
import { PostService } from '../Services/post.service';
import { PostComment } from '../_model/PostComment';
import { AlertifyService } from '../Services/alertify.service';

@Component({
  selector: 'app-sendComment',
  templateUrl: './sendComment.component.html',
  styleUrls: ['./sendComment.component.css']
})
export class SendCommentComponent implements OnInit {
  @Input() postId: number;
  @Output() updateComments = new EventEmitter();
  photoUrl: string;
  comment: any = {};
  constructor(private authService: AuthService, private postService: PostService,
    private alertify: AlertifyService) { }

  ngOnInit() {
    this.authService.currentPhotoUrl.subscribe(photoUrl => {
      this.photoUrl = photoUrl;
    });
  }

  createComment() {
    this.postService.createComment(this.comment.content, this.postId, this.authService.decodedToken.nameid)
      .subscribe((comment) => {
        this.updateComments.emit(comment);
      }, error => {
        this.alertify.error(error);
      });
  }
}
