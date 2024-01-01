export class WholesalerStockClass {
    public id: number;
    public wholesalerId: number;
    public beerId: number;
    public stockQuantity: number;

    constructor(id: number, wholesalerId: number, beerId: number, stockQuantity: number) {
        this.id = id;
        this.wholesalerId = wholesalerId;
        this.beerId = beerId;
        this.stockQuantity = stockQuantity;
    }
}
