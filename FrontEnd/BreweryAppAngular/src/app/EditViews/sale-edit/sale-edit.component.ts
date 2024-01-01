import { Component, Inject } from '@angular/core';
import {FormsModule} from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatButtonModule} from '@angular/material/button';
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogTitle,
} from '@angular/material/dialog';
import { SalesClass } from '../../Models/SalesModel';
import { BreweryInfoService } from '../../Services/brewery-info.service';
import { BreweryPostService } from '../../Services/brewery-post.service';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { BeerClass } from '../../Models/BeersModel';
import { WholesalerClass } from '../../Models/WholesalerModel';

@Component({
  selector: 'app-sale-edit',
  standalone: true,
  imports: [FormsModule, MatInputModule, MatSelectModule, MatFormFieldModule, MatButtonModule],
  templateUrl: './sale-edit.component.html',
  styleUrl: './sale-edit.component.css'
})
export class SaleEditComponent {
  isButtonDisabled: boolean = false;
  itemId: number = 0;
  beers: BeerClass[] = [];
  wholesalers: WholesalerClass[] = [];
  sale: SalesClass = {
    id: 0,
    breweryId: 0,
    wholesalerId: 0,
    beerId: 0,
    quantity: 0,
    saleDate: new Date()
  };

  constructor(private breweryInfoService: BreweryInfoService, private breweryPostService: BreweryPostService, private route: Router, public dialog: MatDialog) {
    this.getBeers();
    this.getWholesalers();
  }

  onSubmit() {
    this.breweryPostService.postItem(this.sale, 'Sales', this.route);
    }

  getBeers() {
    this.breweryInfoService.getInfo('Beers').subscribe((result) => {
      this.beers = result;
    });
  }

  getWholesalers() {
    this.breweryInfoService.getInfo('Wholesalers').subscribe((result) => {
      this.wholesalers = result;
    });
  }

  getQuote() {
    this.breweryInfoService.getQuote(this.sale.beerId, this.sale.quantity, this.sale.wholesalerId)
      .subscribe({
        next: (result) => 
          this.dialog.open(DialogQuote, { data: result}),

        error: (e) => this.dialog.open(DialogQuote, {data: e.error})
      });
  }
}

@Component({
  selector: 'dialog-quote',
  templateUrl: 'dialog-quote.html',
  standalone: true,
  imports: [MatDialogTitle, MatDialogContent, MatDialogActions, MatDialogClose, MatButtonModule],
})
export class DialogQuote {
  constructor(@Inject(MAT_DIALOG_DATA) public data: string) {}
}
