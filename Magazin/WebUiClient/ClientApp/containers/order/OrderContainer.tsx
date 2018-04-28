import * as React from "react"
import { connect } from 'react-redux';
import { RouteComponentProps, withRouter } from 'react-router';
import { Dispatch, bindActionCreators, ActionCreator } from "redux";
import { IAddToCardAction, IRemoveFromCardAction, IBascetState } from "../bascet/Bascet"
import { IApplicationState } from "../../stores/IApplicationState";
import { addToCard, removeFromCard } from "../bascet/actions"
import { Product } from "../../models/Product"
import "./Order.css"

export interface IOrderContainerProps extends RouteComponentProps<any> {
    addToCard: ActionCreator<IAddToCardAction>;
    removeFromCard: ActionCreator<IRemoveFromCardAction>;
    bascetState: IBascetState;
}

class OrderContainer extends React.Component<IOrderContainerProps> {


    onAddClick = (product: Product) => {
        this.props.addToCard(product);
    }

    onRemoveClick = (id: number) => {
        this.props.removeFromCard(id);
    }


    public render() {

        const list = this.props.bascetState.products.map(p => {
            return <div className="card order-container" key={p.product.id.toString()}>
                <div className="card-body">
                    <div className="row">
                        <div className="col-md-4">
                            <img src={p.product.img} width="100%" height="100%" />
                        </div>

                        <div className="col-md-8">
                            <h3>{p.product.title}</h3>
                            <span className="order-text">{p.product.description}</span>
                            <span className="order-text"><b>{p.product.price} руб</b></span>

                            <div className="row">
                                <div className="col-md-6">
                                    <div className="input-group">
                                        <div className="input-group-prepend">
                                            <button className="btn" onClick={() => this.onRemoveClick(p.product.id)}>-</button>
                                        </div>
                                        <label className="form-control product-card-count-input">{p.count}</label>
                                        <div className="input-group-append">
                                            <button className="btn" onClick={() => this.onAddClick(p.product)}>+</button>
                                        </div>
                                    </div>
                                </div>
                                <div className="col-md-6">
                                    <span className="order-text">{p.product.price * p.count} руб</span>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>

            </div>;
        });

        return <div className="row main-page">
            <div className="col-md-4 d-none d-lg-block">

            </div>
            <div className="col-md-8 main-grid-container">
                <h3 className="order-header">Оформление заказа</h3>;
                 <div className="order-product-container">
                    {list}
                 </div>
                <div> 
                    <h3 className="order-total-text">Итоговая сумма: {this.props.bascetState.totalPrice} р</h3>
                </div>
            </div>


        </div>;
    }
}


function mapStateToProps(state: IApplicationState, ownProps: any) {
    return {
        bascetState: state.bascetState
    }
}

function mapDispatchtoProps(dispatch: Dispatch<IApplicationState>) {
    return {
        addToCard: bindActionCreators(addToCard, dispatch),
        removeFromCard: bindActionCreators(removeFromCard, dispatch),
    }
}

export default withRouter(connect(mapStateToProps, mapDispatchtoProps)(OrderContainer)) as React.ComponentClass<{}>;