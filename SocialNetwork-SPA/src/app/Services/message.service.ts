import { Injectable } from '@angular/core';
import { PaginatedResult } from '../_model/pagination';
import { Message } from '../_model/Message';
import { HttpParams, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  baseUrl = environment.apiURL + 'users/';
  constructor(private http: HttpClient) { }

  getMessages(id: number, page?, itemsPerPage?) {
    const paginatedResult: PaginatedResult<Message[]> = new PaginatedResult<Message[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('currentPage', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<Message[]>(this.baseUrl + id + '/messages', { observe: 'response', params })
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

  getMessageThread(id: number, recipientId: number) {
    if (id != recipientId) {
      return this.http.get<Message[]>(this.baseUrl + id + '/messages/thread/' + recipientId);
    }
  }

  sendMessage(id: number, message: Message) {
    if (id != message.recipientId) {
      return this.http.post(this.baseUrl + id + '/messages', message);
    }
  }

  deleteMessage(id: number, userId: number) {
    return this.http.delete(this.baseUrl + userId + '/messages/' + id);
  }

  markAsRead(userId: number, messageId: number) {
    return this.http.post(this.baseUrl + userId + '/messages/' + messageId + '/read', {})
      .subscribe();
  }
}
