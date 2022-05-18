import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostCommentComponent } from '../components/snippets/post-comment/post-comment.component';
import { StickySidebarRComponent } from '../components/snippets/sticky-sidebar-r/sticky-sidebar-r.component';
import { PhotoUploadComponent } from '../components/snippets/photo-upload/photo-upload.component';
import { FileUploadModule } from 'ng2-file-upload';
import { PhotoUpload2Component } from '../components/snippets/photo-upload2/photo-upload.component';
import { MatRangeSliderComponent } from '../components/snippets/mat-range-slider/mat-range-slider.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogComponent } from '../components/snippets/mat-dialog/mat-dialog.component';
import { SharedModule } from './shared.module';
import { MaterialModule } from './material/materialModule/material.module';


@NgModule({

  imports: [
    CommonModule,
    FileUploadModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    MaterialModule
  ],

  declarations: [
    PostCommentComponent,
    StickySidebarRComponent,
    PhotoUploadComponent,
    PhotoUpload2Component,
    MatRangeSliderComponent,
    MatDialogComponent,
  ],

  exports:[
    PostCommentComponent,
    StickySidebarRComponent,
    PhotoUploadComponent,
    PhotoUpload2Component,
    FileUploadModule,
    MatRangeSliderComponent,
    MatDialogComponent,
  ]
})
export class SnippetComponentsModule { }
