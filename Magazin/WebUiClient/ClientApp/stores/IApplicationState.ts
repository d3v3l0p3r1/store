import { RouterState } from "react-router-redux";
import { IProductGridState } from "../containers/product/types"
import { ISliderState } from "../containers/newsSlider/reducer"
import {IBascetState} from "../containers/bascet/Bascet"
import { ICategoriesState } from "../containers/categories/ICategoriesState"


export interface IApplicationState {
    readonly productGridState: IProductGridState;
    readonly sliderState: ISliderState;
    readonly routerState: RouterState;
    readonly categoryState: ICategoriesState;
    readonly bascetState: IBascetState;        
}
