import { IBaseComponentState } from "../../stores/IBaseComponentState"
import { Category } from "../../models/Category"

export interface ICategoriesState extends IBaseComponentState {
    readonly categories: ReadonlyArray<Category>;
}