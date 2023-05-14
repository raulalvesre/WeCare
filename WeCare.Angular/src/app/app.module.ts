import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from  '@angular/forms';
import { ReactiveFormsModule } from  '@angular/forms';


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
import { CausesComponent } from './home/causes/causes.component';
import { ProfileTypeComponent } from './users/forms/profile-type/profile-type.component';
import { ProfileComponent } from './users/profile/profile.component';
import { SearchOportunityComponent } from './users/search-oportunity/search-oportunity.component';
import { UsersIssuesComponent } from './adm/users-issues/users-issues.component';
import { InstitutionsIssuesComponent } from './adm/institutions-issues/institutions-issues.component';


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
    CausesComponent,
    ProfileTypeComponent,
    ProfileComponent,
    SearchOportunityComponent,
    UsersIssuesComponent,
    InstitutionsIssuesComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
