"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var react_router_dom_1 = require("react-router-dom");
var Delivery_1 = require("./components/Delivery");
var AboutComponent_1 = require("./components/AboutComponent");
var Promo_1 = require("./components/Promo/Promo");
var Feedback_1 = require("./components/Feedback/Feedback");
var Contacts_1 = require("./components/Contacts/Contacts");
var News_1 = require("./components/News");
var Login_1 = require("./components/Login/Login");
var main_page_1 = require("./containers/mainpage/main-page");
var OrderContainer_1 = require("./containers/order/OrderContainer");
function routes() {
    return (React.createElement(react_router_dom_1.Switch, null,
        React.createElement(react_router_dom_1.Route, { path: "/", exact: true, component: main_page_1.default }),
        React.createElement(react_router_dom_1.Route, { path: '/Product/:category', component: main_page_1.default }),
        React.createElement(react_router_dom_1.Route, { path: "/Delivery", component: Delivery_1.DeliveryComponent }),
        React.createElement(react_router_dom_1.Route, { path: "/About", component: AboutComponent_1.AboutComponent }),
        React.createElement(react_router_dom_1.Route, { path: "/Promo", component: Promo_1.PromoComponent }),
        React.createElement(react_router_dom_1.Route, { path: "/Feedback", component: Feedback_1.FeedbackComponent }),
        React.createElement(react_router_dom_1.Route, { path: "/Contacts", component: Contacts_1.ContactsComponent }),
        React.createElement(react_router_dom_1.Route, { path: "/News", component: News_1.NewsComponent }),
        React.createElement(react_router_dom_1.Route, { path: "/Login", component: Login_1.LoginComponent }),
        React.createElement(react_router_dom_1.Route, { path: "/Order", component: OrderContainer_1.default })));
}
exports.default = routes;
//# sourceMappingURL=routes.js.map