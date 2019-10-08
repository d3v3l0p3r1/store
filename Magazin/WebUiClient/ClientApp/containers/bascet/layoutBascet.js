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
var configureStore_1 = require("../../stores/configureStore");
require("./styles/layout-bascet.css");
var LayoutBascet = /** @class */ (function (_super) {
    __extends(LayoutBascet, _super);
    function LayoutBascet() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.onOrderClick = function () {
            configureStore_1.history.push("/Order");
        };
        return _this;
    }
    LayoutBascet.prototype.render = function () {
        var _this = this;
        return React.createElement("div", { className: "layout-bascet-container" },
            React.createElement("button", { className: "btn btn-outline-danger", onClick: function () { return _this.onOrderClick(); } },
                React.createElement("span", { className: "fa fa-shopping-cart" }, "\u00A0"),
                React.createElement("span", null,
                    this.props.total,
                    "\u00A0\u00A0"),
                React.createElement("span", null, "\u041A\u043E\u0440\u0437\u0438\u043D\u0430")));
    };
    return LayoutBascet;
}(React.Component));
function mapStateToProps(state, ownProps) {
    return {
        products: state.bascetState.products,
        total: state.bascetState.totalCount,
    };
}
function mapDispatchToProps(dispatch) {
    return {};
}
exports.default = react_redux_1.connect(mapStateToProps, mapDispatchToProps)(LayoutBascet);
//# sourceMappingURL=layoutBascet.js.map