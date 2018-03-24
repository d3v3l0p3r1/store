import * as React from 'react';
import { NavMenu } from './NavMenu';

export interface LayoutProps {
    children?: React.ReactNode;
}

export class Layout extends React.Component<LayoutProps, {}> {
    public render() {
        return <div className='container-fluid'>
            <div className="row">
                <div className="col-md-4">
                    <img src="" width="240" height="8" />
                </div>

                <div className="col-md-2">
                    <span>
                        Московская область, Люберцы <br />
                        Егорьевское шоссе, д.2 <br />
                        ТД "Томилино" (1 этаж)
                    </span>
                </div>

                <div className="col-md-2">
                    <span>
                        +7977-819-1800 <br />
                        +7977-543-0134 <br />
                        +7926-084-3832
                    </span>
                </div>
            </div>
            <div className="clearfix"></div>
            <div className="row">
                <NavMenu />
            </div>

            <div className="clearfix"></div>           
        </div>;
    }
}
