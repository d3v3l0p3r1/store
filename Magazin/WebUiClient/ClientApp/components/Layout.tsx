import * as React from 'react';
import { NavMenu } from './NavMenu';
import 'bootstrap/dist/css/bootstrap.css';

export interface LayoutProps {
    children?: React.ReactNode;
}

export interface ILayoutState {

}



export class Layout extends React.Component<LayoutProps, ILayoutState> {

    constructor(props: LayoutProps) {
        super(props);
    }


    componentWillMount() {

    }

    componentDidMount() {
    }


    public render() {

        return <div className='container-fluid'>


            <div className="header d-none d-lg-block">
                <div className="row">
                    <div className="col-md-4" >
                        <img src="../images/logo.png" />
                    </div>
                    <div className="col-md-4">
                        <span>
                            Московская область, Люберцы <br />
                            Егорьевское шоссе, д.2 <br />
                            ТД "Томилино" (1 этаж)
                                          </span>
                    </div>
                    <div className="col-md-4">
                        <span>
                            +7977-819-1800 <br />
                            +7977-543-0134 <br />
                            +7926-084-3832
                            </span>
                    </div>
                </div>
            </div>

            <div className="clearfix"></div>

            <NavMenu />

            <div className="clearfix"></div>

        </div>;
    }
}