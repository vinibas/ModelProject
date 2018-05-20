import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-erros-formulario',
  templateUrl: './erros-formulario.component.html',
})
export class ErrosFormularioComponent {

  @Input() titulo = 'Erros:';
  @Input() erros: string[] = [];
}
