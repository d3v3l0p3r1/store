import * as React from "react";
import { Product } from "../../models/Product"
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';

export interface IProductItemModalProps {
    product: Product,
    isOpen: boolean,
    closeModal: Function,
}

const headerStyle = {
    color : "#000000"
}

export class ProductItemModal extends React.Component<IProductItemModalProps> {



    public render() {
        return <Modal isOpen={this.props.isOpen} >

            <ModalHeader toggle={() => this.props.closeModal()} style={headerStyle}>                
            </ModalHeader >
            <ModalBody>
                <img src={this.props.product.img} width="100%" />
                <h3>{this.props.product.title}</h3>
                <span>{this.props.product.description}</span>
            </ModalBody>
            <ModalFooter>

            </ModalFooter>
        </Modal>;
    }

}






