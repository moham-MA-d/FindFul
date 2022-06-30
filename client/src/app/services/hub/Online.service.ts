import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpTransportType, HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { User, UserToken } from 'src/app/models/user/user';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})

export class OnlineService {
  hubUrl = environment.hubUrl;
  private hubConnection: HubConnection;
  // A variant of Subject (Gemeroc Observable) that requires an initial value and emits its current value whenever it is subscribed to.
  private onlineUsersSource = new BehaviorSubject<string[]>([]);
  onlineUsers$ = this.onlineUsersSource.asObservable();

  constructor(private toastr: ToastrService) { }

  // it is not an HttpRequest so we cannot use `JwtInterceptor.ts`
  createHubConnection(userToken: UserToken) {
    this.hubConnection = new HubConnectionBuilder()
      .configureLogging(LogLevel.Debug)
      .withUrl(this.hubUrl + 'online', {
        //skipNegotiation: true,
        // transport: should be enable on windows server
        //transport: HttpTransportType.WebSockets,
        //accessTokenFactory: return a string that contains access token and actually is userToken.Token
        accessTokenFactory: () => userToken.token

      })
      .withAutomaticReconnect()
      .build()
      console.log("SignalR: createHubConnection");

    this.hubConnection
      .start()
      .catch(error => console.log("SignalR: ", error));

    //Listen to sever events: UserIsOnline
    this.hubConnection.on('UserIsOnline', username => {
      // this.onlineUsers$.pipe(take(1)).subscribe(usernames => {
      //   this.onlineUsersSource.next([...usernames, username])
      // })
      console.log("UserIsOnline : ", username);
      this.toastr.info(username + 'is online')
    })


    //Listen to sever events: UserIsOffline
    this.hubConnection.on('UserIsOffline', username => {
      // this.onlineUsers$.pipe(take(1)).subscribe(usernames => {
      //   this.onlineUsersSource.next([...usernames.filter(x => x !== username)])
      // })
      console.log("UserIsOffline : ", username );
      this.toastr.info(username + 'is offline')
    })

    this.hubConnection.on('GetOnlineUsers', (usernames: string[]) => {
      console.log("GetOnlineUsers : ", usernames);
      // update onlineUsersSource with list of current users.
      this.onlineUsersSource.next(usernames);
    })

    // this.hubConnection.on('NewMessageReceived', ({username, knownAs}) => {
    //   this.toastr.info(knownAs + ' has sent you a new message!')
    //     .onTap
    //     .pipe(take(1))
    //     .subscribe(() => this.router.navigateByUrl('/members/' + username + '?tab=3'));
    // })
  }

  stopHubConnection() {
    if(this.hubConnection) {
      console.log("SignalR: stop");
      this.hubConnection.stop().catch(error => console.log(error));
    }
  }
}
