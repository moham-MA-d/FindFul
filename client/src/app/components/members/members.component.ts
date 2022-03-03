import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Member } from 'src/app/models/user/member';
import { MemberService } from 'src/app/services/member/member.service';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.css']
})

export class MembersComponent implements OnInit {
  
  members: Observable<Member[]>;

  constructor(private memberService: MemberService) { }

  ngOnInit(): void {
    this.members = this.memberService.getMembers();
  }

}
