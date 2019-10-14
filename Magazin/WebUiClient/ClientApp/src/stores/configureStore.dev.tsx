import { applyMiddleware, createStore, AnyAction, Reducer, combineReducers } from "redux"
import thunkMiddleware from "redux-thunk";
import { routerMiddleware, RouterState } from "connected-react-router"
import { IApplicationState } from "./IApplicationState"
import { IBascetState } from "../containers/bascet/BascetState"
import { initialState as BascetInitialState } from "../containers/bascet/reducer"
import { initialState as ProductGridState } from "../containers/product/reducer"
import { initialState as SliderState } from "../containers/newsSlider/reducer"
import { initialState as CategoryState } from "../containers/categories/reducer"
import { initialState as UserInitialState } from "../containers/user/reducer"
import { IUserState } from "../containers/user/UserState"
import { history } from "./configureStore"
import { productGridReducer } from "../containers/product/reducer"
import { sliderReducer } from "../containers/newsSlider/reducer"
import { categoriesReducer } from "../containers/categories/reducer"
import { bascetReducer } from "../containers/bascet/reducer"
import { userReducer } from "../containers/user/reducer"
import { connectRouter } from "connected-react-router"



export default function configureStore() {
    const routeMiddleware = routerMiddleware(history);

    const stateString = localStorage.getItem("bascetState");

    const bascetState: IBascetState = stateString ? JSON.parse(stateString) as IBascetState : BascetInitialState;

    const productGridState = ProductGridState;

    const sliderState = SliderState;

    const routerState: RouterState = {
        action: history.action,
        location: history.location
    };

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
        userState: userState,
        router: routerState
    };

    const rootReducer: Reducer<IApplicationState> = combineReducers<IApplicationState>({
        productGridState: productGridReducer,
        sliderState: sliderReducer,
        categoryState: categoriesReducer,
        bascetState: bascetReducer,
        userState: userReducer,
        router: connectRouter(history)
    });


    return createStore(
        rootReducer,
        initialState,
        applyMiddleware(thunkMiddleware, routeMiddleware)
    );
}