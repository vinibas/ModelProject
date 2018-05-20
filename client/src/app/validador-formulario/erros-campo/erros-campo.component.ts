import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-erros-campo',
  templateUrl: './erros-campo.component.html',
})
export class ErrosCampoComponent {

  @Input() nome: string;
  @Input() mensagens: string[];
  displayMessages: { [key: string]: string } = {};
}
