import { FormGroup } from '@angular/forms';
import { ElementRef } from '@angular/core';
import { ErrosCampoComponent } from './erros-campo/erros-campo.component';
import { Observable } from 'rxjs/Observable';

export class GenericValidator {
    constructor(private validationMessages: { [key: string]: { [key: string]: string } }) {  }

    processMessages(container: FormGroup): { [key: string]: string } {
        const messages = {};
        for (const controlKey in container.controls) {
            if (container.controls.hasOwnProperty(controlKey)) {
                const c = container.controls[controlKey];

                if (c instanceof FormGroup) {
                    const childMessages = this.processMessages(c);
                    Object.assign(messages, childMessages);
                } else {
                    if (this.validationMessages[controlKey]) {
                        messages[controlKey] = [];
                        if ((c.dirty || c.touched) && c.errors) {
                            Object.keys(c.errors).map(messageKey => {
                                if (this.validationMessages[controlKey][messageKey]) {
                                    messages[controlKey].push(this.validationMessages[controlKey][messageKey]);
                                }
                            });
                        }
                    }
                }
            }
        }
        return messages;
    }

    configureOnAfterViewInit(formGroup: FormGroup, formInputElements: ElementRef[], errosCampoComponent: ErrosCampoComponent[]) {
        const controlBlurs: Observable<any>[] = formInputElements
      .map((formControl: ElementRef) => Observable.fromEvent(formControl.nativeElement, 'blur'));

        Observable.merge(...controlBlurs).subscribe(value => {
        const displayMessages = this.processMessages(formGroup);
        errosCampoComponent
            .map(p => p.displayMessages = displayMessages);
        });
    }
}
