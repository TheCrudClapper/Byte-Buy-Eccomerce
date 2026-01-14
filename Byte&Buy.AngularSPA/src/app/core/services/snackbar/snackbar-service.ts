import { inject, Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root',
})
export class SnackbarService {
  private readonly snackBar: MatSnackBar = inject(MatSnackBar);

  success(message: string){
    this.snackBar.open(message, 'OK', {
      horizontalPosition: 'end',
      verticalPosition: 'top',
      panelClass: ['success']
    })
  }

  error(message: string){

  }

  info(message: string){

  }
}
