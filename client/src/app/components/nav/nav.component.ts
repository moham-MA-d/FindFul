import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { User } from 'src/app/models/user/user';
import { AccountService } from 'src/app/services/account/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  user: User;
  currentUser$!: Observable<User>;

  constructor(public accountService: AccountService) {
    accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;
  }
  logout() {
    this.accountService.logout();
    //this.router.navigateByUrl('/welcome');
  }

}
