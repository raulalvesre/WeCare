import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

//import comps
import { ContentComponent } from './home/content/content.component';
import { FormsComponent } from './users/forms/forms.component';
import { InstitutionsFormsComponent } from './institutions/forms/forms.component';
import { AboutComponent } from './home/about/about.component';
import { LoginComponent } from './home/login/login.component';
import { UsermainComponent } from './users/usermain/usermain.component';
import { CausesComponent } from './home/causes/causes.component';
import { ProfileTypeComponent } from './users/forms/profile-type/profile-type.component';
import { ProfileComponent } from './users/profile/profile.component';
import { SearchOportunityComponent } from './users/search-oportunity/search-oportunity.component';
import { InstitutionsIssuesComponent } from './adm/institutions-issues/institutions-issues.component';
import { UsersIssuesComponent } from './adm/users-issues/users-issues.component';



const routes: Routes = [
  {
    path: 'home',
    component: ContentComponent
  },
  {
    path: 'user/create',
    component: FormsComponent
  },
  {
    path: 'institution/create',
    component: InstitutionsFormsComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'about',
    component: AboutComponent
  },
  {
    path: 'profile',
    component: UsermainComponent
  },
  {
    path: 'causes',
    component: CausesComponent
  },
  {
    path: 'profile-type',
    component: ProfileTypeComponent
  },
  {
    path: 'user-profile',
    component: ProfileComponent
  },
  {
    path: 'user-search-oportunity',
    component: SearchOportunityComponent
  },
  {
    path: 'issues-users',
    component: UsersIssuesComponent
  },
  {
    path: 'issues-institutions',
    component: InstitutionsIssuesComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
