import { Component, OnInit } from '@angular/core';
import { Post } from '../_model/Post';
import { PostService } from '../Services/post.service';
import { AlertifyService } from '../Services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Pagination, PaginatedResult } from '../_model/pagination';
import { AuthService } from '../Services/Auth.service';

@Component({
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.css']
})
export class FeedComponent implements OnInit {
  feed: Post[];
  pagination: Pagination;
  userId: number;
  constructor(private postService: PostService, private alertify: AlertifyService,
    private route: ActivatedRoute, private authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe( data => {
      this.feed = data.feed.result;
      this.pagination = data.feed.pagination;
    })
    this.userId = this.authService.decodedToken.nameid;
  }

  loadFeed()
  {
    this.postService.getFeed(this.userId, this.pagination.CurrentPage, this.pagination.ItemsPerPage)
      .subscribe((res: PaginatedResult<Post[]>) => {
        debugger;
        this.feed = res.result;
        this.pagination = res.pagination;
      }, error => {
        this.alertify.error(error);
      });
  }

  pageChanged(event: any): void {
    this.pagination.CurrentPage = event.page;
    this.loadFeed();
  }
}
