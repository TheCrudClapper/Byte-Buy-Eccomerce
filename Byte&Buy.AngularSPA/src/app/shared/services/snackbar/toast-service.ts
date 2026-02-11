import { inject, Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
@Injectable({
  providedIn: 'root',
})

export class ToastService {
  private readonly toastrService: ToastrService = inject(ToastrService);
  success(message: string) {
    this.toastrService.success(message, "Success", {
      progressAnimation: 'decreasing',
    });
  }

  error(message: string) {
    this.toastrService.error(message, "Error", {
      progressAnimation: 'decreasing',
    });
  }

  info(message: string) {
    this.toastrService.info(message, "Info", {
      progressAnimation: 'decreasing',
    });
  }

}
