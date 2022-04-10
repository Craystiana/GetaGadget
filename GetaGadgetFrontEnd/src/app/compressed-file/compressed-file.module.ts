import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { CompressedFilePageRoutingModule } from './compressed-file-routing.module';

import { CompressedFilePage } from './compressed-file.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    CompressedFilePageRoutingModule
  ],
  declarations: [CompressedFilePage]
})
export class CompressedFilePageModule {}
