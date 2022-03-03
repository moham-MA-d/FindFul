import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/guards/authentication/auth.guard';
import { LoginComponent } from './components/forms/login/login.component';
import { RegisterComponent } from './components/forms/register/register.component';
import { HomeComponent } from './components/home/home.component';
import { LandingRegisterComponent } from './components/landings/landing-register/landing-register.component';
import { MembersComponent } from './components/members/members.component';
import { ProfileAboutComponent } from './components/members/Profile/profileAbout/profileAbout.component';
import { ProfileEditComponent } from './components/members/Profile/ProfileEdit/profileEdit/profileEdit.component';
import { ProfileTimelineComponent } from './components/members/Profile/profileTimeline/profileTimeline.component';
import { MessagesComponent } from './components/messages/messages.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
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
          { path: 'members/:username', component: ProfileTimelineComponent },
          { path: 'members/:username/edit', component: ProfileEditComponent, canDeactivate: [PreventUnsavedChangesGuard],  runGuardsAndResolvers: 'always', },
          { path: 'members/:username/timeline', component: ProfileTimelineComponent },
          { path: 'members/:username/about', component: ProfileAboutComponent },
          { path: 'messages', component: MessagesComponent }
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
