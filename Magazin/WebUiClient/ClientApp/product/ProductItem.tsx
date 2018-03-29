import * as React from 'react';

export interface IProductItemProps {
    title: string,
    price: number,
    img: string,
    id: number
}


export class ProductItem extends React.Component<IProductItemProps, {}> {
    public render() {
        return <div>
            <img src={this.props.img} width="64" height="64" />
            <span>{this.props.title}</span>
            <span>{this.props.price}</span>
        </div>;
    }    
}