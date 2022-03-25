import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LeftModule } from 'src/app/modules/components-wrapper/left/left.module';
import { DefaultComponent } from './default.component';
import { SharedModule } from 'src/app/modules/shared.module';
import { CenterModule } from 'src/app/modules/components-wrapper/center/center.module';
import { SnippetComponentsModule } from 'src/app/modules/snippet-components.module';



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
    SnippetComponentsModule

  ]
})
export class DefaultModule { }
