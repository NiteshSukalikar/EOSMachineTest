import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { AppComponent } from "./app.component";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NgxCalendarModule } from "ss-ngx-calendar";
import { HeaderComponent } from "./components/header/header.component";

import { HttpClientModule } from "@angular/common/http";
import { CommonModule } from "@angular/common";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { NgxEventCalendarModule } from "ngx-event-calendar";

@NgModule({
  declarations: [AppComponent, HeaderComponent],
  imports: [
    BrowserModule,
    CommonModule,
    NgxCalendarModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgbModule,
    NgxEventCalendarModule,
    //AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  exports: [ReactiveFormsModule],
})
export class AppModule {}
