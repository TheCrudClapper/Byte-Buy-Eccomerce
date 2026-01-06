import { AbstractControl, FormGroup } from "@angular/forms";

export function getControl(form: FormGroup, path: string): AbstractControl {
    return form.get(path)!;
}

export function shouldShowError(form: FormGroup, path: string): boolean {
    const control = getControl(form, path);
    return control.invalid && (control.touched || control.dirty);
}