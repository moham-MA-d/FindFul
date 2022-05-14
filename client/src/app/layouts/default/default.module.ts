import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LeftModule } from 'src/app/modules/components-wrapper/left/left.module';
import { DefaultComponent } from './default.component';
import { SharedModule } from 'src/app/modules/shared.module';
import { CenterModule } from 'src/app/modules/components-wrapper/center/center.module';
import { SnippetComponentsModule } from 'src/app/modules/snippet-components.module';
import { AdminMainModule } from 'src/app/modules/admin/adminMain/adminMain.module';



@NgModule({
  declarations: [
    DefaultComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    SharedModule,
    LeftModule,
    CenterModule,
    SnippetComponentsModule,
    AdminMainModule

  ]
})
export class DefaultModule { }
