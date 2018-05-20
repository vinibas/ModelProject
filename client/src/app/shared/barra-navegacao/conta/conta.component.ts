import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../../membership/authentication.service';
import { ContaService } from '../../../services/conta.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-conta',
  templateUrl: './conta.component.html',
})
export class ContaComponent {

  constructor(
    private authenticationService: AuthenticationService,
    private contaService: ContaService,
    private router: Router,
  ) {}

  logout() {

    this.contaService.logout()
    .subscribe(
      success => {
        this.authenticationService.deslogarUsuario();
        this.router.navigate(['']);
      }, error => {
        console.log(error.errors);
        this.authenticationService.deslogarUsuario();
        this.router.navigate(['']);
      });
  }
}
