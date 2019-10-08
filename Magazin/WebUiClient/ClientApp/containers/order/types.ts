﻿import { Action } from "redux"
import { ApiActionKeys } from "../../stores/ApiActionKeys";
import { IOrderModel } from "../../models/OrderModel"


export interface IOrderFetchingAction extends Action {
    readonly type: ApiActionKeys.Order_Fetching
}

export interface IOrderCompleteAction extends Action {
    readonly type: ApiActionKeys.Order_Complete
}

export interface IOrderErrorAction extends Action {
    readonly type: ApiActionKeys.Order_Error,
    readonly payload: IOrderModel
}

export type OrderActions = IOrderFetchingAction | IOrderCompleteAction | IOrderErrorAction