import { Action, ActionCreator, Dispatch } from "redux"
import { ApiActionKeys } from "../../stores/ApiActionKeys"
import { UserModel } from "../../models/UserModel"
import { ThunkAction } from "redux-thunk";
import { IUserState } from "./UserState"
import { loginUser, registerUser } from "../../api"
import { LoginActions, RegisterActions } from "./types"
import { async } from "q";


export interface IOpenLoginFormAction extends Action {
    readonly type: ApiActionKeys.Login_Open_Form,
    readonly payload: boolean,
}

export interface IOpenRegisterAction extends Action {
    readonly type: ApiActionKeys.Login_Register_Form,
    readonly payload: boolean,
}



export const LoginAction: ActionCreator<ThunkAction<Promise<LoginActions>, UserModel, null, LoginActions>> = (email: string, password: string) => {

    return async (dispatch: Dispatch<LoginActions>) => {

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


export const RegisterAction: ActionCreator<ThunkAction<Promise<RegisterActions>, UserModel, null, RegisterActions>> = (email: string, name: string, password: string, passwordConfirm: string, address: string, phone: string) => {
    return async (dispatch: Dispatch<RegisterActions>) => {

        const result = registerUser(name, email, password, passwordConfirm, address, phone);

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