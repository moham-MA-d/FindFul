import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from 'src/app/models/user/member';
import { MemberService } from 'src/app/services/member/member.service';
import { ComponentService } from 'src/app/services/component/component.service';

@Component({
  selector: 'app-profileHeader',
  templateUrl: './profileHeader.component.html',
  styleUrls: ['./profileHeader.component.css']
})
export class ProfileHeaderComponent implements OnInit {

  @Output() user = new EventEmitter();
  member: Member = new Member();

  constructor(private memberService: MemberService, private route: ActivatedRoute, private compService: ComponentService) { }

  ngOnInit() {
    this.compService.setChatBlockEnable(false);
    this.loadMember();
  }
  ngOnDestroy()	{
    this.compService.setChatBlockEnable(true); 
  }


  loadMember(){
    let username = this.route.snapshot.paramMap.get("username");
    this.memberService.getMember(username).subscribe(mem => {
      this.member = mem;
      this.user.emit(this.member);
    });
  }
}
