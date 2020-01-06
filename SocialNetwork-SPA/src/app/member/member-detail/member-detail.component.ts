import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from 'src/app/_model/User';
import { UserService } from 'src/app/Services/user.service';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from 'src/app/Services/alertify.service';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from 'ngx-gallery';
import { TabsetComponent } from 'ngx-bootstrap';
import { PostService } from 'src/app/Services/post.service';
import { Post } from 'src/app/_model/Post';
import { AuthService } from 'src/app/Services/Auth.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  @ViewChild('memberTabs') memberTabs: TabsetComponent;
  user: User;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  posts: Post[];
  constructor(public route: ActivatedRoute, private authService: AuthService,
              private alertify: AlertifyService, private postService: PostService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data.user;
    });
    this.route.queryParams.subscribe(params => {
      const selectTab = params.tab;
      this.memberTabs.tabs[selectTab > 0 ? selectTab : 0].active = true;
    });
    this.loadPosts();
    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false
      }
    ];
    this.galleryImages = this.getImages();
  }

  getImages() {
    const imagesUrls = [];
    for (let i = 0; i < this.user.photos.length; i++) {
      imagesUrls.push({
        small: this.user.photos[i].url,
        medium: this.user.photos[i].url,
        big: this.user.photos[i].url,
        descriprion: 'this.user.photos[i].description'
      });
    }
    return imagesUrls;
  }

  selectTab(tabId: number) {
    this.memberTabs.tabs[tabId].active = true;
  }

  loadPosts() {
    this.postService.getPosts(this.user.id).subscribe(
      posts => {
        this.posts = posts;
      },
      error => {
        this.alertify.error(error);
      }
    );
  }

  sameUser(): boolean {
    return this.user.id == this.authService.decodedToken.nameid;
  }
}
