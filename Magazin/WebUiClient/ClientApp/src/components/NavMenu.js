import * as React from 'react';
import { Link } from 'react-router-dom';
import UserNavMenuItem from "../containers/user/UserNavMenuItem";
export class NavMenu extends React.Component {
    render() {
        return React.createElement("nav", { className: "navbar navbar-expand-lg navbar-dark main-nav justify-content-center" },
            React.createElement("div", { className: "d-lg-none" },
                React.createElement(Link, { className: "navbar-brand ", to: "/" },
                    React.createElement("img", { src: "../images/logo.png" }))),
            React.createElement("button", { id: "toggler", type: "button", className: "navbar-toggler navbar-toggler-right", "data-toggle": "collapse", "data-target": "#main-navbar", "aria-expanded": "false", "aria-controls": "navbar" },
                React.createElement("span", { className: "navbar-toggler-icon" })),
            React.createElement("div", { id: "main-navbar", className: "navbar-collapse collapse w-100" },
                React.createElement("ul", { className: 'navbar-nav w-100 justify-content-center' },
                    React.createElement("li", { className: "nav-item" },
                        React.createElement(Link, { to: '/About', className: "nav-link" },
                            React.createElement("span", { className: "" }, "\u041E \u043D\u0430\u0441"))),
                    React.createElement("li", { className: "nav-item" },
                        React.createElement(Link, { to: '/Delivery', className: "nav-link" },
                            React.createElement("span", null, "\u0414\u043E\u0441\u0442\u0430\u0432\u043A\u0430 \u0438 \u043E\u043F\u043B\u0430\u0442\u0430"))),
                    React.createElement("li", { className: "nav-item" },
                        React.createElement(Link, { to: '/Promo', className: "nav-link" },
                            React.createElement("span", null, "\u0410\u043A\u0446\u0438\u0438"))),
                    React.createElement("li", { className: "nav-item" },
                        React.createElement(Link, { to: '/Feedback', className: "nav-link" },
                            React.createElement("span", null, "\u041E\u0442\u0437\u044B\u0432\u044B"))),
                    React.createElement("li", { className: "nav-item" },
                        React.createElement(Link, { to: '/Contacts', className: "nav-link" },
                            React.createElement("span", null, "\u041A\u043E\u043D\u0442\u0430\u043A\u0442\u044B"))),
                    React.createElement("li", { className: "nav-item" },
                        React.createElement(Link, { to: '/News', className: "nav-link" },
                            React.createElement("span", null, "\u0421\u0442\u0430\u0442\u044C\u0438 | \u041D\u043E\u0432\u043E\u0441\u0442\u0438"))),
                    React.createElement("li", { className: "nav-item" },
                        React.createElement(UserNavMenuItem, null)))));
    }
}
//# sourceMappingURL=NavMenu.js.map