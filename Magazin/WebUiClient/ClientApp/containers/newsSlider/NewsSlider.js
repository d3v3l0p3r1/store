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
var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var react_slick_1 = require("react-slick");
var react_redux_1 = require("react-redux");
var redux_1 = require("redux");
var actions_1 = require("./actions");
require("slick-carousel/slick/slick.css");
require("slick-carousel/slick/slick-theme.css");
var NewsSlider = /** @class */ (function (_super) {
    __extends(NewsSlider, _super);
    function NewsSlider(props) {
        return _super.call(this, props) || this;
    }
    NewsSlider.prototype.componentWillMount = function () {
        this.props.readNewsAction();
    };
    NewsSlider.prototype.render = function () {
        var settings = {
            dots: true,
            infinite: true,
            speed: 500,
            slidesToShow: 1,
            slidesToScroll: 1
        };
        var list = this.props.news.map(function (p) {
            return React.createElement("div", { key: p.id.toString() },
                React.createElement("img", { src: p.image, className: "w-100", height: "340px;" }));
        });
        return React.createElement("div", { className: "slider-wrapper" },
            React.createElement(react_slick_1.default, __assign({}, settings), list));
    };
    return NewsSlider;
}(React.Component));
function mapStateToProps(storeState) {
    return {
        news: storeState.sliderState.newsArray
    };
}
function mapDispatchToProps(dispatch) {
    return {
        readNewsAction: redux_1.bindActionCreators(actions_1.readNewsAction, dispatch)
    };
}
exports.default = (react_redux_1.connect(mapStateToProps, mapDispatchToProps)(NewsSlider));
//# sourceMappingURL=NewsSlider.js.map