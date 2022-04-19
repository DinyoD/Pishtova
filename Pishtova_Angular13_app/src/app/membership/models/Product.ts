import { PriceModel } from "./Price";

export interface ProductModel{
    id: string;
    name: string;
    description: string;
    prices: PriceModel[];
}

