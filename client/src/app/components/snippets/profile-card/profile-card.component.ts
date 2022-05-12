import { Component, Input, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/models/user/member';
import { User } from 'src/app/models/user/user';
import { AccountService } from 'src/app/services/account/account.service';

@Component({
  selector: 'app-profile-card',
  templateUrl: './profile-card.component.html',
  styleUrls: ['./profile-card.component.css']
})
export class ProfileCardComponent implements OnInit {

  @Input() member:Member;
  user: User;

  constructor(private accountService: AccountService) {
    accountService.currentUser$.pipe(take(1)).subscribe(u => this.user = u)
   }

  ngOnInit(): void {

  }

}
