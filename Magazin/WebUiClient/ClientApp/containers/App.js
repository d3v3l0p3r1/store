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
var routes_1 = require("../routes");
var React = require("react");
var react_router_dom_1 = require("react-router-dom");
var react_redux_1 = require("react-redux");
var Layout_1 = require("../components/Layout");
var SearchBox_1 = require("../components/SearchBox");
var NewsSlider_1 = require("./newsSlider/NewsSlider");
var FastOrder_1 = require("../components/FastOrder");
var Categories_1 = require("./categories/Categories");
var App = /** @class */ (function (_super) {
    __extends(App, _super);
    function App(props) {
        return _super.call(this, props) || this;
    }
    App.prototype.render = function () {
        return React.createElement("div", null,
            React.createElement(Layout_1.Layout, null),
            React.createElement("div", { className: "container-fluid slider-container  d-none d-lg-block" },
                React.createElement("div", { className: "container" },
                    React.createElement("div", { className: "w-100 justify-content-center" },
                        React.createElement(NewsSlider_1.default, null)),
                    React.createElement("div", { className: "w-100 search-box-container" },
                        React.createElement("div", { className: "row" },
                            React.createElement("div", { className: "col-md-8" },
                                React.createElement(SearchBox_1.SearchBox, null)),
                            React.createElement("div", { className: "col-md-4" },
                                React.createElement(FastOrder_1.FastOrderComponent, null)))))),
            React.createElement("div", { className: "container-fluid" },
                React.createElement(Categories_1.default, null)),
            React.createElement("div", { className: "container" },
                React.createElement("div", { className: "w-100 justify-content-center" },
                    React.createElement(routes_1.default, null))),
            React.createElement("div", { className: "container" },
                React.createElement("div", { className: "footer" },
                    React.createElement("div", { className: "row" },
                        React.createElement("div", { className: "col-md-4" },
                            React.createElement("span", null, "2018 \"Tami-Yami\" \u0441\u0443\u0448\u0438-\u0431\u0430\u0440-\u0434\u043E\u0441\u0442\u0430\u0432\u043A\u0430"),
                            React.createElement("span", null, "\u0418\u041F \u0411\u0430\u043B\u0434\u0430\u043D\u043E\u0432\u0430, \u041E\u0413\u0420\u041D\u0418\u041F-"),
                            React.createElement("span", null, "316774600418651"),
                            React.createElement("br", null),
                            React.createElement("span", null, "\u041F\u043E\u043B\u0438\u0442\u0438\u043A\u0430 \u043A\u043E\u043D\u0444\u0435\u0434\u0438\u0446\u0438\u0430\u043B\u044C\u043D\u043E\u0441\u0442\u0438")),
                        React.createElement("div", { className: "col-md-4" },
                            React.createElement("span", null,
                                "\u041C\u043E\u0441\u043A\u043E\u0432\u0441\u043A\u0430\u044F \u043E\u0431\u043B\u0430\u0441\u0442\u044C,",
                                React.createElement("br", null),
                                "\u041B\u044E\u0431\u0435\u0440\u0446\u044B, \u0415\u0433\u043E\u0440\u044C\u0435\u0432\u0441\u043A\u043E\u0435",
                                React.createElement("br", null),
                                "\u0448\u043E\u0441\u0441\u0435, \u0434.2,",
                                React.createElement("br", null),
                                "\u0422\u0414 \"\u0422\u043E\u043C\u0438\u043B\u0438\u043D\u043E\"(1 \u044D\u0442\u0430\u0436)"),
                            React.createElement("span", null,
                                "+7977-819-1800 ",
                                React.createElement("br", null),
                                "+7495-142-69-88 ",
                                React.createElement("br", null),
                                "+7926-084-3832")),
                        React.createElement("div", { className: "col-md-4" },
                            React.createElement("span", null, "\u041C\u044B \u0432 \u0441\u043E\u0446.\u0441\u0435\u0442\u044F\u0445 - \u0431\u0443\u0434\u044C \u0441 \u043D\u0430\u043C\u0438!"))))));
    };
    return App;
}(React.Component));
function mapStateToProps(state) {
    return {};
}
function mapDispatchToProps() {
    return {};
}
exports.default = react_router_dom_1.withRouter(react_redux_1.connect(mapStateToProps, mapDispatchToProps)(App));
//# sourceMappingURL=App.js.map