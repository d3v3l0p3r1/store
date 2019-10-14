import * as React from 'react';
import { ProductItemModal } from "./ProductItemModal";
export class ProductItem extends React.Component {
    constructor(props) {
        super(props);
        this.onAddClick = () => {
            this.props.addToCard(this.props.product);
        };
        this.onRemoveClick = () => {
            this.props.removeFromCard(this.props.product.id);
        };
        this.onItemClick = () => {
            this.setState({ modalOpen: true });
        };
        this.modalClose = () => {
            this.setState({ modalOpen: false });
        };
        this.state = {
            modalOpen: false
        };
    }
    render() {
        return React.createElement("div", null,
            React.createElement(ProductItemModal, { product: this.props.product, isOpen: this.state.modalOpen, closeModal: this.modalClose, addToCard: this.props.addToCard, removeFromCard: this.props.removeFromCard, itemInBucketCount: this.props.itemInBucketCount }),
            React.createElement("div", { className: "card mb-4 box-shadow" },
                React.createElement("div", { className: "p-3" },
                    React.createElement("img", { src: this.props.product.img, className: "card-img-top product-card-image", onClick: this.onItemClick, height: "100" })),
                React.createElement("div", { className: "card-body" },
                    React.createElement("h5", { className: "card-text" }, this.props.product.title),
                    React.createElement("p", { className: "card-text product-card-description" }, this.props.product.description)),
                React.createElement("div", { className: "card-footer" },
                    React.createElement("h4", null,
                        this.props.product.price,
                        " \u0440\u0443\u0431 210\u0433\u0440"),
                    React.createElement("div", { className: "input-group" },
                        React.createElement("div", { className: "input-group-prepend" },
                            React.createElement("button", { className: "btn", onClick: this.onRemoveClick }, "-")),
                        React.createElement("label", { className: "form-control product-card-count-input" }, this.props.itemInBucketCount),
                        React.createElement("div", { className: "input-group-append" },
                            React.createElement("button", { className: "btn", onClick: this.onAddClick }, "+"))))));
    }
}
//# sourceMappingURL=ProductItem.js.map