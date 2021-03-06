import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/guards/authentication/auth.guard';
import { AdminPanelComponent } from './components/admin/admin-panel/admin-panel.component';
import { LoginComponent } from './components/forms/login/login.component';
import { RegisterComponent } from './components/forms/register/register.component';
import { HomeComponent } from './components/home/home.component';
import { LandingRegisterComponent } from './components/landings/landing-register/landing-register.component';
import { MembersComponent } from './components/members/members.component';
import { FollowersComponent } from './components/members/Profile/followers/followers.component';
import { FollowingComponent } from './components/members/Profile/following/following.component';
import { ProfileAboutComponent } from './components/members/Profile/profileAbout/profileAbout.component';
import { ProfileAlbumComponent } from './components/members/Profile/profileAlbum/profileAlbum.component';
import { ProfileEditComponent } from './components/members/Profile/profileEdit/profileEdit.component';
import { ProfileTimelineComponent } from './components/members/Profile/profileTimeline/profileTimeline.component';
import { MessagesComponent } from './components/messages/messages.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { AdminGuard } from './guards/admin/admin.guard';
import { PreventUnsavedChangesGuard } from './guards/leave/prevent-unsaved-changes.guard';
import { DefaultComponent } from './layouts/default/default.component';
import { CenterComponent } from './modules/components-wrapper/center/center.component';

const routes: Routes = [
   {
    path: '',
    component: DefaultComponent,
    canActivate: [AuthGuard],
    runGuardsAndResolvers: 'always',
    children: [
      {
        path: '', component: CenterComponent,
        children: [
          { path: 'home', component: HomeComponent },
          { path: 'members', component: MembersComponent },
          { path: 'members/:username/edit', component: ProfileEditComponent, canDeactivate: [PreventUnsavedChangesGuard],  runGuardsAndResolvers: 'always', },
          { path: 'members/:username/timeline', component: ProfileTimelineComponent },
          { path: 'members/:username/about', component: ProfileAboutComponent },
          { path: 'members/:username/album', component: ProfileAlbumComponent },
          { path: 'members/:username/following', component: FollowingComponent },
          { path: 'members/:username/followers', component: FollowersComponent },
          { path: 'members/:username', component: ProfileTimelineComponent },
          { path: 'messages', component: MessagesComponent },
          { path: 'admin', component: AdminPanelComponent, canActivate:[AdminGuard] }
        ]
      }
    ]
  },
  { path: 'welcome', component: LandingRegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: '**', component: NotFoundComponent, pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
