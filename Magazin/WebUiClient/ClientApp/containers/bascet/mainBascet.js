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
require("./styles/main-bascet.css");
var configureStore_1 = require("../../stores/configureStore");
var MainBascet = /** @class */ (function (_super) {
    __extends(MainBascet, _super);
    function MainBascet() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.onAddClick = function (product) {
            _this.props.addToCard(product);
        };
        _this.onRemoveClick = function (id) {
            _this.props.removeFromCard(id);
        };
        _this.onDeleteProductClick = function (id) {
            _this.props.removeProduct(id);
        };
        _this.onOrderClick = function () {
            configureStore_1.history.push("/Order");
        };
        return _this;
    }
    MainBascet.prototype.render = function () {
        var _this = this;
        var items = this.props.products.map(function (x) {
            return React.createElement("li", { key: x.product.id, className: "main-bascet-li" },
                React.createElement("div", { className: "main-bascet-item" },
                    React.createElement("div", { className: "container" },
                        React.createElement("div", { className: "row" },
                            React.createElement("div", { className: "col-md-10" },
                                React.createElement("span", null, x.product.title)),
                            React.createElement("div", { className: "col-md-2" },
                                React.createElement("button", { className: "btn btn-outline-danger btn-sm mb-2", onClick: function () { return _this.onDeleteProductClick(x.product.id); } },
                                    React.createElement("i", { className: "fa fa-close" })))),
                        React.createElement("div", { className: "row" },
                            React.createElement("div", { className: "col-md-8" },
                                React.createElement("button", { className: "btn btn-outline-danger btn-sm", onClick: function () { return _this.onRemoveClick(x.product.id); } },
                                    React.createElement("i", { className: "fa fa-minus" })),
                                React.createElement("span", null,
                                    "\u00A0",
                                    x.count,
                                    "\u00A0\u0448\u0442\u00A0"),
                                React.createElement("button", { className: "btn btn-outline-danger btn-sm", onClick: function () { return _this.onAddClick(x.product); } },
                                    React.createElement("i", { className: "fa fa-plus" }))),
                            React.createElement("div", { className: "col-md-4" },
                                React.createElement("span", null,
                                    x.product.price,
                                    "\u00A0\u0440\u0443\u0431"))))));
        });
        return React.createElement("div", { className: "main-basket-container" },
            React.createElement("div", { className: "row" },
                React.createElement("div", { className: " col-md-8 main-bascet" },
                    React.createElement("div", { className: "main-bascet-header-container" },
                        React.createElement("h3", { className: "main-bascet-header-text" }, "\u0412\u0430\u0448 \u0437\u0430\u043A\u0430\u0437")),
                    React.createElement("ul", null, items),
                    React.createElement("div", { className: "main-bascet-price-container" },
                        React.createElement("h3", { className: "main-bascet-price" },
                            " ",
                            this.props.total,
                            "\u00A0\u0440\u0443\u0431")),
                    React.createElement("div", { className: "main-bascet-footer" },
                        React.createElement("button", { className: "btn btn-outline-danger w-100", onClick: this.onOrderClick },
                            React.createElement("span", null, "\u041E\u0444\u043E\u0440\u043C\u0438\u0442\u044C"))))));
    };
    return MainBascet;
}(React.Component));
exports.MainBascet = MainBascet;
function mapStateToProps(state, ownProps) {
    return {
        products: state.bascetState.products,
        total: state.bascetState.totalPrice,
    };
}
function mapDispatchToProps(dispatch) {
    return {
        addToCard: redux_1.bindActionCreators(actions_1.addToCard, dispatch),
        removeFromCard: redux_1.bindActionCreators(actions_1.removeFromCard, dispatch),
        removeProduct: redux_1.bindActionCreators(actions_1.removeProductFromCard, dispatch)
    };
}
exports.default = react_redux_1.connect(mapStateToProps, mapDispatchToProps)(MainBascet);
//# sourceMappingURL=mainBascet.js.map