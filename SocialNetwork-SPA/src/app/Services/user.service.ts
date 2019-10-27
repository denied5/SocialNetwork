import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../_model/User';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiURL + 'users/';
constructor(private http: HttpClient) { }

  getUsers(){
    return this.http.get<User[]>(this.baseUrl );
  }

  getUser(id){
    return this.http.get<User>(this.baseUrl + id);
  }

  putUser(id: number, user: User) {
    return this.http.put(this.baseUrl + id, user);
  }

}
