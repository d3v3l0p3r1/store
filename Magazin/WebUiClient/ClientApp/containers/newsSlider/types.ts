import { News } from "../../models/News"
import { Action } from "redux"
import { ApiActionKeys } from "../../stores/ApiActionKeys"


export interface IReadNewsAction extends Action {
    readonly type: ApiActionKeys.News_Read,
    readonly payload: ReadonlyArray<News>,
}


export interface IFetchingAction extends Action {
    readonly type: ApiActionKeys.News_Fetching,
    readonly payload: boolean,
}


export interface IErrorAction extends Action {
    readonly type: ApiActionKeys.News_Error,
    readonly payload: string,
}

export type SliderActions = IFetchingAction | IErrorAction | IReadNewsAction;