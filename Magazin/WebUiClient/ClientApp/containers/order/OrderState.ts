

export interface IOrderState {
    readonly fetching: boolean,
    readonly error: string,
    readonly complete: boolean,
}

export const initialState: IOrderState = {
    complete: false,
    error: "",
    fetching: false
}