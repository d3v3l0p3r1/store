import { combineReducers, Dispatch, Reducer } from 'redux';
import { routerReducer, RouterState } from "react-router-redux";
import { IProductGridState } from "../product/types"
import { productGridReducer } from "../product/reducer"

export interface IApplicationState {
    readonly productGridState: IProductGridState;
    readonly routerState: RouterState;
    readonly isAuthenticated: boolean;    
    readonly location: string;
}


export const rootReducer: Reducer<IApplicationState> = combineReducers<IApplicationState>({
    router: routerReducer,
    productGridState: productGridReducer
});
