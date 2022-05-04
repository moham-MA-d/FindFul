import { Component, EventEmitter, Inject, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from 'src/app/models/user/member';
import { MemberService } from 'src/app/services/member/member.service';
import { ComponentService } from 'src/app/services/component/component.service';
import { UserParameters } from 'src/app/models/user/userParameters';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatDialogComponent } from 'src/app/components/snippets/mat-dialog/mat-dialog.component';
import { MessageService } from 'src/app/services/message/message.service';
import { CreateMessage } from 'src/app/models/message/createMessage';
import { ToastrService } from 'ngx-toastr';
import { data } from 'jquery';

@Component({
  selector: 'app-profileHeader',
  templateUrl: './profileHeader.component.html',
  styleUrls: ['./profileHeader.component.css']
})
export class ProfileHeaderComponent implements OnInit {

  @Output() user = new EventEmitter();
  member: Member = new Member();

  constructor(
    private route: ActivatedRoute,
    private dialog: MatDialog,
    private memberService: MemberService,
    private compService: ComponentService,
    private messageService: MessageService,
    private tosterService: ToastrService
  ) { }

  ngOnInit() {
    this.compService.setChatBlockEnable(false);
    this.loadMember();
  }

  ngOnDestroy() {
    this.compService.setChatBlockEnable(true);
  }

  loadMember() {
    let username = this.route.snapshot.paramMap.get("username");
    this.memberService.getMember(username).subscribe(mem => {
      this.member = mem;
      if (mem.userName == username) {
        this.memberService.currentMember = mem;
      } else {
        this.memberService.currentMember = undefined;
      }
      this.user.emit(this.member);
    });
  }

  followUnfollow() {
    let username = this.route.snapshot.paramMap.get("username");
    this.memberService.follow(username).subscribe((r: any) => {
      if (r.data == "follow") {
        this.member.isFollowed = true;
      } else {
        this.member.isFollowed = false;
      }
    });
  }


  openDialog() {
    let dialogRef = this.dialog.open(MatDialogComponent, {
      data: {
        dialogMessage: 'Write a message to ' + this.member.firstName + ' ' + this.member.lastName,
        buttonText: {
          ok: 'Send',
          cancel: 'Cancel'
        },
        label: 'Leave a message',
        example: 'Hello ' + this.member.firstName + ' ' + '...',
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      let username = this.route.snapshot.paramMap.get("username");
      const createMessage : CreateMessage = {
        body: result,
        recieverUsername: username
      }
      this.messageService.SendMessage(createMessage).subscribe(r => {
        if (r) {
          this.tosterService.success("your message succesfully sent to " + this.member.firstName)
        }
      });
    });
  }
}
