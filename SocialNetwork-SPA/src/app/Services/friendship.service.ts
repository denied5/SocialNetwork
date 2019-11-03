import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_model/User';

@Injectable({
  providedIn: 'root'
})
export class FriendshipService {
  baseUrl = 'http://localhost:5000/api/users/';
  constructor(private http: HttpClient) { }

  addFriend(userId: number, recipientId: number){
    return this.http.post(this.baseUrl + userId + "/friendship/" + recipientId, {});
  }

  getFriends(userId){
    return this.http.get<User[]>(this.baseUrl + userId + '/friendship/');
  }

  // getRequestedFriends(userId){
  //   return this.http.get<User[]>()
  // }

  deleteFriend(userId: number, recipientId: number){
    console.log("delete");
    return this.http.delete(this.baseUrl + userId + "/friendship/" + recipientId + '/');
  }
}
