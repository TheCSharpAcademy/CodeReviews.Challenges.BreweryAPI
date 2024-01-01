import { SalesClass } from "./SalesModel";
import { WholesalerStockClass } from "./WholesalerStockModel";

export class WholesalerClass {
    public id: number;
    public name: string;
    public stockLimit: number;
    public sales?: SalesClass[];
    public stocks?: WholesalerStockClass[];
    public allowedBeersId: number[];
    public allowedBeersNames?: string[];

    constructor(id: number, name: string, stockLimit: number, sales: SalesClass[], stocks: WholesalerStockClass[], allowedBeersId: number[]) {
        this.id = id;
        this.stockLimit = stockLimit;
        this.name = name;
        this.sales = sales;
        this.stocks = stocks;
        this.allowedBeersId = allowedBeersId;
    }
}
