import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostCommentComponent } from '../components/snippets/post-comment/post-comment.component';
import { StickySidebarRComponent } from '../components/snippets/sticky-sidebar-r/sticky-sidebar-r.component';



@NgModule({
  declarations: [
    PostCommentComponent,
    StickySidebarRComponent
  ],
  imports: [
    CommonModule,
    
  ],
  exports:[
    PostCommentComponent,
    StickySidebarRComponent
  ]
})
export class SnippetComponentsModule { }
