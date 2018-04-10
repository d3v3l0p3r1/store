import * as React from "react";
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



export interface IProductGridProps extends RouteComponentProps<any> {
    isFetching: boolean,
    currentCategory: number,
    products: ReadonlyArray<Product>,
    readData: ActionCreator<ThunkAction<Action, IProductGridState, void>>;
}

export class ProductGrid extends React.Component<IProductGridProps, {}> {

    constructor(props: IProductGridProps) {
        super(props);
    }


    componentDidMount() {
        this.props.readData(this.props.currentCategory);
    }

    componentDidUpdate(previuos: any) {
        if (this.props.currentCategory !== previuos.currentCategory) {
            this.props.readData(this.props.currentCategory);
        }
    }

    public render() {

        if (this.props.isFetching) {
            return <div className="container-fluid">
                <span className="fa fa-spinner load-spinner"></span>
            </div>;
        } else {
            const listItems = this.props.products.map(p => {
                return <ProductItem price={p.price} img={p.img} title={p.title} id={p.id} key={p.id.toString()} />;
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
        currentCategory: ownProps.match.params.category,
    };
}

function mapDispatchToProps(dispatch: Dispatch<IApplicationState>) {    
    return {
        readData: bindActionCreators(gridActions.readData, dispatch)
    };
}

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(ProductGrid)) as React.ComponentClass<{}>;