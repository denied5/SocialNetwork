import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Post } from '../_model/Post';
import { PaginatedResult } from '../_model/pagination';
import { map } from 'rxjs/operators';
import { PostComment } from '../_model/PostComment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(private http: HttpClient) { }
  baseUrl = environment.apiURL;

  getPosts(userId: number):Observable<Post[]> {
    return this.http.get<Post[]>(this.baseUrl + 'users/' + userId + '/posts/');
  }

  createPost(userId: number, post: Post) {
    return this.http.post(this.baseUrl + 'users/' + userId + '/posts/', post);
  }

  deletePost(userId: number, postId: number) {
    return this.http.delete(this.baseUrl + 'users/' + userId + '/posts/' + postId);
  }

  getFeed(userId: number, page?, itemsPerPage?) {
    const paginatedResult: PaginatedResult<Post[]> = new PaginatedResult<Post[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('currentPage', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<Post[]>(this.baseUrl + 'users/' + userId + '/posts/feed', { observe: 'response', params })
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

  setLike(postId: number, userId) {
    return this.http.post(this.baseUrl + 'posts/' + postId + '/like/' + userId, {});
  }

  deleteLike(postId: number, userId: number) {
    return this.http.delete(this.baseUrl + 'posts/' + postId + '/like/' + userId);
  }

  createComment(content: string, postId: number, userId: number) {
    const comment = {
      content,
      postId,
      userId
    }
    return this.http.post(this.baseUrl + 'comment', comment);
  }

  deleteComment(commentId: number) {
    return this.http.delete(this.baseUrl + 'comment/' + commentId);
  }
}
