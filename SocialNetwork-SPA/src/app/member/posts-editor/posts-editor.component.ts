import { Component, OnInit, Input } from '@angular/core';
import { Post } from 'src/app/_model/Post';
import { PostService } from 'src/app/Services/post.service';
import { AuthService } from 'src/app/Services/Auth.service';
import { AlertifyService } from 'src/app/Services/alertify.service';

@Component({
  selector: 'app-posts-editor',
  templateUrl: './posts-editor.component.html',
  styleUrls: ['./posts-editor.component.css']
})
export class PostsEditorComponent implements OnInit {
  posts: Post[];
  newPost: any = {};
  constructor(private postService: PostService, private authService: AuthService,
      private alertify: AlertifyService) { }

  ngOnInit() {
    this.loadPosts();
    this.newPost.content = '';
    this.newPost.userId = this.authService.decodedToken.nameid;
  }

  createPost(){
    this.postService.createPost(this.authService.decodedToken.nameid, this.newPost)
      .subscribe((post: Post) => {
        this.posts.unshift(post);
        this.newPost.content = '';
    }, error => {
      this.alertify.error(error);
    });;
  }

  loadPosts(){
    this.postService.getPosts(this.authService.decodedToken.nameid).subscribe(
      posts => {
        this.posts = posts;
      },
      error => {
        this.alertify.error(error);
      }
    )
  }

  deletePost(postId: number){
    this.postService.deletePost(this.authService.decodedToken.nameid, postId).subscribe(
      () => {
        this.posts.splice(
          this.posts.findIndex(p => p.id === postId), 1);
      },
      error => {
        this.alertify.error(error);
      }
    )
  }

}
