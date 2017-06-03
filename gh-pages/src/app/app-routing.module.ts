import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GeneralContentComponent } from './general/general-content.component';
import { MenuContentComponent } from './menu/menu-content.component';
import { OptionContentComponent } from './option/option-content.component';

const routes: Routes = [
  { path: '', component: GeneralContentComponent },
  { path: 'menu', component: MenuContentComponent },
  { path: 'option', component: OptionContentComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
