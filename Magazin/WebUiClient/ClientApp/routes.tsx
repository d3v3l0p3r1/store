import * as React from 'react';
import { Route, Switch, Redirect } from "react-router-dom"
import { Home } from "./components/Home"
import { DeliveryComponent } from "./components/Delivery"
import { AboutComponent } from "./components/AboutComponent"
import { PromoComponent } from "./components/Promo/Promo"
import { FeedbackComponent } from "./components/Feedback/Feedback"
import { ContactsComponent } from "./components/Contacts/Contacts"
import { NewsComponent } from "./components/News"
import { LoginComponent } from "./components/Login/Login"
import ProductGrid from "./containers/product/ProductGrid"

export default function routes() {
    return (
        <Switch>            
            <Redirect exact={true} path={"/"} to={"/Home"} />
            <Route path={"/Home"} component={Home} />
            <Route path={"/Delivery"} component={DeliveryComponent} />
            <Route path={"/About"} component={AboutComponent} />
            <Route path={"/Promo"} component={PromoComponent} />
            <Route path={"/Feedback"} component={FeedbackComponent} />
            <Route path={"/Contacts"} component={ContactsComponent} />
            <Route path={"/News"} component={NewsComponent} />
            <Route path={"/Login"} component={LoginComponent}/>
            <Route path={"/Product"} component={ProductGrid} />
        </Switch>
    );
}