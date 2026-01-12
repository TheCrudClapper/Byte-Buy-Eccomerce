import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { SelectListItemResponse } from '../../../shared/api-dto/select-list-item-response';
import { map, Observable } from 'rxjs';
import { SelectListItem } from '../../../shared/models/select-list-item';

@Injectable({
  providedIn: 'root',
})
export class ConditionApiService {
  private readonly resourceUri = "http://localhost:5099/api/conditions";
  private readonly httpClient: HttpClient = inject(HttpClient);

  getSelectList(): Observable<SelectListItem[]>{
    return this.httpClient.get<SelectListItemResponse[]>(`${this.resourceUri}/options`).pipe(
      map(response => response.map(item => ({
        id: item.id,
        title: item.title
      })))
    );
  }
}
