import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { take } from 'rxjs/operators';
import { CreateMessage } from 'src/app/models/message/createMessage';
import { MemberChat } from 'src/app/models/message/memberChat';
import { Message } from 'src/app/models/message/message';
import { Member } from 'src/app/models/user/member';
import { User } from 'src/app/models/user/user';
import { AccountService } from 'src/app/services/account/account.service';
import { MessageService } from 'src/app/services/message/message.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
  @ViewChild('chatBox') private myScrollContainer: ElementRef;
  memberChat: MemberChat[];
  messages: Message[];
  user: User;
  chatId: number;
  messageText: string;
  selectedMember: Member;
  skip = 0;

  constructor(private messageService: MessageService, private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      this.user = user;
    })
  }

  ngOnInit(): void {
    this.GetChats();
  };


  GetChats() {
    this.messageService.GetChats()
      .subscribe((r: MemberChat[]) => {
        this.memberChat = r;//r.concat(this.memberChat);
      });
  }

  GetMessages(userId: number) {
    if (this.chatId != userId) {
      this.messages = [];
      this.skip = 0;
    }
    this.chatId = userId;
    this.messageService.GetMessages(userId, this.skip).subscribe(
      {
        next: (r: Message[]) => {
          if (this.messages != null) {
            this.messages = r.concat(this.messages);
          } else {
            this.messages = r;
          }
         },
        complete: () => {
          this.myScrollContainer.nativeElement.scrollTop = 4;
           this.skip += 20;
          },
        error() { console.log("Erorr in getting message"); }
      })
  }

  SendMessage() {
    const createMessage: CreateMessage = {
      body: this.messageText,
      recieverId: this.chatId
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
    // visible height + pixel scrolled >= total height
    if (event.target.scrollTop < 1) {
      this.GetMessages(this.chatId)
    }
    //End Of The Chat
    if (event.target.offsetHeight + event.target.scrollTop >= event.target.scrollHeight) {}
  }

  scrollToBottom() {
    try {
      this.myScrollContainer.nativeElement.scrollTop = this.myScrollContainer.nativeElement.scrollHeight + 220;
    } catch (err) { }
  }

}

