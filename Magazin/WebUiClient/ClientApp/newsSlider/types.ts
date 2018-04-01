import { News } from "../models/News"
import { Action } from "redux"

export enum SliderActionKeys {
    Read = "READ",
    Fetching = "FETCHING",
    Error = "ERROR"
}

export interface IFetchingAction extends Action {
    readonly type: SliderActionKeys.Fetching,
    readonly payload: boolean,
}

export interface IErrorAction extends Action {
    readonly type: SliderActionKeys.Error,
    readonly payload: string,    
}

export interface IReadNewsAction extends Action {
    readonly type: SliderActionKeys.Read,
    readonly payload: ReadonlyArray<News>,
}

export type SliderActions = IFetchingAction | IErrorAction | IReadNewsAction;