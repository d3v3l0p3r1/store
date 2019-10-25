import * as React from "react";
import Slider from "react-slick";
import { connect } from "react-redux"
import { IApplicationState } from "../../stores/IApplicationState"
import News from "../../models/News"
import { Dispatch, bindActionCreators, Action, ActionCreator, AnyAction } from "redux"
import { ISliderState } from "./SliderState"
import { readNewsAction } from "./actions"
import { ThunkAction } from "redux-thunk"
import { SliderActions } from "./types"
 
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";


export interface ISliderProps {
    news: ReadonlyArray<News>;
    readNewsAction: ActionCreator<ThunkAction<Promise<SliderActions>, News[], null, SliderActions>>;
}


class NewsSlider extends React.Component<ISliderProps> {
    constructor(props: ISliderProps) {
        super(props);
    }

    public componentWillMount() {
        this.props.readNewsAction();
    }

    public render() {

        const settings = {
            dots: true,
            infinite: true,
            speed: 500,
            slidesToShow: 1,
            slidesToScroll: 1        
        };        

        const list = this.props.news.map(p => {
            return <div key={p.id.toString()}>
                <img src={p.image} className="w-100"  height="340px;"/>                
            </div>;
        });

        return <div className="slider-wrapper">
            <Slider { ...settings}>
                {list}
            </Slider>
        </div>;
    }

}


function mapStateToProps(storeState: IApplicationState) {
    return {
        news: storeState.sliderState.newsArray
    }
}

function mapDispatchToProps(dispatch: Dispatch<AnyAction>) {
    return {
        readNewsAction: bindActionCreators(readNewsAction, dispatch)
    };
}

export default (connect(mapStateToProps, mapDispatchToProps)(NewsSlider));



