﻿import * as React from "react"
import { ProductItem, IProductItemProps } from "./ProductItem"
import { RouteComponentProps } from 'react-router';

export interface INetworkSettings {
    address: string
}

export class ProductGrid extends React.Component {

    public render() {

        return <div>
            <h1>Products</h1>
        </div>;

    }

}