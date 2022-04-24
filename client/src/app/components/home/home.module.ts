import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreatePostComponent } from '../snippets/create-post/create-post.component';
import { PostContentComponent } from '../snippets/post-content/post-content.component';
import { HomeComponent } from './home.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import {MatDividerModule} from '@angular/material/divider';
import {MatIconModule} from '@angular/material/icon';



@NgModule({
  declarations: [
    HomeComponent,
    CreatePostComponent,
    PostContentComponent,

  ],
  imports: [
    FormsModule,
    CommonModule,
    CKEditorModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatDividerModule,
    MatIconModule

  ],
  exports: [
    HomeComponent,
    CreatePostComponent,
    PostContentComponent
  ]
})
export class HomeModule { }
