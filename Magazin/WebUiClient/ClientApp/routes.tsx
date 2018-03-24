import * as React from 'react';
import { Route,Switch } from "react-router-dom"
import { Home } from "./components/Home"
import { DeliveryComponent } from "./components/Delivery"
import { AboutComponent } from "./components/AboutComponent"


export default function routes() {
    return (
        <div>
            <Route path={"/"} component={AboutComponent} />
            <Route path={"/Home"} component={Home} />
            <Route path={"/Delivery"} component={DeliveryComponent} />
            <Route path={"/About"} component={AboutComponent} />
        </div>
    );
}