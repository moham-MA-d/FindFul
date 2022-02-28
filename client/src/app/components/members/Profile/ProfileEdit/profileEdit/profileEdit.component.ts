import { Component, OnInit } from '@angular/core';
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

  member: Member = new Member();
  user: User;

  constructor(private accountService: AccountService, private memberService: MemberService) {
    accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }


  ngOnInit() {
    this.loadMember();
    console.log("M", this.member);
  }

  loadMember(){
    this.memberService.getMember(this.user.userName).subscribe(member => {
      console.log("M1", member);
      this.member = member;
    });
  }
}
