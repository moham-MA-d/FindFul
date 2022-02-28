import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user/user';
import { AccountService } from 'src/app/services/account/account.service';

//Using @component decorator means that our TypeScript class now has Angular features.
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

//Tip: Angular components has several life cycle events and the first one is constructors so we inject HttpClient in it
export class AppComponent implements OnInit{
  title = 'client';

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser() {
    var localUser = localStorage.getItem('user');
    if (localUser) {
        const user: User = JSON.parse(localUser);
        this.accountService.setCurrentUser(user);
        this.accountService.isLoggedIn = true;
    }
  }
  
}

