import * as React from "react";
import Product from "../../models/Product"
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';

export interface IProductItemModalProps {
    product: Product,
    isOpen: boolean,
    closeModal: Function,
    itemInBucketCount: number,
    addToCard: Function,
    removeFromCard: Function,
}

const headerStyle = {
    color: "#000000"
}

export class ProductItemModal extends React.Component<IProductItemModalProps> {

    constructor(props: IProductItemModalProps) {
        super(props);
    }

    onAddClick = (product: Product) => {
        this.props.addToCard(product);
    }

    onRemoveClick = (id: number) => {
        this.props.removeFromCard(id);
    }


    public render() {
        return <Modal isOpen={this.props.isOpen} keyboard={true} backdrop={true} toggle={() => this.props.closeModal()}>

            <ModalHeader toggle={() => this.props.closeModal()} style={headerStyle}>
            </ModalHeader >
            <ModalBody>
                <img src={this.props.product.img} width="100%" />
                <h3>{this.props.product.title}</h3>
                <span className="text-muted">{this.props.product.description}</span>
                <br />
                <span className="text-dark"><b>{this.props.product.price} руб</b></span>
            </ModalBody>
            <ModalFooter>
                <div className="input-group">
                    <div className="input-group-prepend">
                        <button className="btn" onClick={() => this.onRemoveClick(this.props.product.id)}>-</button>
                    </div>
                    <label className="form-control product-card-count-input">{this.props.itemInBucketCount}</label>
                    <div className="input-group-append">
                        <button className="btn" onClick={() => this.onAddClick(this.props.product)}>+</button>
                    </div>
                </div>
            </ModalFooter>
        </Modal>;
    }

}






