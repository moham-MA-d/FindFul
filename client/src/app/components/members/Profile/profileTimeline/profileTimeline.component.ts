import { Component, OnInit } from '@angular/core';
import { Member } from 'src/app/models/user/member';

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
