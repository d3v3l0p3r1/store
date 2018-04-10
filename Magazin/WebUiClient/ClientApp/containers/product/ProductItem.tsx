import * as React from 'react';
import {Product} from "../../models/Product"

export interface IProductItemProps {
    product: Product,
    itemInBucketCount : 0,
}


export class ProductItem extends React.Component<IProductItemProps, {}> {



    public render() {
        return <div className="card mb-4 box-shadow">
            <div className="p-3">
                <img src={this.props.product.img} className="card-img-top" height="100"/>
            </div>
            <div className="card-body">
                <h5 className="card-text">{this.props.product.title}</h5>                
                <p className="card-text product-card-description">{this.props.product.description}</p>                
            </div>
            <div className="card-footer">
                <h4>{this.props.product.price} руб 210гр</h4>
                <div className="input-group">
                    <div className="input-group-prepend">
                        <button className="btn">-</button>
                    </div>                    
                    <label className="form-control product-card-count-input">{this.props.itemInBucketCount}</label>
                    <div className="input-group-append">
                        <button className="btn">+</button>
                    </div>
                </div>
            </div>
        </div>;
    }
}