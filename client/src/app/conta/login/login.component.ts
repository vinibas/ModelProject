import { Component, AfterViewInit, ViewChildren, ElementRef, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControlName } from '@angular/forms';
import { GenericValidator } from '../../validador-formulario/generic-validator';
import { Observable } from 'rxjs/Observable';

// import { ReactiveFormsModule, FormControl, FormArray } from '@angular/forms';
// import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/observable/fromEvent';
import 'rxjs/add/observable/merge';

import { ErrosCampoComponent } from '../../validador-formulario/erros-campo/erros-campo.component';
import { ContaService } from '../../services/conta.service';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../membership/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements AfterViewInit {
  formGroup: FormGroup;
  genericValidator: GenericValidator;

  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements: ElementRef[];
  @ViewChildren(ErrosCampoComponent)
  errosCampoComponent: ErrosCampoComponent[];
  errosFormulario: string[];

  constructor(
    formBuilder: FormBuilder,
    private contaService: ContaService,
    private router: Router,
    private authenticationService: AuthenticationService) {
      this.genericValidator = new GenericValidator({
      nome: {
        required: 'O nome é obrigatório'
      },
      senha: {
        required: 'A senha é obrigatória'
      }
      });

      this.formGroup = formBuilder.group({
        nome: ['', Validators.required],
        senha: ['', Validators.required]
    });
  }

  ngAfterViewInit(): void {
    this.genericValidator.configureOnAfterViewInit(this.formGroup, this.formInputElements, this.errosCampoComponent);
  }

  private logar() {
    if (this.formGroup.dirty && this.formGroup.valid) {
      this.formGroup.disable();

      this.contaService.login(this.formGroup.value).subscribe(
        result => {
          this.authenticationService.logarUsuario(result.tokenUsuarioLogado, result.nomeUsuarioLogado);
          this.router.navigate(['']);
        },
        error => {
          this.errosFormulario = error.errors;
          this.formGroup.enable();
        });
    }
  }
}
