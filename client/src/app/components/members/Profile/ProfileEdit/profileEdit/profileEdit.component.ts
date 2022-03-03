import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/models/user/member';
import { User } from 'src/app/models/user/user';
import { AccountService } from 'src/app/services/account/account.service';
import { MemberService } from 'src/app/services/member/member.service';

@Component({
  selector: 'app-profileEdit',
  templateUrl: './profileEdit.component.html',
  styleUrls: ['./profileEdit.component.css']
})
export class ProfileEditComponent implements OnInit {

  //to access browser events
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }
  @ViewChild('editForm') editForm: NgForm;
  user: User;
  member: Member = new Member();

  constructor(private accountService: AccountService,
    private memberService: MemberService,
    private toaster: ToastrService) {
    accountService.currentUser$.pipe(take(1)).subscribe(u => this.user = u);
  }


  ngOnInit() {
    this.loadMember();
  }

  loadMember() {
    this.memberService.getMember(this.user.userName).subscribe(mem => {
      this.member = mem;
    });
  }

  updateMember() {

    return this.memberService.updateMember(this.member).subscribe(() => {
      this.toaster.success("Successfully Updated!");

      // to keep and preserve the values of the form we pass this.member
      this.editForm.reset(this.member);
    });
  }
}
