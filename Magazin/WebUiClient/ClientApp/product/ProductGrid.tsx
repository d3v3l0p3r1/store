import * as React from "react";
import { Action } from "redux"
import { connect } from 'react-redux';
import { Dispatch, bindActionCreators, ActionCreator } from "redux";
import { ThunkAction } from "redux-thunk"
import { ProductItem, IProductItemProps } from "./ProductItem";
import { RouteComponentProps, withRouter } from 'react-router';
import { gridActions } from "./actions";
import { IApplicationState } from "../stores/index";
import { Product } from "../models/Product";
import { IProductGridState, IReadProductAction, IErrorAction, IFetchingAction } from "./types";



export interface IProductGridProps extends RouteComponentProps<any> {
    products: ReadonlyArray<Product>,
    dispatch: (action: any) => void;
    readData: ActionCreator<ThunkAction<Action, IProductGridState, void>>;    
}

class ProductGrid extends React.Component<IProductGridProps, {}> {

    constructor(props: IProductGridProps) {
        super(props);
    }

    componentWillMount() {
        this.props.readData(1);
    }

    public render() {
        const listItems = this.props.products.map(p => {
            return <ProductItem price={p.price} img={p.img} title={p.title} id={p.id} key={p.id.toString()} />;
        });

        return <div>
            <h1>Products</h1>
            {listItems}
        </div>;
    }

}

function mapStateToProps(state: IApplicationState, ownProps: any) {
    return {
        products: state.productGridState.products
    };
}

function mapDispatchToProps(dispatch: Dispatch<IApplicationState>) {
    debugger;

    return {
        readData: bindActionCreators(gridActions.readData, dispatch)
    };
}

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(ProductGrid)) as React.ComponentClass<{}>;