import Routes from "../routes"
import * as React from "react"
import { RouteComponentProps, withRouter } from "react-router-dom"
import { IApplicationState } from "../stores/index"
import { connect } from "react-redux"
import { Layout } from "../components/Layout"
import NewsSlider from "../newsSlider/NewsSlider"


interface IAppProps extends RouteComponentProps<any> {
    readonly isAuthentificated: boolean;    
}

class App extends React.Component<IAppProps, IApplicationState> {
    constructor(props: IAppProps) {
        super(props);
    }

    public render() {
        return <div>
            <Layout />
            <NewsSlider/>
            <div className="container-fluid">                
                <Routes />                
            </div>
        </div>;
    }
}

function mapStateToProps(state: IApplicationState) {    
    return {
        isAuthentificated: state.isAuthenticated
    };
}

function mapDispatchToProps() {
    return {
    };
}


export default withRouter(connect(mapStateToProps, mapDispatchToProps)(App)) as React.ComponentClass<{}>;