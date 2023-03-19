import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from  '@angular/forms';
import { ReactiveFormsModule } from  '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { FormsComponent } from './users/forms/forms.component';
import { FooterComponent } from './home/footer/footer.component';
import { HeaderComponent } from './home/header/header.component';
import { ContentComponent } from './home/content/content.component';
import { NavbarComponent } from './home/navbar/navbar.component';
import { CarouselComponent } from './home/carousel/carousel.component';
import { AppRoutingModule } from './app-routing.module';
import { AboutComponent } from './home/about/about.component';
import { LoginComponent } from './home/login/login.component';
import { UsermainComponent } from './users/usermain/usermain.component';
import { InstitutionsFormsComponent } from './institutions/forms/forms.component';


@NgModule({
  declarations: [
    AppComponent,
    FormsComponent,
    FooterComponent,
    HeaderComponent,
    ContentComponent,
    NavbarComponent,
    CarouselComponent,
    AboutComponent,
    LoginComponent,
    UsermainComponent,
    InstitutionsFormsComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
