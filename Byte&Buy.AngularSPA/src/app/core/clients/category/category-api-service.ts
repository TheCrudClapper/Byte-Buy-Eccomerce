import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { SelectListItemResponse } from '../../dto/common/select-list-item-response';
import { SelectListItem } from '../../../shared/models/select-list-item';

@Injectable({
  providedIn: 'root',
})
export class CategoryApiService {
  private readonly resourceUri = "http://localhost:5099/api/categories";
  private readonly httpClient: HttpClient = inject(HttpClient);

  getSelectList(): Observable<SelectListItem[]>{
    return this.httpClient.get<SelectListItemResponse[]>(`${this.resourceUri}/options`).pipe(
      map(response => response.map( item => ({
         id: item.id,
         title: item.title
        })))
    );
  }
  
}
