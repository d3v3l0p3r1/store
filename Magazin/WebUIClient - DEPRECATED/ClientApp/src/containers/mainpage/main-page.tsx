﻿import * as React from "react"
import { RouteComponentProps, withRouter } from 'react-router';
import ProductGird from "../product/ProductGrid"
import { IApplicationState } from "../../stores/IApplicationState";
import { connect } from 'react-redux';
import { Dispatch, bindActionCreators, Action, ActionCreator, AnyAction } from "redux"
import  MainBascet from "../bascet/mainBascet"


export interface IMainPageProps extends RouteComponentProps<any> {
}


export class MainPage extends React.Component<IMainPageProps, {}> {

    public render() {
        return <div className="row main-page">
            <div className="col-md-4 d-none d-lg-block">
                <MainBascet />
            </div>
            <div className="col-md-8 main-grid-container">
                <ProductGird />
            </div>
        </div>;
    }
}

function mapStateToProps(state: IApplicationState, ownProps: any) {
    return {

    };
}

function mapDispatchToProps(dispatch: Dispatch<AnyAction>) {

    return {

    };
}

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(MainPage));
