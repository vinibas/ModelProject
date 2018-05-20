import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot, CanActivateChild } from '@angular/router';
import { AuthenticationService } from './authentication.service';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class AuthorizationService implements CanActivate, CanActivateChild {

  canActivateChild(childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | Observable<boolean> | Promise<boolean> {
    return this.canActivate(childRoute, state);
  }
  constructor(private router: Router, private authentication: AuthenticationService) { }

  canActivate(routeAc: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const permiteAcesso =
      (routeAc.data.estadoLogin && this.authentication.usuarioEstaLogado)
      ||
      (!routeAc.data.estadoLogin && !this.authentication.usuarioEstaLogado);

    if (permiteAcesso) {
      return true;
    } else {
      if (routeAc.data.estadoLogin) {
        this.router.navigate(['Conta/Login']);
        return false;
    } else {
        this.router.navigate(['']);
        return false;
      }
    }
  }
}
