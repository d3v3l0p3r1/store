import { applyMiddleware, createStore, AnyAction } from "redux"
import thunkMiddleware from "redux-thunk";
import { routerMiddleware } from "connected-react-router"
import { rootReducer } from "./index"
import { IApplicationState } from "./IApplicationState"
import { IBascetState } from "../containers/bascet/BascetState"
import { initialState as BascetInitialState } from "../containers/bascet/reducer"
import { initialState as ProductGridState } from "../containers/product/reducer"
import { initialState as SliderState } from "../containers/newsSlider/reducer"
import { initialState as CategoryState } from "../containers/categories/reducer"
import { initialState as UserInitialState } from "../containers/user/reducer"
import { IUserState } from "../containers/user/UserState"
import { history } from "./configureStore"
import { RouterState } from "react-router-redux";



export default function configureStore() {
    const routeMiddleware = routerMiddleware(history);

    const stateString = localStorage.getItem("bascetState");

    const bascetState: IBascetState = stateString ? JSON.parse(stateString) as IBascetState : BascetInitialState;

    const productGridState = ProductGridState;

    const sliderState = SliderState;

    const routerState: RouterState = { location: null };

    var userState = UserInitialState;
    
    const userString = localStorage.getItem("user");
    if (userString) {
        userState = {...userState, user: JSON.parse(userString)}
    }
    
    const initialState: IApplicationState = {
        productGridState: productGridState,
        sliderState: sliderState,
        bascetState: bascetState,
        categoryState: CategoryState,
        routerState: routerState,
        userState: userState
    };

    return createStore(
        rootReducer,
        initialState,
        applyMiddleware(thunkMiddleware, routeMiddleware)
    );
}