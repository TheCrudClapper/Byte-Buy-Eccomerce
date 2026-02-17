import { Injectable } from '@angular/core';
import Swal, { SweetAlertIcon } from 'sweetalert2';

@Injectable({
  providedIn: 'root',
})

export class DialogService {

  confirm(options:
    {
      title: string;
      text?: string;
      icon?: SweetAlertIcon;
      confirmText?: string;
      cancelText?: string;
    }) {
    return Swal.fire(
      {
        title: options.title,
        text: options.text,
        icon: options.icon ?? 'question',
        iconColor: 'var(--primary-accent)',
        color: 'var(--text-primary)',
        showCancelButton: true,
        confirmButtonText: options.confirmText ?? 'Confirm',
        cancelButtonText: options.cancelText ?? 'Cancel',
        customClass: { popup: 'my-dialog', confirmButton: 'btn', cancelButton: 'btn' }
      });
  }

  success(message: string) {
    return Swal.fire(
      {
        title: 'Success',
        text: message,
        icon: 'success',
        iconColor: 'var(--primary-accent)',
        color: 'var(--text-primary)',
        customClass: { popup: 'my-dialog', confirmButton: 'btn', cancelButton: 'btn' }
      });
  }

  error(message: string) {
    return Swal.fire(
      {
        title: 'Something went wrong',
        text: message,
        icon: 'error',
        iconColor: 'var(--primary-accent)',
        color: 'var(--text-primary)',
        customClass: { popup: 'my-dialog', confirmButton: 'btn', cancelButton: 'btn' }
      });
  }
}
