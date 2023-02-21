import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

//import comps
import { ContentComponent } from './home/content/content.component';
import { FormsComponent } from './users/forms/forms.component';
import { AboutComponent } from './home/about/about.component';
import { LoginComponent } from './home/login/login.component';

const routes: Routes = [
  { path: 'home', 
  component: ContentComponent
  },
  { path: 'user/create', 
  component: FormsComponent
  },
  { path: 'login', 
  component: LoginComponent
  },
  { path: 'about', 
  component: AboutComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
