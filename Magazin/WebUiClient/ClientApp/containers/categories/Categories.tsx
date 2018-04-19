import * as React from "react"
import { connect } from "react-redux"
import { Dispatch, bindActionCreators, Action, ActionCreator } from "redux"
import { ThunkAction } from "redux-thunk"
import { Category } from "../../models/Category"
import { ICategoriesState } from "./ICategoriesState"
import { IApplicationState } from "../../stores/IApplicationState"
import { readData } from "./actions"
import { Link, NavLink } from 'react-router-dom';
import { IChangeCategoryAction } from "./types"
import {changeCategory} from "./actions"

export interface ICategoriesProps {
    categories: ReadonlyArray<Category>;
    readData: ActionCreator<ThunkAction<Action, ICategoriesState, void>>;
    changeCategory: ActionCreator<IChangeCategoryAction>,
}

export class Categories extends React.Component<ICategoriesProps> {

    public componentWillMount() {
        this.props.readData();
    }

    public componentDidUpdate() {        
        if (this.props.categories.length > 0) {
            this.props.changeCategory(this.props.categories[0].id);
        }
    }   

    public render() {
        const list = this.props.categories.map(c => {
            return <li key={c.id} className="nav-item">
                <button onClick={()=> this.props.changeCategory(c.id)} className="nav-link">
                    <span>{c.title}</span>
                </button>
            </li>;
        });

        return <nav className="navbar navbar-expand-lg main-nav navbar-dark justify-content-center">
                   <div className="d-lg-none">
                       <Link className="navbar-brand " to={"/"}>                           
                       </Link>
                   </div>
                   <button id="toggler" type="button" className="navbar-toggler navbar-toggler-right" data-toggle="collapse" data-target="#category-navbar" aria-expanded="false" aria-controls="navbar">
                       <span className="navbar-toggler-icon"></span>
                   </button>
                   <div id="category-navbar" className="navbar-collapse collapse w-100">

                       <ul className='navbar-nav w-100 justify-content-center'>
                           {list}

                       </ul>

                   </div>
               </nav>;
    }
}



function mapStateToProps(storeState: IApplicationState) {
    return {
        categories: storeState.categoryState.categories
    }
}

function mapDispatchToProps(dispatch: Dispatch<ICategoriesState>) {
    return {
        readData: bindActionCreators(readData, dispatch),
        changeCategory: bindActionCreators(changeCategory, dispatch),
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(Categories);