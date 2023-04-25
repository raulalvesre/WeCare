import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Realizada, Vaga } from './vaga';
declare var window: any;

@Component({
  selector: 'app-usermain',
  templateUrl: './usermain.component.html',
  styleUrls: ['./usermain.component.css']
})

export class UsermainComponent implements OnInit {
  formModal: any;
  userMenu: string = "news"
  userMenuType: string = "institution"

  ngOnInit(): void {
    this.formModal = new window.bootstrap.Modal(
      document.getElementById('exampleModal')
    );
  }

  openFormModal(modalName) {
    this.formModal =  document.getElementById(modalName)
    this.formModal.show();
  }

  saveSomeThing() {
    // confirm or save something
    this.formModal.hide();
  }

  onMenuClick(option) {
    this.userMenu = option;
  }

  vagas: Array<Vaga> = [
    { name: 'Teste 1', type: 2, date: "22/07/2023", location: "Jabaquara - Sp", description: 'Esse é o teste 1' },
    { name: 'Teste 2', type: 1, date: "22/07/2023", location: "Jabaquara - Sp", description: 'Esse é o teste 2' },
    { name: 'Teste 3', type: 3, date: "22/07/2023", location: "Jabaquara - Sp", description: 'Esse é o teste 3' },
    // { name: 'Teste 4', type: 5, date: "22/07/2023", location: "Jabaquara - Sp", description: 'Esse é o teste 4' },
    // { name: 'Teste 5', type: 4, date: "22/07/2023", location: "Jabaquara - Sp", description: 'Esse é o teste 5' },
  ]

  realizados: Array<Realizada> = [
    { name: 'Teste 1', type: 2, date: "22/07/2023", certificado: true },
    { name: 'Teste 2', type: 2, date: "25/07/2023", certificado: true },
    { name: 'Teste 3', type: 2, date: "22/08/2023", certificado: true },
    { name: 'Teste 4', type: 2, date: "21/07/2023", certificado: false },
  ]
}


