import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from 'src/app/models/user/member';
import { MemberService } from 'src/app/services/member/member.service';

@Component({
  selector: 'app-profileAbout',
  templateUrl: './profileAbout.component.html',
  styleUrls: ['./profileAbout.component.css']
})
export class ProfileAboutComponent implements OnInit {

  member: Member;
  constructor() { }

  ngOnInit() {
  }

  getUserInfo(event: Member){
    this.member = event;
  }
}
