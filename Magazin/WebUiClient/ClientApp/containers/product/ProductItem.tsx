import * as React from 'react';

export interface IProductItemProps {
    title: string,
    price: number,
    img: string,
    id: number,
}


export class ProductItem extends React.Component<IProductItemProps, {}> {
    public render() {
        return <div className="product-item col-md-4">
            <div className="card mb-4 box-shadow">
                <img src={this.props.img} className="card-img-top" />
                <div className="card-body">
                    <span className="card-text">{this.props.title}</span>
                    <span className="card-text">{this.props.price}</span>
                </div>
            </div>
        </div>;
    }
}