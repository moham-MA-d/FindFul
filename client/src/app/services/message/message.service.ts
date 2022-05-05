import { HttpClient } from '@angular/common/http';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/internal/operators/map';
import { CreateMessage } from 'src/app/models/message/createMessage';
import { ScrollParameters } from 'src/app/models/page/scrollParameters';
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

  GetMessages(userId: number, skip: number) {
    //const filter = this.turnFilterIntoUrl(filterDto);
    return this.http.get(this.baseUrl + 'message/getmessages?userid=' + userId + "&&skip="+ skip)
    .pipe(map((response: any) => {
      return response;
    }))
  }


  private turnFilterIntoUrl(filterDto?: ScrollParameters) {
    if (!filterDto) {
      return '';
    }

    if (!Object.entries(filterDto).length) {
      return '';
    }

    let urlFilter = '?';

    for (const [key, value] of Object.entries(filterDto)) {
      urlFilter += `${key}=${value}&`;
    }

    return urlFilter.substring(0, urlFilter.length - 1);
  }
}
