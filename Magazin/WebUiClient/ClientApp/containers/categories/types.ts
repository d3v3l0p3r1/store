import { Category } from "../../models/Category"
import { Action } from "redux"
import { ApiActionKeys } from "../../stores/ApiActionKeys"

export interface IReadCategoryAction extends Action {
    readonly type: ApiActionKeys.Category_Read,
    readonly payload: ReadonlyArray<Category>,
}

export interface IFetchingAction extends Action {
    readonly type: ApiActionKeys.Category_Fetching,
    readonly payload: boolean,
}
export interface IErrorAction extends Action {
    readonly type: ApiActionKeys.Category_Error,
    readonly payload: string,
}

export interface IChangeCategoryAction extends Action {
    readonly type: ApiActionKeys.ChangeProductCategory,
    readonly payload: number,    
}




export type CategoriesActions = IFetchingAction | IErrorAction | IReadCategoryAction | IChangeCategoryAction;