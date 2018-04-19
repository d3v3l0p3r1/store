import { Reducer } from "redux"
import { ApiActionKeys } from "../../stores/ApiActionKeys"
import { CategoriesActions, IReadCategoryAction , IChangeCategoryAction} from "./types"
import {ICategoriesState} from "./ICategoriesState"

export const initialState: ICategoriesState = {
    errorMessage: "",
    isBusy: false,
    categories: []
}


export const categoriesReducer: Reducer<ICategoriesState> = (state: ICategoriesState = initialState, action) => {

    let retState: ICategoriesState = state;
    
    switch ((action as CategoriesActions).type) {
   
        case ApiActionKeys.Category_Fetching:
            {
                retState = { ...state, categories: [], errorMessage: "", isBusy: true };                
                break;
            }

        case ApiActionKeys.Category_Error:
            {                
                retState = { ...state, categories: [], errorMessage: action.payload };
                break;
            }
        case ApiActionKeys.Category_Read:
            {
                var payload = (action as IReadCategoryAction).payload;
                retState = { ...state, categories: payload, errorMessage: "" }
                break;
            }               
    }

    return retState;
}