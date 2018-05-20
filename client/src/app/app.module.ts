import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';

import { CarouselModule } from 'ngx-bootstrap/carousel';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

import { IndexComponent } from './home/index/index.component';
import { SobreComponent } from './home/sobre/sobre.component';
import { BarraNavegacaoComponent } from './shared/barra-navegacao/barra-navegacao.component';
import { RodapeComponent } from './shared/rodape/rodape.component';
import { ContaComponent } from './shared/barra-navegacao/conta/conta.component';
import { IdiomaComponent } from './shared/barra-navegacao/idioma/idioma.component';
import { LoginComponent } from './conta/login/login.component';
import { ListarComponent } from './conta/listar/listar.component';
import { ValidadorFormularioModule } from './validador-formulario/validador-formulario.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { HttpClientInMemoryWebApiModule } from 'angular-in-memory-web-api';
import { HttpModule } from '@angular/http';
import { ContaService } from './services/conta.service';
import { CadastrarUsuarioComponent } from './conta/cadastrar-usuario/cadastrar-usuario.component';
import { MembershipModule } from './membership/membership.module';
import { AuthenticationService } from './membership/authentication.service';
import { AuthorizationService } from './membership/authorization.service';


@NgModule({
  declarations: [
    AppComponent,
    IndexComponent,
    SobreComponent,
    BarraNavegacaoComponent,
    RodapeComponent,
    ContaComponent,
    IdiomaComponent,
    LoginComponent,
    ListarComponent,
    CadastrarUsuarioComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CarouselModule.forRoot(),
    BsDropdownModule.forRoot(),
    ValidadorFormularioModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    // HttpClientInMemoryWebApiModule.forRoot(InMemoryDataService, {delay: 100 /*, dataEncapsulation: false */}),
    MembershipModule,
  ],
  providers: [
    ContaService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
