import { Injectable } from '@angular/core';
import { JwtHelper } from 'angular2-jwt';

@Injectable()
export class AuthenticationService {

  constructor(private jwtHelper: JwtHelper) { }

  get usuarioEstaLogado(): boolean {
    const token = this.tokenUsuarioLogado;
    return Boolean(token && !this.jwtHelper.isTokenExpired(token));
  }

  get tokenUsuarioLogado(): string {
    return localStorage.getItem('ps.token');
  }

  get nomeUsuarioLogado(): string {
    return localStorage.getItem('ps.nome');
  }

  logarUsuario(token: string, nome: string) {
    localStorage.setItem('ps.token', token);
    localStorage.setItem('ps.nome', nome);
  }

  deslogarUsuario() {
    localStorage.removeItem('ps.token');
    localStorage.removeItem('ps.nome');
  }
}
