import { Component, OnInit, ViewChildren, ElementRef, AfterViewInit } from '@angular/core';
import { FormGroup, FormControlName, FormBuilder, Validators } from '@angular/forms';
import { GenericValidator } from '../../validador-formulario/generic-validator';
import { ErrosCampoComponent } from '../../validador-formulario/erros-campo/erros-campo.component';
import { ContaService } from '../../services/conta.service';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../membership/authentication.service';


@Component({
  selector: 'app-cadastrar-usuario',
  templateUrl: './cadastrar-usuario.component.html',
})
export class CadastrarUsuarioComponent implements AfterViewInit {
  formGroup: FormGroup;
  genericValidator: GenericValidator;

  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements: ElementRef[];
  @ViewChildren(ErrosCampoComponent)
  errosCampoComponent: ErrosCampoComponent[];
  errosFormulario: string[];

  constructor(formBuilder: FormBuilder,
    private contaService: ContaService,
    private router: Router,
    private authenticationService: AuthenticationService) {
      this.genericValidator = new GenericValidator({
      nome: {
        required: 'O nome é obrigatório',
        maxlength: 'O nome não pode conter mais de 30 caracteres',
      },
      senha: {
        required: 'A senha é obrigatória',
        minlength: 'A senha deve possuir entre 3 e 20 caracteres',
        maxlength: 'A senha deve possuir entre 3 e 20 caracteres',
      }
      });

      this.formGroup = formBuilder.group({
        nome: ['', [Validators.required, Validators.maxLength(30)]],
        senha: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(20)]]
    });
  }

  ngAfterViewInit(): void {
    this.genericValidator.configureOnAfterViewInit(this.formGroup, this.formInputElements, this.errosCampoComponent);
  }

  enviar() {
    if (this.formGroup.dirty && this.formGroup.valid) {
      this.formGroup.disable();

      this.contaService.cadastrarUsuario(this.formGroup.value).subscribe(
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
