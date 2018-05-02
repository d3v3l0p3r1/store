import {UserModel} from "../../models/UserModel"

export interface IUserState {
    readonly user: UserModel | null;
    readonly isLoginFormOpen: boolean;
    readonly isRegisterFormOpen: boolean;
    readonly isFetching: boolean;
    readonly error: string;    
}