import {IBascetItem} from "../containers/bascet/BascetState"


export interface IOrderModel {
    name: string,
    phone: string,
    address: string,
    deliveryType: number,
    comment: string,
    change: string,
    deliveryTime: number,
    personCount: number,

    products: ReadonlyArray<IBascetItem>,
}

export class Order {
    id: number = 0;
    state: number = 0;
}

