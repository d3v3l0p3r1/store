import * as React from "react"
import { connect } from 'react-redux';
import { RouteComponentProps, withRouter } from 'react-router';
import { Action, Dispatch, bindActionCreators, ActionCreator, AnyAction } from "redux";
import { ThunkAction } from "redux-thunk"
import { IAddToCardAction, IRemoveFromCardAction } from "../bascet/Bascet"
import { IBascetState } from "../bascet/BascetState"
import { IApplicationState } from "../../stores/IApplicationState";
import { addToCard, removeFromCard } from "../bascet/actions"
import Product from "../../models/Product"
import UserModel from "../../models/UserModel"
import { OrderAction } from "./actions"
import { IOrderState } from "./OrderState"
import { IOrderModel, Order } from "../../models/OrderModel"
import { OrderActions } from "./types"
import "./Order.css"

export interface IOrderContainerProps extends RouteComponentProps<any> {
    addToCard: ActionCreator<IAddToCardAction>;
    removeFromCard: ActionCreator<IRemoveFromCardAction>;
    user: UserModel | null,
    bascetState: IBascetState;
    error: string,
    fetching: boolean,
    complete: boolean,
    orderAction: ActionCreator<ThunkAction<Promise<OrderActions>, Order, null, OrderActions>>,
}

export interface IOrderContainerState {
    name: string,
    phone: string,
    delivery: number,
    address: string,
    comment: string,
    change: string, // Сдача с какой суммы
    delyveryTime: number,
    personCount: number
}

export enum DeliveryType {
    Delivery = 0,
    Pickup = 1,
}

export enum DeliveryTime {
    Today = 0,
    Tomorow = 1,
}

class OrderContainer extends React.Component<IOrderContainerProps, IOrderContainerState> {

    private orderModel!: IOrderModel;

    constructor(props: IOrderContainerProps) {
        super(props);

        this.state = {
            name: "",
            address: "",
            change: "",
            comment: "",
            delivery: DeliveryType.Delivery,
            delyveryTime: DeliveryTime.Today,
            personCount: 1,
            phone: ""
        };
    }

    public componentWillMount() {

        if (this.props.user) {
            this.setState({
                address: this.props.user.address,
                name: this.props.user.name,
                phone: this.props.user.phone
            });
        }
    }


    onAddClick = (product: Product) => {
        this.props.addToCard(product);
    }

    onRemoveClick = (id: number) => {
        this.props.removeFromCard(id);
    }

    onDeliveryTypeChange = (e: any) => {
        var value = e.target.value;
        this.setState({
            delivery: value
        });
    }


    onNameChange = (e: any) => {
        var value = e.target.value;

        this.setState({
            name: value
        });
    }

    onPhoneChange = (e: any) => {
        var value = e.target.value;

        this.setState({
            phone: value
        });
    }

    onAddressChange = (e: any) => {
        var value = e.target.value;

        this.setState({
            address: value
        });
    }

    onCommentChange = (e: any) => {
        var value = e.target.value;

        this.setState({
            comment: value
        });
    }

    onChangeChange = (e: any) => {
        var value = e.target.value;

        this.setState({
            change: value
        });
    }

    onPersonCountChange = (e: any) => {
        var value = e.target.value;

        this.setState({
            personCount: value
        });
    }

    orderRequest = () => {
        this.orderModel = {
            address: this.state.address,
            change: this.state.change,
            comment: this.state.comment,
            deliveryTime: this.state.delyveryTime,
            deliveryType: this.state.delivery,
            name: this.state.name,
            personCount: this.state.personCount,
            phone: this.state.phone,
            products: this.props.bascetState.products
        };
        this.props.orderAction(this.orderModel);
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

        const userData = <div className="order-user-container">
            <div className="form-group">
                <input disabled={this.props.fetching}
                    onChange={this.onNameChange}
                    className="form-control"
                    placeholder="Ваше имя"
                    value={this.state.name} />
            </div>

            <div className="form-group">
                <input disabled={this.props.fetching}
                    onChange={this.onPhoneChange}
                    className="form-control"
                    placeholder="Телефон"
                    value={this.state.phone} />
            </div>

            <div className="form-group row">
                <label htmlFor="Delivery" className="col-sm-6 col-form-label">Способ получения заказа</label>
                <div className="col-sm-6">
                    <select className="form-control" onChange={this.onDeliveryTypeChange}>
                        <option value={DeliveryType.Delivery}>Доставка</option>
                        <option value={DeliveryType.Pickup}>Самовывоз</option>
                    </select>
                </div>
            </div>

            <div className="form-group">
                <input disabled={this.props.fetching}
                    onChange={this.onAddressChange}
                    className="form-control"
                    placeholder="Адрес доставки"
                    value={this.state.address} />
            </div>

            <div className="form-group">
                <textarea disabled={this.props.fetching}
                    onChange={this.onCommentChange}
                    className="form-control"
                    placeholder="Коментарий к заказу"
                    value={this.state.comment}
                    rows={2}></textarea>
            </div>


            <div className="form-group row">
                <label htmlFor="change" className="col-sm-6 col-form-label">С какой суммы готовить сдачу</label>
                <div className="col-sm-6">
                    <input id="change"
                        disabled={this.props.fetching}
                        onChange={this.onChangeChange}
                        className="form-control"
                        placeholder="5000"
                        value={this.state.change} />
                </div>
            </div>

            <div className="form-group row">
                <label htmlFor="DeliveryTime" className="col-sm-6 col-form-label">Время доставки</label>
                <div className="col-sm-6">
                    <select className="form-control" id="DeliveryTime" onChange={this.onDeliveryTypeChange}>
                        <option value={DeliveryTime.Today}>Ближайшее</option>
                        <option value={DeliveryTime.Tomorow}>Завтра</option>
                    </select>
                </div>
            </div>


            <div className="form-group row">
                <label htmlFor="personCount" className="col-sm-6 col-form-label">Количество приборов</label>
                <div className="col-sm-6">
                    <input id="personCount"
                        type="number"
                        disabled={this.props.fetching}
                        onChange={this.onPersonCountChange}
                        className="form-control"
                        placeholder="5000"
                        value={this.state.personCount} />
                </div>
            </div>
        </div>;

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

                <div className="order-user-container-wrapper">
                    {userData}
                </div>
                <div className="order-user-container-wrapper">
                    <button className="btn btn-outline-danger" onClick={this.orderRequest}>Оформить</button>
                </div>
            </div>


        </div>;
    }
}


function mapStateToProps(state: IApplicationState, ownProps: any) {
    return {
        bascetState: state.bascetState,
        user: state.userState.user,

    }
}

function mapDispatchtoProps(dispatch: Dispatch<AnyAction>) {
    return {
        addToCard: bindActionCreators(addToCard, dispatch),
        removeFromCard: bindActionCreators(removeFromCard, dispatch),
        orderAction: bindActionCreators(OrderAction, dispatch)
    }
}

export default withRouter(connect(mapStateToProps, mapDispatchtoProps)(OrderContainer));