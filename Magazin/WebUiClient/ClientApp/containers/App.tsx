import Routes from "../routes";
import * as React from "react";
import { RouteComponentProps, withRouter } from "react-router-dom";
import { IApplicationState } from "../stores/IApplicationState";
import { connect } from "react-redux";
import { Layout } from "../components/Layout";
import { SearchBox } from "../components/SearchBox"
import NewsSlider from "./newsSlider/NewsSlider";
import { FastOrderComponent } from "../components/FastOrder"
import Categories  from "./categories/Categories"


interface IAppProps extends RouteComponentProps<any> {
    
}

class App extends React.Component<IAppProps, IApplicationState> {
    constructor(props: IAppProps) {
        super(props);
    }

    public render() {
        return <div>
            <Layout />
            <div className="container-fluid slider-container  d-none d-lg-block">
                <div className="container">
                    <div className="w-100 justify-content-center">
                        <NewsSlider />
                    </div>
                    <div className="w-100 search-box-container">
                        <div className="row">
                            <div className="col-md-8">
                                <SearchBox />
                            </div>
                            <div className="col-md-4">
                                <FastOrderComponent />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div className="container-fluid">
                <Categories />
            </div>
            <div className="container">
                <div className="w-100 justify-content-center">
                    <Routes />
                </div>
            </div>

        </div>;
    }
}

function mapStateToProps(state: IApplicationState) {
    return {
        
    };
}

function mapDispatchToProps() {
    return {
    };
}


export default withRouter(connect(mapStateToProps, mapDispatchToProps)(App)) as React.ComponentClass<{}>;