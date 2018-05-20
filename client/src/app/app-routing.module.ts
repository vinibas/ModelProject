import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IndexComponent } from './home/index/index.component';
import { SobreComponent } from './home/sobre/sobre.component';
import { LoginComponent } from './conta/login/login.component';
import { CadastrarUsuarioComponent } from './conta/cadastrar-usuario/cadastrar-usuario.component';
import { ListarComponent } from './conta/listar/listar.component';
import { MembershipModule } from './membership/membership.module';
import { AuthorizationService } from './membership/authorization.service';

const routes: Routes = [
  { path: '', component: IndexComponent },
  { path: 'Home/Index', component: IndexComponent },
  { path: 'Home/Sobre', component: SobreComponent },
  { path: 'Conta/Login', component: LoginComponent, canActivate: [AuthorizationService], data: {estadoLogin: false} },
  { path: 'Conta/Listar', component: ListarComponent, canActivate: [AuthorizationService], data: {estadoLogin: true}  },
  { path: 'NovoUsuario', component: CadastrarUsuarioComponent, canActivate: [AuthorizationService], data: {estadoLogin: false} },
  { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
