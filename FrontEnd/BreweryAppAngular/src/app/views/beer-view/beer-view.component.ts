import { Component } from '@angular/core';
import {MatTableModule} from '@angular/material/table';
import {MatDividerModule} from '@angular/material/divider';
import {MatButtonModule} from '@angular/material/button';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { BeerClass } from '../../Models/BeersModel';
import { BreweryInfoService } from '../../Services/brewery-info.service';
import { BreweryPostService } from '../../Services/brewery-post.service';

@Component({
  selector: 'app-beer-view',
  standalone: true,
  imports: [MatTableModule, MatDividerModule, MatButtonModule, RouterLink, RouterOutlet],
  templateUrl: './beer-view.component.html',
  styleUrl: './beer-view.component.css'
})
export class BeerViewComponent {
  beers: BeerClass[] = [];
  columnsToDisplay = ['id', 'name', 'flavour', 'age', 'breweryPrice', 'brewery'];
  clickedBeer!: BeerClass;

  constructor(private breweryInfoService: BreweryInfoService, private breweryPostService: BreweryPostService, private router: Router) {
   this.getBeers();
  }
  
  deleteBeer() {

    if (this.clickedBeer) {
      this.breweryPostService.deleteRow(this.clickedBeer.id, 'Beers');
      this.beers = this.beers.filter(b => b.id !== this.clickedBeer.id)  
    } else {

    }
  }

  editItem() {
    if (this.clickedBeer) {
      let beer = this.clickedBeer;
      this.router.navigate([`Beers/edit`], { state: {beer}});
    }
    
  }

  getBeers() {
    this.breweryInfoService.getInfo('Beers').subscribe((result) => {
      this.beers = result;
    });
  }
}
