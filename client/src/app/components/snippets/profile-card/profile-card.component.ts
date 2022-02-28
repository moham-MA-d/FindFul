import { Component, Input, OnInit } from '@angular/core';
import { Member } from 'src/app/models/user/member';

@Component({
  selector: 'app-profile-card',
  templateUrl: './profile-card.component.html',
  styleUrls: ['./profile-card.component.css']
})
export class ProfileCardComponent implements OnInit {

  @Input() member:Member;

  constructor() { }

  ngOnInit(): void {
   
  }

}
