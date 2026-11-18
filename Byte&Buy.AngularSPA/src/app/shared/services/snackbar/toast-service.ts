import { Injectable } from '@angular/core';
import Swal, { SweetAlertIcon, SweetAlertOptions } from 'sweetalert2';

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  success(message: string) {
    return this.show(message, 'success', 'Success');
  }

  error(message: string) {
    return this.show(message, 'error', 'Something went wrong');
  }

  info(message: string) {
    return this.show(message, 'info', 'Information');
  }

  private show(message: string, icon: SweetAlertIcon, title: string) {
    const options: SweetAlertOptions = {
      title,
      text: message,
      icon,
      toast: true,
      position: 'bottom-end',
      showConfirmButton: false,
      timer: 3000,
      timerProgressBar: true,
      iconColor: 'var(--primary-accent)',
      color: 'var(--text-primary)',
      background: 'var(--card-background)',
      customClass: {
        popup: 'app-toast',
      },
    };

    return Swal.fire(options);
  }
}
