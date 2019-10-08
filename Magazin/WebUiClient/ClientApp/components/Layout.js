"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var NavMenu_1 = require("./NavMenu");
var react_router_dom_1 = require("react-router-dom");
var layoutBascet_1 = require("../containers/bascet/layoutBascet");
require("bootstrap/dist/css/bootstrap.css");
var Layout = /** @class */ (function (_super) {
    __extends(Layout, _super);
    function Layout(props) {
        return _super.call(this, props) || this;
    }
    Layout.prototype.render = function () {
        return React.createElement("div", { className: "container-fluid header" },
            React.createElement("div", { className: "container" },
                React.createElement("div", { className: "contact-container d-none d-lg-block" },
                    React.createElement("div", { className: "row" },
                        React.createElement("div", { className: "col-md-4" },
                            React.createElement(react_router_dom_1.Link, { to: '/' },
                                React.createElement("img", { src: "../images/logo.png" }))),
                        React.createElement("div", { className: "col-md-3" },
                            React.createElement("span", null,
                                "\u041C\u043E\u0441\u043A\u043E\u0432\u0441\u043A\u0430\u044F \u043E\u0431\u043B\u0430\u0441\u0442\u044C, \u041B\u044E\u0431\u0435\u0440\u0446\u044B ",
                                React.createElement("br", null),
                                "\u0415\u0433\u043E\u0440\u044C\u0435\u0432\u0441\u043A\u043E\u0435 \u0448\u043E\u0441\u0441\u0435, \u0434.2 ",
                                React.createElement("br", null),
                                "\u0422\u0414 \"\u0422\u043E\u043C\u0438\u043B\u0438\u043D\u043E\" (1 \u044D\u0442\u0430\u0436)")),
                        React.createElement("div", { className: "col-md-3" },
                            React.createElement("span", null,
                                "+7977-819-1800 ",
                                React.createElement("br", null),
                                "+7977-543-0134 ",
                                React.createElement("br", null),
                                "+7926-084-3832")),
                        React.createElement("div", { className: "col-md-2" },
                            React.createElement(layoutBascet_1.default, null))))),
            React.createElement("div", { className: "clearfix" }),
            React.createElement("div", { className: "container-fluid" },
                React.createElement(NavMenu_1.NavMenu, null),
                React.createElement("div", { className: "clearfix" })));
    };
    return Layout;
}(React.Component));
exports.Layout = Layout;
//# sourceMappingURL=Layout.js.map