import Routes from "../routes"
import * as React from "react"
import { bindActionCreators, Dispatch } from "redux";
import { RouteComponentProps, withRouter } from "react-router-dom"
import { IApplicationState } from "../stores/index"
import { connect } from "react-redux"
import { Layout } from "../components/Layout"


interface IAppProps extends RouteComponentProps<any> {
    readonly isAuthentificated: boolean;
}

class App extends React.Component<IAppProps> {
    constructor(props: IAppProps) {
        super(props);
    }

    public render() {
        return <div>
            <Layout />
            <div className="container-fluid">                
                <Routes />                
            </div>
        </div>;
    }
}

function mapStateToProps(state: IApplicationState, ownProps: IAppProps) {
    return {
        isAuthentificated: state.isAuthenticated,       
    };
}

function mapDispatchToProps(dispatch: Dispatch<IApplicationState>) {
    return {
    };
}


export default withRouter(
    connect(mapStateToProps, mapDispatchToProps)(App)
) as React.ComponentClass<{}>;