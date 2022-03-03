import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from 'src/app/models/user/member';
import { MemberService } from 'src/app/services/member/member.service';

@Component({
  selector: 'app-profileTimeline',
  templateUrl: './profileTimeline.component.html',
  styleUrls: ['./profileTimeline.component.css']
})
export class ProfileTimelineComponent implements OnInit {

  member: Member = new Member();
  constructor() { }

  ngOnInit() {
  }

  getUserInfo(event: Member){
    this.member = event;
  }
}
