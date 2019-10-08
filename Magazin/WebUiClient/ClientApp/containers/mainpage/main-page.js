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
var react_router_1 = require("react-router");
var ProductGrid_1 = require("../product/ProductGrid");
var react_redux_1 = require("react-redux");
var mainBascet_1 = require("../bascet/mainBascet");
var MainPage = /** @class */ (function (_super) {
    __extends(MainPage, _super);
    function MainPage() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    MainPage.prototype.render = function () {
        return React.createElement("div", { className: "row main-page" },
            React.createElement("div", { className: "col-md-4 d-none d-lg-block" },
                React.createElement(mainBascet_1.default, null)),
            React.createElement("div", { className: "col-md-8 main-grid-container" },
                React.createElement(ProductGrid_1.default, null)));
    };
    return MainPage;
}(React.Component));
exports.MainPage = MainPage;
function mapStateToProps(state, ownProps) {
    return {};
}
function mapDispatchToProps(dispatch) {
    return {};
}
exports.default = react_router_1.withRouter(react_redux_1.connect(mapStateToProps, mapDispatchToProps)(MainPage));
//# sourceMappingURL=main-page.js.map