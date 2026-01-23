import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { SelectListItemResponse } from '../../dto/common/select-list-item-response';
import { map, Observable } from 'rxjs';
import { SelectListItem } from '../../../shared/models/select-list-item';
import { environment } from '../../../../environments/environment';
import { API_ENDPOINTS } from '../../constants/api-constants';

@Injectable({
  providedIn: 'root',
})
export class ConditionApiService {
  private readonly httpClient: HttpClient = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;

  getSelectList(): Observable<SelectListItem[]> {
    return this.httpClient.get<SelectListItemResponse[]>(`${this.baseUrl}${API_ENDPOINTS.conditions.options}`).pipe(
      map(response => response.map(item => ({
        id: item.id,
        title: item.title
      })))
    );
  }
}