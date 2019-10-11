import * as React from "react"
import {connect} from "react-redux"
import { IOpenLoginFormAction , OpenLoginFormAction} from "./actions"
import { IApplicationState } from "../../stores/IApplicationState"
import { UserModel } from "../../models/UserModel"
import { ActionCreator, bindActionCreators, Dispatch, AnyAction } from "redux"
import LoginContainer from "./LoginContainer"
import RegisterContainer from "./RegistrationContainer"

export interface IUSerNavProps {
    user: UserModel,
    openLoginForm: ActionCreator<IOpenLoginFormAction>,
}

class UserNavMenuItem extends React.Component<IUSerNavProps>{

    public render() {

        var content: any;

        if (this.props.user) {
            content = <button className="btn btn-outline-danger">Личный кабинет</button>;
        } else {
            content =
                <button className="btn btn-outline-danger" onClick={() => { this.props.openLoginForm(true) }}>Войти|Регистрация</button>;
        }

        return <div>
            <LoginContainer />       
            <RegisterContainer/>
            {content}
        </div>;
    }
}

function mapStateToProps(state: IApplicationState) {
    return {
        user: state.userState.user
    }
}

function mapDispatchToProps(dispatch: Dispatch<AnyAction>) {
    return {
        openLoginForm: bindActionCreators(OpenLoginFormAction, dispatch)
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(UserNavMenuItem);