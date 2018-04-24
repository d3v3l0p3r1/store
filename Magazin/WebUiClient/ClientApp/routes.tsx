import * as React from 'react';
import { Route, Switch, Redirect } from "react-router-dom"
import { DeliveryComponent } from "./components/Delivery"
import { AboutComponent } from "./components/AboutComponent"
import { PromoComponent } from "./components/Promo/Promo"
import { FeedbackComponent } from "./components/Feedback/Feedback"
import { ContactsComponent } from "./components/Contacts/Contacts"
import { NewsComponent } from "./components/News"
import { LoginComponent } from "./components/Login/Login"
import MainPage from "./containers/mainpage/main-page"
import OrderContainer from "./containers/order/OrderContainer"


export default function routes() {
    return (
        <Switch>             
            <Route path={"/"} exact={true} component={MainPage}/>
            <Route path={'/Product/:category'} component={MainPage} />
            <Route path={"/Delivery"} component={DeliveryComponent} />
            <Route path={"/About"} component={AboutComponent} />
            <Route path={"/Promo"} component={PromoComponent} />
            <Route path={"/Feedback"} component={FeedbackComponent} />
            <Route path={"/Contacts"} component={ContactsComponent} />
            <Route path={"/News"} component={NewsComponent} />
            <Route path={"/Login"} component={LoginComponent} />
            <Route path={"/Order"} component={OrderContainer} />
        </Switch>
    );
} 