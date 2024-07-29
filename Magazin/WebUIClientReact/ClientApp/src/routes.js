import * as React from 'react';
import { Route, Switch } from "react-router-dom";
import { DeliveryComponent } from "./components/Delivery";
import { AboutComponent } from "./components/AboutComponent";
import { PromoComponent } from "./components/Promo/Promo";
import { FeedbackComponent } from "./components/Feedback/Feedback";
import { ContactsComponent } from "./components/Contacts/Contacts";
import { NewsComponent } from "./components/News";
import { LoginComponent } from "./components/Login/Login";
import MainPage from "./containers/mainpage/main-page";
import OrderContainer from "./containers/order/OrderContainer";
export default function routes() {
    return (React.createElement(Switch, null,
        React.createElement(Route, { path: "/", exact: true, component: MainPage }),
        React.createElement(Route, { path: '/Product/:category', component: MainPage }),
        React.createElement(Route, { path: "/Delivery", component: DeliveryComponent }),
        React.createElement(Route, { path: "/About", component: AboutComponent }),
        React.createElement(Route, { path: "/Promo", component: PromoComponent }),
        React.createElement(Route, { path: "/Feedback", component: FeedbackComponent }),
        React.createElement(Route, { path: "/Contacts", component: ContactsComponent }),
        React.createElement(Route, { path: "/News", component: NewsComponent }),
        React.createElement(Route, { path: "/Login", component: LoginComponent }),
        React.createElement(Route, { path: "/Order", component: OrderContainer })));
}
//# sourceMappingURL=routes.js.map