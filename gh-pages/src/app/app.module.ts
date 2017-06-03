import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { GeneralContentComponent } from './general/general-content.component';
import { MenuContentComponent } from './menu/menu-content.component';
import { OptionContentComponent } from './option/option-content.component';

@NgModule({
  declarations: [
    AppComponent,
    GeneralContentComponent,
    MenuContentComponent,
    OptionContentComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
