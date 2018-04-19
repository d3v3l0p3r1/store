﻿import * as React from "react";
import { Action } from "redux"
import { connect } from 'react-redux';
import { Dispatch, bindActionCreators, ActionCreator } from "redux";
import { ThunkAction } from "redux-thunk"
import { ProductItem, IProductItemProps } from "./ProductItem";
import { RouteComponentProps, withRouter } from 'react-router';
import { gridActions } from "./actions";
import { IApplicationState } from "../../stores/IApplicationState";
import { Product } from "../../models/Product";
import { IProductGridState } from "./types";
import { IAddToCardAction, IRemoveFromCardAction, IBascetState } from "../bascet/Bascet"
import { addToCard, removeFromCard } from "../bascet/actions"




export interface IProductGridProps extends RouteComponentProps<any> {
    isFetching: boolean,
    currentCategory: number,
    products: ReadonlyArray<Product>,
    readData: ActionCreator<ThunkAction<Action, IProductGridState, void>>;
    addToCard: ActionCreator<IAddToCardAction>;
    removeFromCard: ActionCreator<IRemoveFromCardAction>;
    bascetState: IBascetState;
}

export class ProductGrid extends React.Component<IProductGridProps, {}> {

    constructor(props: IProductGridProps) {
        super(props);
    }


    public componentWillUpdate(next: IProductGridProps) {        
        if (next.currentCategory !== this.props.currentCategory) {
            this.props.readData(next.currentCategory);
        }
    }

    public componentDidMount() {
        this.props.readData(this.props.currentCategory);
    }   

    getCountFromBascet = (id: number) => {        
        var items = this.props.bascetState.products.filter(x => x.product.id === id);

        if (items.length > 0) {            
            return items[0].count;
        }

        return 0;
    }

    public render() {        
        if (this.props.isFetching) {
            return <div className="container-fluid">
                <span className="fa fa-spinner load-spinner"></span>
            </div>;
        } else {
            const listItems = this.props.products.map(p => {
                return <div className="col-md-4" key={p.id.toString()}>
                    <ProductItem product={p} itemInBucketCount={this.getCountFromBascet(p.id)} addToCard={this.props.addToCard} removeFromCard={this.props.removeFromCard} />
                </div>;
            });

            return <div className="row">
                {listItems}
            </div>;
        }

    }


}

function mapStateToProps(state: IApplicationState, ownProps: any) {
    return {
        isFetching: state.productGridState.isBusy,
        products: state.productGridState.products,
        currentCategory: state.productGridState.currentCategory,
        bascetState: state.bascetState,
    };
}

function mapDispatchToProps(dispatch: Dispatch<IApplicationState>) {
    return {
        readData: bindActionCreators(gridActions.readData, dispatch),
        addToCard: bindActionCreators(addToCard, dispatch),
        removeFromCard: bindActionCreators(removeFromCard, dispatch),
    };
}

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(ProductGrid)) as React.ComponentClass<{}>;