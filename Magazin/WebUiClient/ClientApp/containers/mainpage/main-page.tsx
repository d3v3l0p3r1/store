import * as React from "react"
import { RouteComponentProps, withRouter } from 'react-router';
import ProductGird from "../product/ProductGrid"
import { IApplicationState } from "../../stores/IApplicationState";
import { connect } from 'react-redux';
import { Dispatch } from "redux";


export interface IMainPageProps extends RouteComponentProps<any> {

}


export class MainPage extends React.Component<IMainPageProps, {}> {

    public render() {
        return <div>
            <ProductGird />
        </div>;
    }
}

function mapStateToProps(state: IApplicationState, ownProps: any) {
    return {

    };
}

function mapDispatchToProps(dispatch: Dispatch<IApplicationState>) {

    return {

    };
}

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(MainPage)) as React.ComponentClass<{}>;
