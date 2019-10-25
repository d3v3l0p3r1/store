import { Action } from "redux"
import { ApiActionKeys } from "../../stores/ApiActionKeys";
import UserModel from "../../models/UserModel"
import { type } from "os";


export interface ILoginFetchingAction extends Action {
    readonly type: ApiActionKeys.Login_Fetching,
}

export interface ILoginSuccessAction extends Action {
    readonly type: ApiActionKeys.Login_Success,
    readonly payload: UserModel
}

export interface ILoginErrrorAction extends Action {
    readonly type: ApiActionKeys.Login_Error,
    readonly payload: string
}

export type LoginActions = ILoginFetchingAction | ILoginSuccessAction | ILoginErrrorAction


export interface IRegisterFetchingAction extends Action {
    readonly type: ApiActionKeys.Register_Fetching,
}

export interface IRegisterCompleteAction extends Action {
    readonly type: ApiActionKeys.Register_Complete,
    readonly payload: UserModel
}

export interface IRegisterErrorAction extends Action {
    readonly type: ApiActionKeys.Register_Error,
    readonly payload: string
}

export type RegisterActions = IRegisterCompleteAction | IRegisterErrorAction | IRegisterFetchingAction

