import { IUserState } from "./UserState"
import { Reducer } from "redux"
import { ApiActionKeys } from "../../stores/ApiActionKeys"

export const initialState: IUserState = {
    isLoginFormOpen: false,
    isRegisterFormOpen: false,
    user: null,
    isFetching: false,
    error: "",
}

export const userReducer: Reducer<IUserState> = (state: IUserState = initialState, action) => {
    let retState: IUserState = state;

    switch (action.type) {
        case ApiActionKeys.Login_Fetching:
            {
                retState = {
                    ...state,
                    isLoginFormOpen: true,
                    user: null,
                    isFetching: true,
                    error: ""
                };
                break;
            }
        case ApiActionKeys.Login_Error:
            {
                retState = {
                    ...state,
                    isFetching: false,
                    isLoginFormOpen: true,
                    user: null,
                    error: action.payload
                };
                break;
            }
        case ApiActionKeys.Login_Success:
            {
                retState = {
                    ...state,
                    isFetching: false,
                    isLoginFormOpen: false,
                    user: action.payload,
                    error: "",
                    isRegisterFormOpen: false
                };
                break;
            }

        case ApiActionKeys.Login_Open_Form:
            {
                retState = {
                    ...state,
                    isFetching: false,
                    isLoginFormOpen: action.payload,
                    user: null,
                    error: "",
                    isRegisterFormOpen: false
                };
                break;
            }
        case ApiActionKeys.Login_Register_Form:
            {
                retState = {
                    ...state,
                    isFetching: false,
                    isLoginFormOpen: false,
                    user: null,
                    error: "",
                    isRegisterFormOpen: action.payload
                };
                break;
            }
        case ApiActionKeys.Register_Fetching:
            {
                retState = {
                    ...state,
                    isFetching: true,
                    error: "",
                }
                break;
            }
        case ApiActionKeys.Register_Error:
            {
                retState = {
                    ...state,
                    error: action.payload,
                    isFetching: false
                };
                break;
            }
        case ApiActionKeys.Register_Complete:
            {
                retState = {
                    ...state,
                    user: action.payload,
                    isFetching: false,
                    isRegisterFormOpen: false,
                    isLoginFormOpen: false,
                    error: ""
                }
                break;
            }


        default:
    }


    return retState;
}