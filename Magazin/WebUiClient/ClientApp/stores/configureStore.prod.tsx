import { applyMiddleware, createStore } from "redux"
import thunkMiddleware from "redux-thunk";
import { IApplicationState, rootReducer } from "./index"
import { routerMiddleware } from "react-router-redux"
import { history } from "./configureStore"

export default function configureStore() {
    const routeMiddleware = routerMiddleware(history);

    return createStore<IApplicationState>(
        rootReducer,
        applyMiddleware(thunkMiddleware, routeMiddleware)
    );
}