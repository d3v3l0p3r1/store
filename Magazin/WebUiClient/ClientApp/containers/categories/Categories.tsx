import * as React from "react"
import { connect } from "react-redux"
import { Dispatch, bindActionCreators, Action, ActionCreator } from "redux"
import { ThunkAction } from "redux-thunk"
import { Category } from "../../models/Category"
import { ICategoriesState } from "./ICategoriesState"
import { IApplicationState } from "../../stores/IApplicationState"
import { readData } from "./actions"
import { Link, NavLink } from 'react-router-dom';

export interface ICategoriesProps {
    categories: ReadonlyArray<Category>;
    readData: ActionCreator<ThunkAction<Action, ICategoriesState, void>>;
}

export class Categories extends React.Component<ICategoriesProps> {

    public componentWillMount() {
        this.props.readData();
    }

    public render() {        
        const list = this.props.categories.map(c => {
            return <li key={c.id} className="nav-item">
                <Link to={'/Product/' + c.id} className="nav-link">
                    <span>{c.title}</span>
                </Link>
            </li>;
        });

        return <ul className="navbar-nav w-100 justify-content-center">
            {list}
        </ul>;
    }
}



function mapStateToProps(storeState: IApplicationState) {
    return {
        categories: storeState.categoryState.categories
    }
}

function mapDispatchToProps(dispatch: Dispatch<ICategoriesState>) {
    return {
        readData: bindActionCreators(readData, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(Categories);