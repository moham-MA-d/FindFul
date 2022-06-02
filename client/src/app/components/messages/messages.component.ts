import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { take } from 'rxjs/operators';
import { CreateMessage } from 'src/app/models/message/createMessage';
import { MemberChat } from 'src/app/models/message/memberChat';
import { Message } from 'src/app/models/message/message';
import { Member } from 'src/app/models/user/member';
import { User, UserToken } from 'src/app/models/user/user';
import { AccountService } from 'src/app/services/account/account.service';
import { MessageService } from 'src/app/services/message/message.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
  @ViewChild('chatBox') private chatBox: ElementRef;
  memberChat: MemberChat[];
  messages: Message[];
  user: User;
  userToken: UserToken;
  chatId: number;
  messageText: string;
  selectedMember: Member;
  skip = 0;
  tmpDisableScroll: boolean = false;

  constructor(private messageService: MessageService, private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(u => {
      this.user = u;
    })
  }

  ngOnInit(): void {
    this.GetChats();
  };


  GetChats() {

    this.messageService.GetChats()
      .subscribe((r: MemberChat[]) => {
        this.memberChat = r;
        this.skip = 0;
      });


  }

  GetMessages(userId: number) {
    this.tmpDisableScroll = true;
    if (this.chatId != userId) {
      this.messages = [];
      this.skip = 0;
    }
    this.chatId = userId;
    this.messageService.GetMessages(userId, this.skip).subscribe(
      {
        next: (r: Message[]) => {
          if (r) {
            if (this.messages != null) {
              this.messages = r.concat(this.messages);
            } else {
              this.messages = r;
            }
            console.log("Messages: ", this.messages);
          }
        },
        complete: () => {
          this.chatBox.nativeElement.scrollTop = 4;
          this.skip += 20;
          setTimeout(() => {
            this.tmpDisableScroll = false;
          }, 10);
        },
        error() { console.log("Erorr in getting message"); }
      })
  }

  SendMessage() {
    const createMessage: CreateMessage = {
      body: this.messageText,
      receiverId: this.chatId
    }
    this.messageService.SendMessage(createMessage).subscribe((r: any) => {
      if (r) {
        this.messages.push(r);
        this.messageText = "";
        this.scrollToBottom();
      }
    });
  }

  onScroll(event: any) {
    if (this.tmpDisableScroll === false) {
      // visible height + pixel scrolled >= total height
      if (event.target.scrollTop == 0) {
        this.GetMessages(this.chatId);
      }
    }
    //End Of The Chat
    if (event.target.offsetHeight + event.target.scrollTop >= event.target.scrollHeight) { }
  }

  scrollToBottom() {
    try {
      this.chatBox.nativeElement.scrollTop = this.chatBox.nativeElement.scrollHeight;
    } catch (err) { }
  }

}

