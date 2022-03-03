import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CenterComponent } from './center.component';
import { RouterModule } from '@angular/router';
import { HomeModule } from 'src/app/components/home/home.module';
import { MembersComponent } from 'src/app/components/members/members.component';
import { ProfileAboutComponent } from 'src/app/components/members/Profile/profileAbout/profileAbout.component';
import { ProfileTimelineComponent } from 'src/app/components/members/Profile/profileTimeline/profileTimeline.component';
import { ProfileHeaderComponent } from 'src/app/components/members/Profile/profileHeader/profileHeader.component';
import { ProfileEditComponent } from 'src/app/components/members/Profile/ProfileEdit/profileEdit/profileEdit.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    HomeModule,
    FormsModule
  ],
  declarations: [
    CenterComponent,
    MembersComponent,
    ProfileAboutComponent,
    ProfileTimelineComponent,
    ProfileHeaderComponent,
    ProfileEditComponent,
  ],
  exports: [
    CenterComponent,
    MembersComponent,
    ProfileAboutComponent,
    ProfileTimelineComponent,
    ProfileHeaderComponent,
    ProfileEditComponent
  ]
})
export class CenterModule { }
