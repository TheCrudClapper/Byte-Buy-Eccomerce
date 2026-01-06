import { Pipe, PipeTransform } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Pipe({
  name: 'showError',
  standalone: true
})
export class ShowErrorPipe implements PipeTransform {
  transform(form: FormGroup, path: string): boolean {
    const control = form.get(path);
    return !!control && control.invalid && (control.touched || control.dirty);
  }
}
