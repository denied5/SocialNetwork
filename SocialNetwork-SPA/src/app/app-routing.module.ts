import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PeoplesComponent } from './peoples/peoples.component';
import { MessagesComponent } from './messages/messages.component';
import { MemberDetailComponent } from './member/member-detail/member-detail.component';
import { MemberDetailResolver } from './_resolver/member-detail.resolver';
import { MemberEditComponent } from './member/member-edit/member-edit.component';
import { MemberEditResolver } from './_resolver/member-edit.resolver';
import { MemberListResolver } from './_resolver/member-list.resolver';
import { FriendsComponent } from './friends/friends.component';
import { FriendsResolver } from './_resolver/friends.resolver';
import { MessagesResolver } from './_resolver/messages.resolver';
import { FeedComponent } from './feed/feed.component';
import { FeedResolver } from './_resolver/feed.resolver';

const routes: Routes = [
    {path: '', component: HomeComponent},
    {path: 'members', component: PeoplesComponent,
        resolve: {users: MemberListResolver}},
    {path: 'members/:id', component: MemberDetailComponent,
        resolve: {user: MemberDetailResolver}},
    {path: 'member/edit', component: MemberEditComponent,
        resolve: {user: MemberEditResolver}},
    {path: 'messages', component: MessagesComponent,
        resolve: {messages: MessagesResolver}},
    {path: 'friends', component: FriendsComponent,
        resolve: {users: FriendsResolver}},
    {path: 'feed', component: FeedComponent,
        resolve: {feed: FeedResolver}},
    {path: '**', redirectTo: '', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
