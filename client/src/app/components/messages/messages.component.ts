import { Component, OnInit } from '@angular/core';
import { MemberChat } from 'src/app/models/message/memberChat';
import { MessageService } from 'src/app/services/message/message.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
  memberChat: MemberChat[];
  constructor(private messageService: MessageService) { }

  ngOnInit(): void {
   this.GetChats();

  }

  GetChats() {
    this.messageService.GetChats()
    .subscribe((r: MemberChat[]) => {
      this.memberChat = r;
    });
  }


  GetMessages(userId: number) {
    this.messageService.GetMessages(userId).subscribe(r => {

    })
  }

}
