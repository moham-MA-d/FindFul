import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/models/user/member';
import { User } from 'src/app/models/user/user';
import { AccountService } from 'src/app/services/account/account.service';
import { MemberService } from 'src/app/services/member/member.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  member: Member = new Member();
  user: User;
  currentUser$!: Observable<User>;

  constructor(public accountService: AccountService, private router: Router, private memberService: MemberService) { 
    accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;
    this.loadMember();
  }
  logout() {
    this.accountService.logout();
    //this.router.navigateByUrl('/welcome');
  }

  
  loadMember(){
    this.memberService.getMember(this.user.userName).subscribe(member => {
      this.member = member;
    });
  }

}
