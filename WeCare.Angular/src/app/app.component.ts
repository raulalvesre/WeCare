import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {

  notLogged: boolean = false;

  title = 'We Care';
  name = '';
  cpf = '';
  fone = '';
  adress = '';
}
