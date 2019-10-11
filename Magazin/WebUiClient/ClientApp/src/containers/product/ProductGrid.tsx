import * as React from "react";
import { Action, AnyAction } from "redux"
import { connect } from 'react-redux';
import { Dispatch, bindActionCreators, ActionCreator } from "redux";
import { ThunkAction } from "redux-thunk"
import { ProductItem } from "./ProductItem";
import { RouteComponentProps, withRouter } from 'react-router';
import { gridActions } from "./actions";
import { IApplicationState } from "../../stores/IApplicationState";
import { Product } from "../../models/Product";
import { IProductGridState, GridActions } from "./types";
import { IAddToCardAction, IRemoveFromCardAction } from "../bascet/Bascet"
import { addToCard, removeFromCard } from "../bascet/actions"
import { IBascetState } from "../bascet/BascetState"
import "./product.css"




export interface IProductGridProps extends RouteComponentProps<any> {
    isFetching: boolean,
    currentCategory: number,
    products: ReadonlyArray<Product>,
    readData: ActionCreator<ThunkAction<Promise<GridActions>, Product[], null, GridActions>>;
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

            let groups: { groupID: number, groupName: string, products: Product[] }[] = [];

            this.props.products.map(p => {

                var group = groups.filter(f => f.groupID === p.kindId);
                if (group.length > 0) {
                    group[0].products.push(p);
                } else {
                    var g = {
                        groupID: p.kindId,
                        groupName: p.kindName,
                        products: [p]
                    };
                    groups.push(g);
                }
            });

            const listItems = groups.map(p => {

                return <div key={p.groupID} className="product-group">
                    <h3 className="product-group-header">{p.groupName}</h3>
                    <div className="row">
                        {
                            p.products.map(x => {
                                let element = <div className="col-md-4" key={x.id.toString()}>
                                    <ProductItem product={x}
                                        itemInBucketCount={this.getCountFromBascet(x.id)}
                                        addToCard={this.props.addToCard}
                                        removeFromCard={this.props.removeFromCard} />
                                </div>;
                                return element;
                            })
                        }
                    </div>
                </div>;
            });

            return <div>                
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
        bascetState: state.bascetState,
    };
}

function mapDispatchToProps(dispatch: Dispatch<AnyAction>) {
    return {
        readData: bindActionCreators(gridActions.readData, dispatch),
        addToCard: bindActionCreators(addToCard, dispatch),
        removeFromCard: bindActionCreators(removeFromCard, dispatch),
    };
}

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(ProductGrid)) as React.ComponentClass<{}>;