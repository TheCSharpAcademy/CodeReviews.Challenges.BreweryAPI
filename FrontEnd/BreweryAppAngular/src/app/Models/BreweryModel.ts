import { BeerClass } from "./BeersModel";

export class BreweryClass {
    public id: number;
    public name: string;
    public Beers?: BeerClass[];

    constructor(id:number, name:string, Beers:BeerClass[]) {
        this.id = id;
        this.name = name;
        this.Beers = Beers;
    }
}
