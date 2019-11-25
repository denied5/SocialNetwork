import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { User } from '../_model/User';
import { Photo } from '../_model/Photo';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiURL + 'admin/';
  constructor(private http: HttpClient) { }

  getUsersWithRoles() {
    return this.http.get(this.baseUrl + 'users/roles');
  }

  getPhotosForModeration(): Observable<Photo[]> {
    return this.http.get<Photo[]>(this.baseUrl + 'photos/moderation');
  }

  approvePhoto(photoId: number) {
    return this.http.put(this.baseUrl + 'photos/' + photoId, {});
  }

  updateUserRoles(user: User, roles: {}) {
    return this.http.post(this.baseUrl + 'editRoles/' + user.userName, roles);
  }

  deletePhoto(photoId: number) {
    return this.http.delete(this.baseUrl + 'photos/' + photoId);
  }
}
