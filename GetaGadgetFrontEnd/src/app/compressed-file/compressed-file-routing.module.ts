import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CompressedFilePage } from './compressed-file.page';

const routes: Routes = [
  {
    path: '',
    component: CompressedFilePage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CompressedFilePageRoutingModule {}
