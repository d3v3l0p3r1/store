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
                   <Layout/>
                   <div className="container-fluid slider-container  d-none d-lg-block">
                       <div className="container">
                           <div className="w-100 justify-content-center">
                               <NewsSlider/>
                           </div>
                           <div className="w-100 search-box-container">
                               <div className="row">
                                   <div className="col-md-8">
                                       <SearchBox/>
                                   </div>
                                   <div className="col-md-4">
                                       <FastOrderComponent/>
                                   </div>
                               </div>

                           </div>
                       </div>
                   </div>
                   <div className="container-fluid">
                       <Categories/>
                   </div>
                   <div className="container">
                       <div className="w-100 justify-content-center">
                           <Routes/>
                       </div>
                   </div>
                   <div className="container">
                       <div className="footer">
                           <div className="row">
                               <div className="col-md-4">
                                   <span>2018 "Tami-Yami" суши-бар-доставка</span>
                                   <span>ИП Балданова, ОГРНИП-</span>
                                   <span>316774600418651</span>

                                   <br/>
                                   <span>
                                       Политика конфедициальности
                                   </span>
                               </div>
                               <div className="col-md-4">
                                   <span>Московская область,<br/>
                                       Люберцы, Егорьевское<br/>
                                       шоссе, д.2,<br/>
                                       ТД "Томилино"(1 этаж)</span>

                                   <span>
                                       +7977-819-1800 <br/>
                                       +7495-142-69-88 <br/>
                                       +7926-084-3832
                                   </span>
                               </div>

                               <div className="col-md-4">
                                   <span>Мы в соц.сетях - будь с нами!</span>

                               </div>

                           </div>
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