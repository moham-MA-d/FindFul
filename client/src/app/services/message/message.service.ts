import { HttpClient } from '@angular/common/http';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/internal/operators/map';
import { CreateMessage } from 'src/app/models/message/createMessage';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  SendMessage(model: CreateMessage) {
    return this.http.post(this.baseUrl + 'message/addmessage', model)
    .pipe(map((response: Message) => {
      return response;
    }))
  }

  GetChats(){
    return this.http.get(this.baseUrl + 'message/getchats')
    .pipe(map((response: any) => {
      return response;
    }))
  }

  GetMessages(userId: number){
    return this.http.get(this.baseUrl + 'message/getmessages/' + userId)
    .pipe(map((response: any) => {
      return response;
    }))
  }

}
