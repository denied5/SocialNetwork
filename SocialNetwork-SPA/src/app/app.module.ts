import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RegisterComponent } from './register/register.component';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthService } from './Services/Auth.service';
import { HomeComponent } from './home/home.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { PeoplesComponent } from './peoples/peoples.component';
import { MessagesComponent } from './messages/messages.component';
import { AlertifyService } from './Services/alertify.service';
import { ErrorInterceptorProvider } from './Services/error.interceptor';
import { JwtModule } from '@auth0/angular-jwt';
import { MemberCardComponent } from './member/member-card/member-card.component';
import { MemberDetailComponent } from './member/member-detail/member-detail.component';
import { MemberDetailResolver } from './_resolver/member-detail.resolver';
import { TabsModule, PaginationModule } from 'ngx-bootstrap';
import { NgxGalleryModule } from 'ngx-gallery';
import { FileUploadModule } from 'ng2-file-upload';
import { MemberEditResolver } from './_resolver/member-edit.resolver';
import { MemberEditComponent } from './member/member-edit/member-edit.component';
import { PhotoEditorComponent } from './member/photo-editor/photo-editor.component';
import { PhotoService } from './Services/photo.service';
import { MemberListResolver } from './_resolver/member-list.resolver';
import { FriendshipService } from './Services/friendship.service';
import { FriendsComponent } from './friends/friends.component';
import { FriendsResolver } from './_resolver/friends.resolver';
import { MessagesResolver } from './_resolver/messages.resolver';
import { MessageService } from './Services/message.service';
import { MemberMessagesComponent } from './member/member-messages/member-messages.component';
import { TimeAgoPipe } from 'time-ago-pipe';
import { FeedComponent } from './feed/feed.component';
import { PostsEditorComponent } from './member/posts-editor/posts-editor.component';
import { FeedResolver } from './_resolver/feed.resolver';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { AuthGuard } from './_resolver/auth.guard';
import { PhotoMenegmentComponent } from './admin/photo-menegment/photo-menegment.component';
import { UserMenegmentComponent } from './admin/user-menegment/user-menegment.component';
import { AdminService } from './Services/admin.service';
import { RoleModalComponent } from './admin/role-modal/role-modal.component';
import { HasRoleDirective } from './_directives/hasRole.directive';
import { CommentComponent } from './comment/comment.component';
import { SendCommentComponent } from './sendComment/sendComment.component';
import { AngularFireModule } from '@angular/fire';
import { AngularFireMessagingModule } from '@angular/fire/messaging';
import { UserMenegmentResolver } from './_resolver/user-menegment.resolver';


export function tokenGetter() {
   return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      RegisterComponent,
      HomeComponent,
      PeoplesComponent,
      MessagesComponent,
      MemberCardComponent,
      MemberDetailComponent,
      MemberEditComponent,
      PhotoEditorComponent,
      FriendsComponent,
      MemberMessagesComponent,
      TimeAgoPipe,
      FeedComponent,
      PostsEditorComponent,
      AdminPanelComponent,
      HasRoleDirective,
      PhotoMenegmentComponent,
      UserMenegmentComponent,
      RoleModalComponent,
      CommentComponent,
      SendCommentComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      FormsModule,
      ReactiveFormsModule,
      ModalModule.forRoot(),
      HttpClientModule,
      BsDatepickerModule.forRoot(),
      BrowserAnimationsModule,
      BsDropdownModule.forRoot(),
      TabsModule.forRoot(),
      NgxGalleryModule,
      FileUploadModule,
      JwtModule.forRoot({
         config: {
            tokenGetter,
            whitelistedDomains: ['localhost:5000'],
            blacklistedRoutes: ['localhost:5000/api/auth']
         }
      }),
      AngularFireModule.initializeApp({
         apiKey: 'AIzaSyAB-PTzdcLbJxyPW4zE-faDabEUJ3cFMUs',
         authDomain: 'socializer-7f68d.firebaseapp.com',
         databaseURL: 'https://socializer-7f68d.firebaseio.com',
         projectId: 'socializer-7f68d',
         storageBucket: 'socializer-7f68d.appspot.com',
         messagingSenderId: '155560449491',
         appId: '1:155560449491:web:d2b02d98aa57f2d5164b26'
       }),
       AngularFireMessagingModule,
      PaginationModule.forRoot(),
   ],
   providers: [
      AuthService,
      AlertifyService,
      PhotoService,
      FriendshipService,
      MessageService,
      ErrorInterceptorProvider,
      MemberDetailResolver,
      MemberEditResolver,
      MemberListResolver,
      FriendsResolver,
      MessagesResolver,
      UserMenegmentResolver,
      FeedResolver,
      AuthGuard,
      AdminService
   ],
   entryComponents: [
      RoleModalComponent,
      MemberDetailComponent
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }