import { Component } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  activeTab: string = "volunteer"
  
  onTabClick(activeTabName) {
  this.activeTab = activeTabName;
  }
}
