﻿import { ActionCreator, Dispatch, Action } from "redux";
import { ThunkAction } from "redux-thunk";
import { ProductGridActionKeys, IProductGridState, } from "./types";
import { readProducts } from "../api"

export const readData : ActionCreator<ThunkAction<Action, IProductGridState, void>> = (category: number) => {    
        return (dispatch: Dispatch<IProductGridState>) => {            
            const result = readProducts(category);

            result.then((response) => {
                debugger;
                return dispatch({
                    type: ProductGridActionKeys.ReadProducts,
                    payload: response
                });
            });

            result.catch((error) => {
                debugger;
                return dispatch({
                    type: ProductGridActionKeys.ReadProducts,
                    payload: error
                });
            });

            return dispatch({ type: ProductGridActionKeys.Fetching, payload: true });
        };
    };





export const gridActions = {
    readData: readData
}


