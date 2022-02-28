import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SideMenuLComponent } from 'src/app/components/snippets/side-menu-l/side-menu-l.component';
import { RouterModule } from '@angular/router';
import { ProfileCardComponent } from 'src/app/components/snippets/profile-card/profile-card.component';
import { ChatBlockComponent } from 'src/app/components/snippets/chat-block/chat-block.component';
import { LeftComponent } from './left.component';


@NgModule({
  declarations: [
    LeftComponent,
    SideMenuLComponent,
    ProfileCardComponent,
    ChatBlockComponent
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports:[
    LeftComponent,
    SideMenuLComponent,
    ProfileCardComponent,
    ChatBlockComponent
  ]
})
export class LeftModule { }
