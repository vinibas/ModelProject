import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-idioma',
  templateUrl: './idioma.component.html',
  styleUrls: ['./idioma.component.scss']
})
export class IdiomaComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  selIdiomaChange($event) {
    console.dir($event);
  }

  private getIdiomas(): any[] {
    return [
      { label: 'Português (Brasil)', value: 'pt-BR'},
      { label: 'Español', value: 'es'},
    ];
  }
}
