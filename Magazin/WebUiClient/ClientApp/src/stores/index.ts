import { combineReducers, Reducer } from 'redux';
import { productGridReducer } from "../containers/product/reducer"
import { sliderReducer } from "../containers/newsSlider/reducer"
import { categoriesReducer } from "../containers/categories/reducer"
import { IApplicationState } from "./IApplicationState"
import { bascetReducer } from "../containers/bascet/reducer"
import { userReducer } from "../containers/user/reducer"
import { history } from "./configureStore"
import { connectRouter } from "connected-react-router"


export const rootReducer: Reducer<IApplicationState> = combineReducers<IApplicationState>({
    productGridState: productGridReducer,
    sliderState: sliderReducer,
    categoryState: categoriesReducer,
    bascetState: bascetReducer,
    userState: userReducer,
    router: connectRouter(history)
});
