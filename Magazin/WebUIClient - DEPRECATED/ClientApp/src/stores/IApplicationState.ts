import { IProductGridState } from "../containers/product/types"
import { ISliderState } from "../containers/newsSlider/SliderState"
import { IBascetState } from "../containers/bascet/BascetState"
import { ICategoriesState } from "../containers/categories/ICategoriesState"
import { IUserState } from "../containers/user/UserState"
import { RouterState } from "connected-react-router"

export interface IApplicationState {
    readonly productGridState: IProductGridState;
    readonly sliderState: ISliderState; 
    readonly categoryState: ICategoriesState;
    readonly bascetState: IBascetState;
    readonly userState: IUserState;
    readonly router: RouterState
}
