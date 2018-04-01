import * as React from "react";
import Slider from "react-slick";
import { withRouter } from 'react-router';
import { connect } from "react-redux"
import { IApplicationState } from "../stores/index"
import { News } from "../models/News"
import { Dispatch, bindActionCreators, Action, ActionCreator } from "redux"
import { ISliderState } from "./reducer"
import { readData } from "./actions"
import { ThunkAction } from "redux-thunk"


export interface ISliderProps {
    news: ReadonlyArray<News>;
    readData: Function;
}


class NewsSlider extends React.Component<ISliderProps> {
    constructor(props: ISliderProps) {        
        super(props);
    }

    public componentWillMount() {        
        this.props.readData();
    }

    public render() {

        const settings = {
            dots: true,
            infinite: true,
            speed: 500,
            slidesToShow: 1,
            slidesToScroll: 1
        };

        console.log(this.props.news);

        const list = this.props.news.map(p => {
            return <div key={p.id.toString()}>
                <img src={p.image} width="64" height="64" />
                <span>{p.title}</span>
            </div>;
        });

        return <div>
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

function mapDispatchToProps(dispatch: Dispatch<ISliderState>) {    
    return {
        readData: bindActionCreators(readData, dispatch)
    };
}

export default (connect(mapStateToProps, mapDispatchToProps)(NewsSlider));



