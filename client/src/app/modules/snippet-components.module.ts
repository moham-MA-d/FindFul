import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostCommentComponent } from '../components/snippets/post-comment/post-comment.component';
import { StickySidebarRComponent } from '../components/snippets/sticky-sidebar-r/sticky-sidebar-r.component';
import { PhotoUploadComponent } from '../components/snippets/photo-upload/photo-upload.component';
import { FileUploadModule } from 'ng2-file-upload';
import { PhotoUpload2Component } from '../components/snippets/photo-upload2/photo-upload.component';
import { MatRangeSliderComponent } from '../components/snippets/mat-range-slider/mat-range-slider.component';
import {MatSliderModule} from '@angular/material/slider';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



@NgModule({

  imports: [
    CommonModule,
    FileUploadModule,
    MatSliderModule,
    FormsModule,
    ReactiveFormsModule
  ],

  declarations: [
    PostCommentComponent,
    StickySidebarRComponent,
    PhotoUploadComponent,
    PhotoUpload2Component,
    MatRangeSliderComponent
  ],

  exports:[
    PostCommentComponent,
    StickySidebarRComponent,
    PhotoUploadComponent,
    PhotoUpload2Component,
    FileUploadModule,
    MatRangeSliderComponent
  ]
})
export class SnippetComponentsModule { }
