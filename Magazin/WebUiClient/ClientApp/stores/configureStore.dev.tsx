import { applyMiddleware, createStore } from "redux"
import thunkMiddleware from "redux-thunk";
import { routerMiddleware } from "react-router-redux"
import { rootReducer } from "./index"
import { IApplicationState } from "./IApplicationState"
import { history } from "./configureStore"



export default function configureStore() {
    const routeMiddleware = routerMiddleware(history);

    const stateString = localStorage.getItem("state");

    const initialState: IApplicationState = stateString ? JSON.parse(stateString) as IApplicationState : {} as IApplicationState;    

    return createStore<IApplicationState>(
        rootReducer,
        initialState,
        applyMiddleware(thunkMiddleware, routeMiddleware)
    );
}