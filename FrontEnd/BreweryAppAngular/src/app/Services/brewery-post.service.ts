import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {MatDialog} from '@angular/material/dialog';
import { DialogQuote } from '../EditViews/sale-edit/sale-edit.component';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class BreweryPostService {

  private apiUrl = "https://localhost:7036/api"

  constructor(private http: HttpClient, private dialog: MatDialog) { }

  postItem(item: any, model: string, route: Router) {
    this.http.post(`${this.apiUrl}/${model}`, item)
      .subscribe({
        next: (r) => {
          setTimeout(() => 
          {
            route.navigate([model]);
          },
            1000);
        },
        error: (e) => { 
          this.dialog.open(DialogQuote, {data: e.error})
        }
      })
  }

  updateItem(item: any, model: string) {

    this.http.put(`${this.apiUrl}/${model}/${item.id}`, item)
      .subscribe({
        next: (r) => console.log("Api sucess", r),
        error: (e) => console.error("Api error", e)
      })
  }

  deleteRow(id: number, model: string) {

    this.http.delete(`${this.apiUrl}/${model}/${id}`)
      .subscribe({
        next: (r) => {
          console.log("Api sucess", r);
        },
        error: (e) => console.error("Api error", e)
      })
  }
}
