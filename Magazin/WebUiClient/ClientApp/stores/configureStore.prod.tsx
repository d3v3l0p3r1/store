import { applyMiddleware, createStore } from "redux"
import thunkMiddleware from "redux-thunk";
import { rootReducer } from "./index"
import { routerMiddleware } from "react-router-redux"
import { history } from "./configureStore"
import { IApplicationState } from "./IApplicationState"
import { IBascetState } from "../containers/bascet/Bascet"
import { initialState as BascetInitialState } from "../containers/bascet/reducer"
import { initialState as ProductGridState } from "../containers/product/reducer"
import { initialState as SliderState } from "../containers/newsSlider/reducer"
import { initialState as CategoryState } from "../containers/categories/reducer"
import { RouterState } from "react-router-redux";


export default function configureStore() {
    const routeMiddleware = routerMiddleware(history);

    const stateString = localStorage.getItem("bascetState");

    const bascetState: IBascetState = stateString ? JSON.parse(stateString) as IBascetState : BascetInitialState;

    const productGridState = ProductGridState;

    const sliderState = SliderState;
    
    const routerState: RouterState = { location: null };

    const initialState: IApplicationState = {
        productGridState: productGridState,
        sliderState: sliderState,
        bascetState: bascetState,
        categoryState: CategoryState,        
        routerState: routerState,        
    };

    return createStore<IApplicationState>(
        rootReducer,
        initialState,
        applyMiddleware(thunkMiddleware, routeMiddleware)
    );
}