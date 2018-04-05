import { IProductItemProps } from "../containers/product/ProductItem";


export class Product implements IProductItemProps{
    public id: number;
    public title: string;
    public description: string;
    public img: string;
    public price: number;
}