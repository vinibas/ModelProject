import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ErrosCampoComponent } from './erros-campo/erros-campo.component';
import { ErrosFormularioComponent } from './erros-formulario/erros-formulario.component';
import { GenericValidator } from './generic-validator';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    ErrosCampoComponent,
    ErrosFormularioComponent
  ],
  exports: [
    ErrosCampoComponent,
    ErrosFormularioComponent
  ]
})
export class ValidadorFormularioModule { }
