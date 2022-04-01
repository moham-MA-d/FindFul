import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CenterComponent } from './center.component';
import { RouterModule } from '@angular/router';
import { HomeModule } from 'src/app/components/home/home.module';
import { MembersComponent } from 'src/app/components/members/members.component';
import { ProfileAboutComponent } from 'src/app/components/members/Profile/profileAbout/profileAbout.component';
import { ProfileTimelineComponent } from 'src/app/components/members/Profile/profileTimeline/profileTimeline.component';
import { ProfileHeaderComponent } from 'src/app/components/members/Profile/profileHeader/profileHeader.component';
import { ProfileEditComponent } from 'src/app/components/members/Profile/profileEdit/profileEdit.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProfileAlbumComponent } from 'src/app/components/members/Profile/profileAlbum/profileAlbum.component';
import { ProfileAlbumPhotoEditComponent } from 'src/app/components/members/Profile/ProfileEdit/profileAlbumPhotoEdit/profileAlbumPhotoEdit.component';
import { SnippetComponentsModule } from '../../snippet-components.module';
import { ProfilePhotoEditComponent } from 'src/app/components/members/Profile/ProfileEdit/profilePhotoEdit/profilePhotoEdit.component';
import { ProfileEditBasicInfoComponent } from 'src/app/components/members/Profile/ProfileEdit/profileEditBasicInfo/profileEditBasicInfo.component';
import { SharedModule } from '../../shared.module';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    HomeModule,
    FormsModule,
    SnippetComponentsModule,
    ReactiveFormsModule,
    SharedModule
  ],
  declarations: [
    CenterComponent,
    MembersComponent,
    ProfileAboutComponent,
    ProfileTimelineComponent,
    ProfileHeaderComponent,
    ProfileEditComponent,
    ProfileAlbumComponent,
    ProfileAlbumPhotoEditComponent,
    ProfilePhotoEditComponent,
    ProfileEditBasicInfoComponent,
  ],
  exports: [
    CenterComponent,
    MembersComponent,
    ProfileAboutComponent,
    ProfileTimelineComponent,
    ProfileHeaderComponent,
    ProfileEditComponent,
    ProfileAlbumComponent,
    ProfileAlbumPhotoEditComponent,
    ProfilePhotoEditComponent,
    ProfileEditBasicInfoComponent,
  ]
})
export class CenterModule { }
