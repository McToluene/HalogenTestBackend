import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { UploadComponent } from './upload/upload.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { GenerateComponent } from './generate/generate.component';
import { ResultComponent } from './result/result.component';
import { DownloadComponent } from './download/download.component';
import { NavbarComponent } from './navbar/navbar.component';

@NgModule({
  declarations: [AppComponent, UploadComponent, GenerateComponent, ResultComponent, DownloadComponent, NavbarComponent],
  imports: [BrowserModule, NgbModule, HttpClientModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
