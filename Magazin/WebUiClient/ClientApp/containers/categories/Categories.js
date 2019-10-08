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
var react_redux_1 = require("react-redux");
var redux_1 = require("redux");
var actions_1 = require("./actions");
var react_router_dom_1 = require("react-router-dom");
var Categories = /** @class */ (function (_super) {
    __extends(Categories, _super);
    function Categories() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    Categories.prototype.componentWillMount = function () {
        this.props.readData();
    };
    Categories.prototype.render = function () {
        var list = this.props.categories.map(function (c) {
            return React.createElement("li", { key: c.id, className: "nav-item" },
                React.createElement(react_router_dom_1.Link, { to: '/Product/' + c.id, className: "nav-link" },
                    React.createElement("span", null, c.title)));
        });
        return React.createElement("nav", { className: "navbar navbar-expand-lg main-nav navbar-dark justify-content-center" },
            React.createElement("div", { className: "d-lg-none" },
                React.createElement(react_router_dom_1.Link, { className: "navbar-brand ", to: "/" })),
            React.createElement("button", { id: "toggler", type: "button", className: "navbar-toggler navbar-toggler-right", "data-toggle": "collapse", "data-target": "#category-navbar", "aria-expanded": "false", "aria-controls": "navbar" },
                React.createElement("span", { className: "navbar-toggler-icon" })),
            React.createElement("div", { id: "category-navbar", className: "navbar-collapse collapse w-100" },
                React.createElement("ul", { className: 'navbar-nav w-100 justify-content-center' }, list)));
    };
    return Categories;
}(React.Component));
exports.Categories = Categories;
function mapStateToProps(storeState, ownProps) {
    return {
        categories: storeState.categoryState.categories,
    };
}
function mapDispatchToProps(dispatch) {
    return {
        readData: redux_1.bindActionCreators(actions_1.readData, dispatch),
    };
}
exports.default = react_redux_1.connect(mapStateToProps, mapDispatchToProps)(Categories);
//# sourceMappingURL=Categories.js.map