import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BreweryInfoService {

  private apiUrl = "https://localhost:7036/api"

  getApiRoute() {
    return this.apiUrl;
  }

  constructor(private http: HttpClient) { }

  getInfo(model: string, id?: number): Observable<any> {
    if (id) {
      return this.http.get<any>(`${this.apiUrl}/${model}/${id}`);
    }
    return this.http.get<any>(`${this.apiUrl}/${model}`);
  }

  getQuote(beerId: number, quantity: number, wholesalerId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/Sale/beer=${beerId}&quantity=${quantity}&seller=${wholesalerId}`);
  }
}
