import * as React from "react"
import { IBascetItem } from "./Bascet"
import { IApplicationState } from "../../stores/IApplicationState";
import { Dispatch } from "redux";
import { connect } from "react-redux"
import { history } from "../../stores/configureStore"
import "./styles/layout-bascet.css"

export interface ILayoutBascetProps {
    readonly total: number;
    readonly products: ReadonlyArray<IBascetItem>,
}

class LayoutBascet extends React.Component<ILayoutBascetProps, {}> {

    onOrderClick = () => {
        history.push("/Order");
    }

    public render() {
        return <div className="layout-bascet-container">

            <button className="btn btn-outline-danger" onClick={() => this.onOrderClick()} >
                <span className="fa fa-shopping-cart">&nbsp;</span>
                <span>{this.props.total}&nbsp;&nbsp;</span>
                <span>Корзина</span>
            </button>

        </div>;
    }
}


function mapStateToProps(state: IApplicationState, ownProps: any) {

    return {
        products: state.bascetState.products,
        total: state.bascetState.totalCount,
    };
}

function mapDispatchToProps(dispatch: Dispatch<IApplicationState>) {
    return {
    };
}


export default connect(mapStateToProps, mapDispatchToProps)(LayoutBascet) as React.ComponentClass<{}>;