import { Component, OnInit } from '@angular/core';
import { ContaService } from '../../services/conta.service';
import { Usuario } from '../models/usuario';

@Component({
  selector: 'app-listar',
  templateUrl: './listar.component.html',
})
export class ListarComponent {

  usuarios: Usuario[];
  errosFormulario: string[];

  constructor(private contaService: ContaService) {
    contaService.listarUsuario().subscribe(
      success => this.usuarios = success,
      error => {
          this.errosFormulario = error.errors;

        //   console.log(error);
        // if (error.erroTratado) {
        //   // TODO: posso passar isso pra dentro do componente, nomear ele e acessar aqui pra não ter
        //   // que criar essa variável e fazer esse mesmo tratamento em todo lugar.
        //   this.errosFormulario = error.errors;
        // } else {
        //   console.log(error.errors);
        // }
      });
  }
}
