import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JwtHelper } from 'angular2-jwt';
import { AuthenticationService } from './authentication.service';
import { AuthorizationService } from './authorization.service';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [],
  providers: [
    JwtHelper,
    AuthenticationService,
    AuthorizationService,
  ],
})
export class MembershipModule { }
