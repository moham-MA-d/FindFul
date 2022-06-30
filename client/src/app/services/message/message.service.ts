import { HttpClient } from '@angular/common/http';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/internal/operators/map';
import { take } from 'rxjs/operators';
import { CreateMessage } from 'src/app/models/message/createMessage';
import { Group } from 'src/app/models/message/group';
import { ScrollParameters } from 'src/app/models/page/scrollParameters';
import { UserToken } from 'src/app/models/user/user';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  baseUrl = environment.apiUrl;
  hubUrl = environment.hubUrl;
  private hubConnection: HubConnection;

  // to deal with situation when a message send to this hub we need to cereate an Observable
  private messageThreadSource = new BehaviorSubject<Message[]>([]);
  messageThread$ = this.messageThreadSource.asObservable();

  constructor(private http: HttpClient) { }

  createHubConnection(userToken: UserToken, otherUserId: string, skip: string) {
    //WebSocket = undefined;
    //EventSource = undefined;
    this.hubConnection = new HubConnectionBuilder()
      //.configureLogging(LogLevel.Debug)
      .withUrl(this.hubUrl + 'message?targetUserId=' + otherUserId + '&&skip=' + skip, {
        //skipNegotiation: true,
        // transport: should be enable on windows server
        //transport: HttpTransportType.WebSockets,
        //accessTokenFactory: return a string that contains access token and actually is userToken.Token
        accessTokenFactory: () => userToken.token
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.on('UpdatedGroup', (group: Group) => {
      console.log("Group : ", group);
      if (group.connections.some(x => x.userId === otherUserId)) {
        console.log("Group other user id: ", otherUserId);
        this.messageThread$.pipe(take(1)).subscribe(messages => {
          messages.forEach(message => {
            // if (!message.dateRead) {
            //   message.dateRead = new Date(Date.now())
            // }
          })
          this.messageThreadSource.next([...messages]);
        })
      }
    });

    this.hubConnection.on("NewMessage", message => {
      this.messageThread$.pipe(take(1)).subscribe(messages => {
        this.messageThreadSource.next([...messages, message]);
      })
    });

    // let allMessages;
    // this.hubConnection.on("NewMessage", message => {
    //   this.messageThread$.pipe(take(1)).subscribe(
    //   {
    //     next: (messages: Message[]) => {
    //       allMessages = [...messages,message];
    //     },
    //     complete: () => {
    //       this.messageThreadSource.next(allMessages);
    //     },
    //     error() { console.log("Erorr in getting message"); }
    //   })
    // });

    this.hubConnection.on("ReceiveMessageThread", messages => {
      this.messageThreadSource.next(messages);
    });

    this.hubConnection
      .start()
      .catch(error => console.log("SignalR Message Error : ", error));

  }

  stopHubConnection() {
    console.log("SignalR: stop");
    if (this.hubConnection) {
      this.messageThreadSource.next([]);
      this.hubConnection.stop().catch(error => console.log(error));
    }
  }


  async SendMessageSignalR(model: CreateMessage) {
    // invoke(): call a method in server.
    return this.hubConnection.invoke('SendMessage', model)
      .catch(error => console.log("Error in seding message : ", error));
  }

  SendMessage(model: CreateMessage) {
    return this.http.post(this.baseUrl + 'messages/addmessage', model)
      .pipe(map((response: Message) => {
        return response;
      }))
  }

  GetChats() {
    return this.http.get(this.baseUrl + 'messages/getchats')
      .pipe(map((response: any) => {
        return response;
      }))
  }

  GetMessages(userId: number, skip: number) {
    //const filter = this.turnFilterIntoUrl(filterDto);
    return this.http.get(this.baseUrl + 'messages/getmessages?userid=' + userId + "&&skip=" + skip)
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
