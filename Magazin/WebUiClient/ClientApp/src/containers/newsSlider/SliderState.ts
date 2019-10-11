import { News } from "../../models/News"
import { IBaseComponentState } from "../../stores/IBaseComponentState"

export interface ISliderState extends IBaseComponentState {
    readonly newsArray: ReadonlyArray<News>;

}