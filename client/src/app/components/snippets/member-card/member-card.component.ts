import { Component, Input, OnInit } from '@angular/core';
import { UserEnums } from 'src/app/enum/userEnums';
import { Member } from 'src/app/models/user/member';
import { OnlineService } from 'src/app/services/hub/Online.service';
import { MemberService } from 'src/app/services/member/member.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {

  @Input() member: Member;
  enumGenderValues = UserEnums.Gender;

  constructor(private memberService: MemberService, public onlineService: OnlineService) { }

  ngOnInit() {
  }

  followUnfollow(member: Member) {
    this.memberService.follow(member.userName).subscribe((r:any) => {
      if (r.data == "follow") {
        member.isFollowed = true;
      } else {
        member.isFollowed = false;
      }
    });
  }

  getEnumKeyByEnumValue(myEnum:any, enumValue: number | string): string {
    let keys = Object.keys(myEnum).filter((x) => myEnum[x] == enumValue);
    return keys.length > 0 ? keys[0].replace(/([A-Z])/g, ' $1').trim() : '';
  }
}
