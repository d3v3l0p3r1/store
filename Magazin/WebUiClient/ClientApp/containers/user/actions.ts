import { Action, ActionCreator, Dispatch } from "redux"
import { ApiActionKeys } from "../../stores/ApiActionKeys"
import { UserModel } from "../../models/UserModel"
import { ThunkAction } from "redux-thunk";
import { IUserState } from "./UserState"
import {loginUser, registerUser } from "../../api"

export interface IFetchingAction extends Action {
    readonly type: ApiActionKeys.Login_Fetching,
    readonly payload: boolean,
}

export interface IErrorAction extends Action {
    readonly type: ApiActionKeys.Login_Error,
    readonly payload: string,
}

export interface ILoginSuccessAction extends Action {
    readonly type: ApiActionKeys.Login_Success,
    readonly payload: UserModel,
}

export interface IOpenLoginFormAction extends Action {
    readonly type: ApiActionKeys.Login_Open_Form,
    readonly payload: boolean,
}

export interface IOpenRegisterAction extends Action {
    readonly type: ApiActionKeys.Login_Register_Form,
    readonly payload: boolean,
}



export const LoginAction: ActionCreator<ThunkAction<Action, IUserState, void>> = (email: string, password: string) => {

    return (dispatch: Dispatch<IUserState>) => {

        const result = loginUser(email, password);

        result.then((response) => {
            dispatch({
                type: ApiActionKeys.Login_Success,
                payload: response
            });
        });

        result.catch((error) => {
            dispatch({
                type: ApiActionKeys.Login_Error,
                payload: error,
            });
        });


        return dispatch({
            type: ApiActionKeys.Login_Fetching,
            payload: true
        });
    }
}

export const OpenLoginFormAction: ActionCreator<IOpenLoginFormAction> = (isOpen : boolean) => {
    return {
        type: ApiActionKeys.Login_Open_Form,
        payload: isOpen
    }
}

export const OpenRegisterFormAction: ActionCreator<IOpenRegisterAction> = (isOpen: boolean) => {
    return {
        type: ApiActionKeys.Login_Register_Form,
        payload: isOpen
    }
}


export const RegisterAction: ActionCreator<ThunkAction<Action, IUserState, void>> = (email: string, name: string,password: string, passwordConfirm: string) => {
    return (dispatch: Dispatch<IUserState>) => {

        const result = registerUser(name, email, password, passwordConfirm);

        result.then((response) => {            
            dispatch({
                type: ApiActionKeys.Register_Complete,
                payload: response
            });
        });

        result.catch((error) => {            
            dispatch({
                type: ApiActionKeys.Register_Error,
                payload: error
            });
        });

        return dispatch({
            type: ApiActionKeys.Register_Fetching,
            payload: true
        });

    }
}