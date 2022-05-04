import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CenterComponent } from './center.component';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../../shared.module';
import { TimeagoModule } from 'ngx-timeago';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomeModule } from 'src/app/components/home/home.module';
import { MembersComponent } from 'src/app/components/members/members.component';
import { ProfileAboutComponent } from 'src/app/components/members/Profile/profileAbout/profileAbout.component';
import { ProfileTimelineComponent } from 'src/app/components/members/Profile/profileTimeline/profileTimeline.component';
import { ProfileHeaderComponent } from 'src/app/components/members/Profile/profileHeader/profileHeader.component';
import { ProfileEditComponent } from 'src/app/components/members/Profile/profileEdit/profileEdit.component';
import { ProfileAlbumComponent } from 'src/app/components/members/Profile/profileAlbum/profileAlbum.component';
import { ProfileAlbumPhotoEditComponent } from 'src/app/components/members/Profile/ProfileEdit/profileAlbumPhotoEdit/profileAlbumPhotoEdit.component';
import { SnippetComponentsModule } from '../../snippet-components.module';
import { ProfilePhotoEditComponent } from 'src/app/components/members/Profile/ProfileEdit/profilePhotoEdit/profilePhotoEdit.component';
import { ProfileEditBasicInfoComponent } from 'src/app/components/members/Profile/ProfileEdit/profileEditBasicInfo/profileEditBasicInfo.component';
import { MatDatepickerModule } from '@angular/material/datepicker/';
import { MatNativeDateModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatSelectModule } from '@angular/material/select';
import { EnumToArrayPipe } from 'src/app/helper/EnumToArrayPipe';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSliderModule } from '@angular/material/slider';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MemberCardComponent } from 'src/app/components/snippets/member-card/member-card.component';
import { FollowingComponent } from 'src/app/components/members/Profile/following/following.component';
import { FollowersComponent } from 'src/app/components/members/Profile/followers/followers.component';
import { MessagesComponent } from 'src/app/components/messages/messages.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    HomeModule,
    FormsModule,
    SnippetComponentsModule,
    ReactiveFormsModule,
    SharedModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatFormFieldModule,
    MatInputModule,
    MatAutocompleteModule,
    MatSelectModule,
    MatPaginatorModule,
    MatSliderModule,
    MatButtonModule,
    MatButtonToggleModule,
    TimeagoModule.forRoot()
  ],
  declarations: [
    CenterComponent,
    MembersComponent,
    MemberCardComponent,
    FollowingComponent,
    FollowersComponent,
    ProfileAboutComponent,
    ProfileTimelineComponent,
    ProfileHeaderComponent,
    ProfileEditComponent,
    ProfileAlbumComponent,
    ProfileAlbumPhotoEditComponent,
    ProfilePhotoEditComponent,
    ProfileEditBasicInfoComponent,
    MessagesComponent,
    EnumToArrayPipe
  ],
  exports: [
    CenterComponent,
    MembersComponent,
    MemberCardComponent,
    FollowingComponent,
    FollowersComponent,
    ProfileAboutComponent,
    ProfileTimelineComponent,
    ProfileHeaderComponent,
    ProfileEditComponent,
    ProfileAlbumComponent,
    ProfileAlbumPhotoEditComponent,
    ProfilePhotoEditComponent,
    ProfileEditBasicInfoComponent,
    MessagesComponent,
    EnumToArrayPipe,
    TimeagoModule

  ],
  providers: [MatDatepickerModule, MatNativeDateModule]
})
export class CenterModule { }
