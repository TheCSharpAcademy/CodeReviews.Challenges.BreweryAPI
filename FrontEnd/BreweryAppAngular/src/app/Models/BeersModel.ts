import { BreweryClass } from "./BreweryModel";

export class BeerClass {
    public id: number;
    public name: string;
    public flavour: string;
    public age: string;
    public breweryId: number;
    public breweryPrice: number;
    public brewery?: BreweryClass | undefined;

    constructor(id: number, name: string, flavour: string, age: string, breweryId: number, breweryPrice: number, brewery: BreweryClass) {
        this.id = id;
        this.name = name;
        this.flavour = flavour;
        this.age = age;
        this.breweryId = breweryId;
        this.breweryPrice = breweryPrice;
        this.brewery = brewery;
    }

}
