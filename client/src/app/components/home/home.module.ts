import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreatePostComponent } from '../snippets/create-post/create-post.component';
import { PostContentComponent } from '../snippets/post-content/post-content.component';
import { HomeComponent } from './home.component';



@NgModule({
  declarations: [
    HomeComponent,
    CreatePostComponent,
    PostContentComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    HomeComponent,
    CreatePostComponent,
    PostContentComponent
  ]
})
export class HomeModule { }