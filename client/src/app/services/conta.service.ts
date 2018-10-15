import { Injectable } from '@angular/core';
import { Http, RequestOptions, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import 'rxjs/add/operator/do';
import 'rxjs/add/operator/switchMap';
import { Usuario } from '../conta/models/usuario';
import { AuthenticationService } from '../membership/authentication.service';

@Injectable()
export class ContaService {
  private urlService = 'http://localhost:6525/api/conta/';

  private get options(): RequestOptions {
    const headers = new Headers({ 'Content-Type': 'application/json' });
    return new RequestOptions({ headers: headers });
  }

  private get optionsAuth(): RequestOptions {
    const headers = new Headers({ 'Content-Type': 'application/json' });
    headers.append('Authorization', `Bearer ${this.authenticationService.tokenUsuarioLogado}`);
    return new RequestOptions({ headers: headers });
  }

  constructor(private http: Http, private authenticationService: AuthenticationService) { }

  login(usuario: any): Observable<{tokenUsuarioLogado: string, nomeUsuarioLogado: string}> {
    return this.http
      .post(this.urlService + 'login', usuario, this.options)
      .map(this.extrairDadosResponse)
      .catch(this.tratarErroResponse);
  }

  logout() {
    return this.http.post(this.urlService + 'logout', this.options)
      .catch(this.tratarErroResponse);
  }

  listarUsuario(): Observable<Usuario[]> {
    return this.http
    .get(this.urlService + 'listar', this.optionsAuth)
    .map(this.extrairDadosResponse)
    .map(p => p.map(q => {
      const usuario = new Usuario();
        usuario.nome = q.nome;
        usuario.criadoEm = new Date(q.criadoEm);
        return usuario;
      }))
      .catch(this.tratarErroResponse);
  }

  cadastrarUsuario(usuario: any): Observable<{tokenUsuarioLogado: string, nomeUsuarioLogado: string}> {
    return this.http
      .post(this.urlService + 'cadastrar-usuario', usuario, this.options)
      .map(this.extrairDadosResponse)
      .catch(this.tratarErroResponse);
  }

  extrairDadosResponse(response: Response): any {
    return response.json().data;
  }

  tratarErroResponse(erro: Response | any): any {
    const erros = erro.text() ? erro.json().errors : [];
    if (erros.length) {
      return Observable.throw({ erroTratado: true, status: erro.status, errors: erro.json().errors });
    } else {
      const errors: string[] = [];

      switch (erro.status) {
        case 401:
          errors.push('Usuário não autorizado! Faça login novamente!');
          break;
        case 404:
          errors.push('Houve um erro de conexão com o servidor. Tente novamente em alguns instantes!');
          break;
        default:
          errors.push(`Ocorreu um erro inesperado! Código: ${erro.status} ${erro.statusText}`);
          break;
      }

      return Observable.throw({
        erroTratado: false,
        status: erro.status,
        errors: errors
      });
    }
  }
}
