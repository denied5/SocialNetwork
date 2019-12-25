import { Component, OnInit, Input } from '@angular/core';
import { PostComment } from '../_model/PostComment';
import { AuthService } from '../Services/Auth.service';
import { PostService } from '../Services/post.service';
import { AlertifyService } from '../Services/alertify.service';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent implements OnInit {
  @Input() comments: PostComment[];
  isFull: boolean;
  userKnownAs: string = '';
  constructor(private authService: AuthService,private postService: PostService, 
    private alertify: AlertifyService) { }

  ngOnInit() {
    this.comments = this.comments.reverse();
    this.userKnownAs = JSON.parse(localStorage.getItem('user')).knownAs;
  }

  changeCommentsList() {
    this.isFull = !this.isFull;
  }

  deleteComment(id: number) {
    this.postService.deleteComment(id).subscribe( () => {
      let index = this.comments.findIndex(x => x.id == id)
      this.comments.splice(index, 1);
      this.alertify.success("Comment Deleted");
    }, error => {
      this.alertify.error(error);
    })
  }

}
