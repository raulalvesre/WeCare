import { Component } from '@angular/core';
import { User } from '../shared/user'; 
import { FormGroup, FormControl }  from '@angular/forms'; 


@Component({
  selector: 'app-forms',
  templateUrl: './forms.component.html',
  styleUrls: ['./forms.component.css']
})
export class FormsComponent {

  formUser: FormGroup;
  userName = '';

  ngOnInit(){
    this.creatForm(new User());
  }

  creatForm(user: User){
    user.name = 'teste'
    this.formUser = new FormGroup({
      name: new FormControl(user.name),
      cpf: new FormControl(user.cpf)
    })
  }
}
