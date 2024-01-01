import { Component } from '@angular/core';
import {MatTableModule} from '@angular/material/table';
import {MatDividerModule} from '@angular/material/divider';
import {MatButtonModule} from '@angular/material/button';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { BreweryClass } from '../../Models/BreweryModel';
import { BreweryInfoService } from '../../Services/brewery-info.service';
import { BreweryPostService } from '../../Services/brewery-post.service';


@Component({
  selector: 'app-brewery-view',
  standalone: true,
  imports: [MatTableModule, MatDividerModule, MatButtonModule, RouterLink, RouterOutlet],
  templateUrl: './brewery-view.component.html',
  styleUrl: './brewery-view.component.css'
})
export class BreweryViewComponent {
  breweries: BreweryClass[] = [];
  columnsToDisplay = ['id', 'name'];
  clickedBrewery!: BreweryClass;

  constructor(private breweryInfoService: BreweryInfoService, private breweryPostService: BreweryPostService, private router: Router) {
    this.getBreweries();
   }
   
   deleteBrewery() {

    if (this.clickedBrewery) {
      this.breweryPostService.deleteRow(this.clickedBrewery.id, 'Breweries');
      this.breweries = this.breweries.filter(b => b.id !== this.clickedBrewery.id)  
    } else {

    }
  }

  editItem() {
    if (this.clickedBrewery) {
      let brewery = this.clickedBrewery;
      this.router.navigate([`Breweries/edit`], { state: {brewery}});
    }
    
  }

  getBreweries() {
    this.breweryInfoService.getInfo('Breweries').subscribe((result) => {
      this.breweries = result;
    });
  }
}
