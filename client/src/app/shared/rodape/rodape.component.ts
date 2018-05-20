import { Component, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-rodape',
  templateUrl: './rodape.component.html',
  styleUrls: ['./rodape.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class RodapeComponent {
  private email = 'viny.bas@gmail.com';
}
