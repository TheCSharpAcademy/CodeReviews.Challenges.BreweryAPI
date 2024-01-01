import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { BeerViewComponent } from './beer-view/beer-view.component';
import { BreweryViewComponent } from './brewery-view/brewery-view.component';
import { WholesalerViewComponent } from './wholesaler-view/wholesaler-view.component';
import { BrewEditComponent } from './EditViews/brew-edit/brew-edit.component';
import { WholesalerEditComponent } from './EditViews/wholesaler-edit/wholesaler-edit.component';
import { BreweryEditComponent } from './EditViews/brewery-edit/brewery-edit.component';
import { SalesViewComponent } from './views/sales-view/sales-view.component';
import { SaleEditComponent } from './EditViews/sale-edit/sale-edit.component';

export const routes: Routes = [
    {
        path: '',
        component: HomeComponent
    },
    {
        path: 'Beers',
        component: BeerViewComponent
    },
    {
        path: 'Breweries',
        component: BreweryViewComponent
    },
    {
        path: 'Wholesalers',
        component: WholesalerViewComponent
    },
    {
        path: 'Sales',
        component: SalesViewComponent
    },
    {
        path: 'Beers/Add',
        component: BrewEditComponent
    },
    {
        path: 'Beers/edit',
        component: BrewEditComponent
    },
    {
        path: 'Wholesalers/Add',
        component: WholesalerEditComponent
    },
    {
        path: 'Wholesalers/edit',
        component: WholesalerEditComponent
    },
    {
        path: 'Breweries/Add',
        component: BreweryEditComponent
    },
    {
        path: 'Breweries/edit',
        component: BreweryEditComponent
    },
    {
        path: 'Sales/Add',
        component: SaleEditComponent
    },
];
