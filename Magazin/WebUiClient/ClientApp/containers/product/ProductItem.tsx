import * as React from 'react';
import { Product } from "../../models/Product"
import { ProductItemModal } from "./ProductItemModal"

export interface IProductItemProps {
    product: Product,
    itemInBucketCount: number,
    addToCard: Function,
    removeFromCard: Function,
}

interface IProductItemState {
    modalOpen: boolean;
}

export class ProductItem extends React.Component<IProductItemProps, IProductItemState> {
    constructor(props: IProductItemProps) {
        super(props);
        this.state = {
            modalOpen: false
        };
    }

    onAddClick = () => {
        this.props.addToCard(this.props.product);
    }

    onRemoveClick = () => {
        this.props.removeFromCard(this.props.product.id);
    }

    onItemClick = () => {
        this.setState({modalOpen : true});
    }

    modalClose = () => {
        this.setState({modalOpen : false});
    }


    public render() {
        return <div>
            <ProductItemModal product={this.props.product} isOpen={this.state.modalOpen} closeModal={this.modalClose} />
            <div className="card mb-4 box-shadow">
                <div className="p-3">
                    <img src={this.props.product.img} className="card-img-top" onClick={this.onItemClick} height="100" />
                </div>
                <div className="card-body">
                    <h5 className="card-text">{this.props.product.title}</h5>
                    <p className="card-text product-card-description">{this.props.product.description}</p>
                </div>
                <div className="card-footer">
                    <h4>{this.props.product.price} руб 210гр</h4>
                    <div className="input-group">
                        <div className="input-group-prepend">
                            <button className="btn" onClick={this.onRemoveClick}>-</button>
                        </div>
                        <label className="form-control product-card-count-input">{this.props.itemInBucketCount}</label>
                        <div className="input-group-append">
                            <button className="btn" onClick={this.onAddClick}>+</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>;
    }
}