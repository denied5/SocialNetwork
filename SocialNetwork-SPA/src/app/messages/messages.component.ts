import { Component, OnInit } from '@angular/core';
import { Message } from '../_model/Message';
import { Pagination, PaginatedResult } from '../_model/pagination';
import { MessagesResolver } from '../_resolver/messages.resolver';
import { MessageService } from '../Services/message.service';
import { AuthService } from '../Services/Auth.service';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../Services/alertify.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
  messages: Message[];
  pagination: Pagination;
  userId: number;

  constructor(private messageService: MessageService, private authService: AuthService,
    private route: ActivatedRoute, private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe( data => {
      this.messages = data.messages.result;
      this.pagination = data.messages.pagination;
    })
    this.userId = this.authService.decodedToken.nameid;
  }

  loadMessages(){
    this.messageService.getMessages(this.userId, this.pagination.CurrentPage, 
        this.pagination.ItemsPerPage)
          .subscribe((res: PaginatedResult<Message[]>) => {
            this.messages = res.result;
            this.pagination = res.pagination;
          }, error => {
            this.alertify.error(error);
          });
  }

  deleteMessage(id: number){
    this.alertify.confirm('Are you sure', () => {
      this.messageService.deleteMessage(id, this.authService.decodedToken.nameid).subscribe(() => {
        this.messages.splice(this.messages.findIndex(m => m.id === id), 1);
        this.alertify.success('message deleted');
      }, error => {
        this.alertify.error(error);
      })
    })
  }

  pageChanged(event: any): void {
    this.pagination.CurrentPage = event.page;
    this.loadMessages();
  }
}
