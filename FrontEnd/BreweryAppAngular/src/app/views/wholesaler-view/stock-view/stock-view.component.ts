import { Component, Inject } from '@angular/core';
import {MAT_DIALOG_DATA} from '@angular/material/dialog';

@Component({
  selector: 'app-stock-view',
  standalone: true,
  imports: [],
  templateUrl: './stock-view.component.html',
  styleUrl: './stock-view.component.css'
})
export class StockViewComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: any[]) {}
}
