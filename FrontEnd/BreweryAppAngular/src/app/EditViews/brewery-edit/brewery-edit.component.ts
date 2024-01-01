import { Component } from '@angular/core';
import {FormsModule} from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatButtonModule} from '@angular/material/button';
import { BreweryClass } from '../../Models/BreweryModel';
import { BreweryInfoService } from '../../Services/brewery-info.service';
import { BreweryPostService } from '../../Services/brewery-post.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-brewery-edit',
  standalone: true,
  imports: [FormsModule, MatInputModule, MatSelectModule, MatFormFieldModule, MatButtonModule],
  templateUrl: './brewery-edit.component.html',
  styleUrl: './brewery-edit.component.css'
})
export class BreweryEditComponent {
  isButtonDisabled: boolean = false;
  itemId: number = 0;
  brewery: BreweryClass = {
    id: 0,
    name: '',
    Beers: []
  };

  onSubmit() {

    this.isButtonDisabled = true;

    if (this.brewery.id != 0) {
      delete this.brewery.Beers;
      this.breweryPostService.updateItem(this.brewery, 'Breweries');
    } else {
      this.breweryPostService.postItem(this.brewery, 'Breweries', this.route);
    }
  }

  constructor(private breweryInfoService: BreweryInfoService, private breweryPostService: BreweryPostService, private route: Router) {

    if (this.route.getCurrentNavigation()?.extras?.state != undefined){
      this.brewery = this.route.getCurrentNavigation()?.extras?.state!['brewery'];
    }
  }

}
