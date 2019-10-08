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
var ProductItem_1 = require("./ProductItem");
var react_router_1 = require("react-router");
var actions_1 = require("./actions");
var actions_2 = require("../bascet/actions");
require("./Product.css");
var ProductGrid = /** @class */ (function (_super) {
    __extends(ProductGrid, _super);
    function ProductGrid(props) {
        var _this = _super.call(this, props) || this;
        _this.getCountFromBascet = function (id) {
            var items = _this.props.bascetState.products.filter(function (x) { return x.product.id === id; });
            if (items.length > 0) {
                return items[0].count;
            }
            return 0;
        };
        return _this;
    }
    ProductGrid.prototype.componentWillUpdate = function (next) {
        if (next.currentCategory !== this.props.currentCategory) {
            this.props.readData(next.currentCategory);
        }
    };
    ProductGrid.prototype.componentDidMount = function () {
        this.props.readData(this.props.currentCategory);
    };
    ProductGrid.prototype.render = function () {
        var _this = this;
        if (this.props.isFetching) {
            return React.createElement("div", { className: "container-fluid" },
                React.createElement("span", { className: "fa fa-spinner load-spinner" }));
        }
        else {
            var groups_1 = [];
            this.props.products.map(function (p) {
                var group = groups_1.filter(function (f) { return f.groupID === p.kindId; });
                if (group.length > 0) {
                    group[0].products.push(p);
                }
                else {
                    var g = {
                        groupID: p.kindId,
                        groupName: p.kindName,
                        products: [p]
                    };
                    groups_1.push(g);
                }
            });
            var listItems = groups_1.map(function (p) {
                return React.createElement("div", { key: p.groupID, className: "product-group" },
                    React.createElement("h3", { className: "product-group-header" }, p.groupName),
                    React.createElement("div", { className: "row" }, p.products.map(function (x) {
                        var element = React.createElement("div", { className: "col-md-4", key: x.id.toString() },
                            React.createElement(ProductItem_1.ProductItem, { product: x, itemInBucketCount: _this.getCountFromBascet(x.id), addToCard: _this.props.addToCard, removeFromCard: _this.props.removeFromCard }));
                        return element;
                    })));
            });
            return React.createElement("div", null, listItems);
        }
    };
    return ProductGrid;
}(React.Component));
exports.ProductGrid = ProductGrid;
function mapStateToProps(state, ownProps) {
    return {
        isFetching: state.productGridState.isBusy,
        products: state.productGridState.products,
        currentCategory: ownProps.match.params.category,
        bascetState: state.bascetState,
    };
}
function mapDispatchToProps(dispatch) {
    return {
        readData: redux_1.bindActionCreators(actions_1.gridActions.readData, dispatch),
        addToCard: redux_1.bindActionCreators(actions_2.addToCard, dispatch),
        removeFromCard: redux_1.bindActionCreators(actions_2.removeFromCard, dispatch),
    };
}
exports.default = react_router_1.withRouter(react_redux_1.connect(mapStateToProps, mapDispatchToProps)(ProductGrid));
//# sourceMappingURL=ProductGrid.js.map