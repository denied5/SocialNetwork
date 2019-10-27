import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PeoplesComponent } from './peoples/peoples.component';
import { MessagesComponent } from './messages/messages.component';
import { MemberDetailComponent } from './member/member-detail/member-detail.component';
import { MemberDetailResolver } from './_resolver/member-detail.resolver';
import { MemberEditComponent } from './member/member-edit/member-edit.component';
import { MemberEditResolver } from './_resolver/member-edit.resolver';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'members', component: PeoplesComponent},
  {path: 'members/:id', component: MemberDetailComponent,
        resolve: {user: MemberDetailResolver}},
  {path: 'members', component: MemberDetailComponent},
  {path: 'messages', component: MessagesComponent},
  {path: 'member/edit', component: MemberEditComponent,
  resolve: {user: MemberEditResolver}},
  {path: '**', redirectTo: '', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
