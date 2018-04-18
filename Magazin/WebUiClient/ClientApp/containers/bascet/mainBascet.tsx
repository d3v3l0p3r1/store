import * as React from 'react'
import { Product } from "../../models/Product"
import { connect } from "react-redux"
import { IApplicationState } from "../../stores/IApplicationState";
import { Dispatch, ActionCreator, bindActionCreators } from "redux";
import { IBascetItem, IRemoveAllProduct, IAddToCardAction, IRemoveFromCardAction } from "./Bascet"
import {addToCard, removeFromCard, removeProductFromCard} from "./actions"

export interface IMainBascetProps {
    readonly products: ReadonlyArray<IBascetItem>,
    readonly total: number;
    readonly addToCard: ActionCreator<IAddToCardAction>,
    readonly removeFromCard: ActionCreator<IRemoveFromCardAction>,
    readonly removeProduct: ActionCreator<IRemoveAllProduct>,    
}

export class MainBascet extends React.Component<IMainBascetProps, {}> {

    onAddClick = (product: Product) => {
        this.props.addToCard(product);
    }

    onRemoveClick = (id: number) => {
        this.props.removeFromCard(id);
    }

    onDeleteProductClick = (id: number) => {        
        this.props.removeProduct(id);
    }

    public render() {

        const items = this.props.products.map(x => {
            return <li key={x.product.id} className="main-bascet-li">
                <div className="main-bascet-item">
                    <div className="container">
                        <div className="row">
                            <div className="col-md-10">
                                <span>{x.product.title}</span>
                            </div>
                            <div className="col-md-2">
                                <button className="btn btn-outline-danger btn-sm mb-2" onClick={() => this.onDeleteProductClick(x.product.id)}>
                                    <i className="fa fa-close" />
                                </button>
                            </div>
                        </div>

                        <div className="row">
                            <div className="col-md-8">
                                <button className="btn btn-outline-danger btn-sm" onClick={() => this.onRemoveClick(x.product.id)}>
                                    <i className="fa fa-minus" />
                                </button>
                                <span>&nbsp;{x.count}&nbsp;шт&nbsp;</span>
                                <button className="btn btn-outline-danger btn-sm" onClick={() => this.onAddClick(x.product)}>
                                    <i className="fa fa-plus" />
                                </button>
                            </div>
                            <div className="col-md-4">
                                <span>{x.product.price}&nbsp;руб</span>
                            </div>
                        </div>
                    </div>
                </div>
            </li>;
        });

        return <div className="row">
            <div className=" col-md-8 main-bascet">
                <div className="main-bascet-header-container">
                    <h3 className="main-bascet-header-text">Ваш заказ</h3>
                </div>
                <ul>
                    {items}
                </ul>

                <div className="main-bascet-price-container">
                    <h3 className="main-bascet-price"> {this.props.total}&nbsp;руб</h3>                    
                </div>
                <div className="main-bascet-footer">
                    <button className="btn btn-outline-danger w-100">
                        <span>Оформить</span>
                    </button>
                </div>
            </div>

        </div>;
    }
}

function mapStateToProps(state: IApplicationState, ownProps: any) {
    return {
        products: state.bascetState.products,
        total: state.bascetState.total,
    };
}

function mapDispatchToProps(dispatch: Dispatch<IApplicationState>) {

    return {
        addToCard: bindActionCreators(addToCard, dispatch),
        removeFromCard: bindActionCreators(removeFromCard, dispatch),
        removeProduct: bindActionCreators(removeProductFromCard, dispatch)
    };
}


export default connect(mapStateToProps, mapDispatchToProps)(MainBascet) as React.ComponentClass<{}>;