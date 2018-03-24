import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import {ProductGrid} from "./Products/ProductGrid"

export class Home extends React.Component<RouteComponentProps<{}>, {}> {
    public render() {
        return <div>
            <ProductGrid/>
        </div>;
    }
}
