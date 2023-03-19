import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccessService } from 'src/shared/services/access.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  form: FormGroup;

  constructor(private accessService: AccessService) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      email: new FormControl(
        '',
        [Validators.required, Validators.minLength(1)]
      ),
      password: new FormControl(
        '',
        [Validators.required, Validators.minLength(1)]
      )
    });

  }

  login() {
    const { email, password } = this.form.value;
    this.accessService.login({
      email,
      password
    }).subscribe(token => {
      console.log(token);
      console.log(JSON.stringify(token));
    });
  }

}
