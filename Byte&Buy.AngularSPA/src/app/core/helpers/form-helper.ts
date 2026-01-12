import { AbstractControl, FormGroup } from "@angular/forms";

export function getControl(form: FormGroup, path: string): AbstractControl {
    return form.get(path)!;
}

export function getErrorMessage(form: FormGroup, path: string): string | null{
    const control = getControl(form, path);

    if (!control || !control.errors || !(control.touched || control.dirty)) {
        return null;
    }
    
    if(control.errors['required']) return "This field is required";
    if(control.errors['minlength']) return `Minimum length is ${control.errors['minlength'].requiredLength}`;
    if (control.errors['email']) return 'Email is in invalid format';
    if(control.errors['maxlength']) return `Minimum length is ${control.errors['maxlength'].requiredLength}`;

    return 'Invalid value';
}