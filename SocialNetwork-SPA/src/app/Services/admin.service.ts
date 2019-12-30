import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { User } from '../_model/User';
import { Photo } from '../_model/Photo';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../_model/pagination';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiURL + 'admin/';
  constructor(private http: HttpClient) { }

  getUsersWithRoles(page?, itemsPerPage?, userParams?) {
    debugger;
    const paginatedResult: PaginatedResult<User[]> = new PaginatedResult<User[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('currentPage', page);
      params = params.append('pageSize', itemsPerPage);
    }

    if (userParams != null) {
      params = params.append('minAge', userParams.minAge);
      params = params.append('maxAge', userParams.maxAge);
      params = params.append('gender', userParams.gender);
      params = params.append('orderBy', userParams.orderBy);
      params = params.append('name', userParams.name);
    }

    return this.http.get<User[]>(this.baseUrl + 'users/roles', { observe: 'response', params })
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          return paginatedResult;
        })
      );
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
