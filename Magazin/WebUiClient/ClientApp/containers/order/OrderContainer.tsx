import * as React from "react"
import { connect } from 'react-redux';
import { RouteComponentProps, withRouter } from 'react-router';
import { Dispatch, bindActionCreators, ActionCreator } from "redux";
import { IAddToCardAction, IRemoveFromCardAction, IBascetState } from "../bascet/Bascet"
import { IApplicationState } from "../../stores/IApplicationState";
import { addToCard, removeFromCard } from "../bascet/actions"

export interface IOrderContainerProps extends RouteComponentProps<any>{
    addToCard: ActionCreator<IAddToCardAction>;
    removeFromCard: ActionCreator<IRemoveFromCardAction>;
    bascetState: IBascetState;
}

class OrderContainer extends React.Component<IOrderContainerProps> {

    public render() {

        const list = this.props.bascetState.products.map(p => {
            return <span>{p.product.title}: {p.product.price}</span>;
        });

        return <div>
            <h1>Order</h1>;
            {list}

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