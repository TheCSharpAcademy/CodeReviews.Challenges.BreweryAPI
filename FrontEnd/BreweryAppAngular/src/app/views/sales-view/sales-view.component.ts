import { Component } from '@angular/core';
import { SalesClass } from '../../Models/SalesModel';
import {MatTableModule} from '@angular/material/table';
import {MatDividerModule} from '@angular/material/divider';
import {MatButtonModule} from '@angular/material/button';
import { BreweryInfoService } from '../../Services/brewery-info.service';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-sales-view',
  standalone: true,
  imports: [MatButtonModule, MatDividerModule, MatTableModule, CommonModule, RouterLink, RouterOutlet],
  templateUrl: './sales-view.component.html',
  styleUrl: './sales-view.component.css'
})
export class SalesViewComponent {
  sales: SalesClass[] = [];
  columnsToDisplay = ['id', 'quantity', 'beer', 'wholesaler', 'brewery', 'date',];
  clickedSale!: SalesClass;

  constructor(private breweryInfoService: BreweryInfoService) {
   this.getSales();
  }
  
  getSales() {
    this.breweryInfoService.getInfo('Sales').subscribe((result) => {
      this.sales = result;
    });
  }

}
