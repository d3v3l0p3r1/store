import * as React from "react"
import { Action, ActionCreator, bindActionCreators, Dispatch, AnyAction } from "redux";
import { ThunkAction } from "redux-thunk"
import { connect } from "react-redux"
import { IApplicationState } from "../../stores/IApplicationState";
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';
import { IUserState } from "./UserState"
import { LoginAction, IOpenLoginFormAction, IOpenRegisterAction, OpenLoginFormAction, OpenRegisterFormAction } from "./actions"
import { LoginActions } from "./types"
import UserModel from "../../models/UserModel"
import "./Styles/user.css"

export interface ILoginContainerProps {
    isOpen: boolean,
    error: string,
    fetching: boolean,
    closeModal: Function,
    loginAction: ActionCreator<ThunkAction<Promise<LoginActions>, UserModel, null, LoginActions>>,
    openLoginForm: ActionCreator<IOpenLoginFormAction>,
    openRegisterForm: ActionCreator<IOpenRegisterAction>,
}

export interface ILoginState {
    email: string,
    password: string,
}

class LoginContainer extends React.Component<ILoginContainerProps, ILoginState> {

    constructor(props: ILoginContainerProps) {
        super(props);

        this.state = {
            email: "",
            password: ""
        };
    }

    onEmailChange = (e: any) => {        
        var email = e.target.value;
        this.setState({ email: email });
    }

    onPasswordChange = (e: any) => {        
        var pass = e.target.value;
        this.setState({ password: pass });
    }

    Login = () => {        
        this.props.loginAction(this.state.email, this.state.password);
    }

    public render() {
        var button = this.props.fetching
            ? (<button className="btn btn-danger btn-block" disabled={this.props.fetching}><span className="fa fa-circle-o-notch fa-spin"></span></button>)
            : (<button className="btn btn-danger btn-block" disabled={this.props.fetching} onClick={() => { this.Login(); }}>Войти</button>);

        var error = this.props.error
            ? <div className="alert alert-danger" role="alert"> {this.props.error} </div>
            : <div></div>;

        return <Modal isOpen={this.props.isOpen} className="modal-dialog-centered modal-sm">
            <ModalHeader toggle={() => this.props.openLoginForm(false)}>
                Авторизация
            </ModalHeader>

            <ModalBody>
                <div>
                    <div className="form-group">
                        <input disabled={this.props.fetching}
                            type="email"
                            className="form-control"
                            placeholder="Электронная почта"
                            value={this.state.email}
                            onChange={this.onEmailChange} />
                    </div>

                    <div className="form-group">
                        <input disabled={this.props.fetching}
                            type="password"
                            className="form-control"
                            placeholder="Пароль"
                            value={this.state.password}
                            onChange={this.onPasswordChange} />
                    </div>
                    {error}
                    {button}
                </div>
            </ModalBody>

            <ModalFooter>
                <p>Нет аккаунта? </p>
                <p className="control-text" onClick={() => {this.props.openRegisterForm(true)}}>Зарегистрироваться</p>
            </ModalFooter>

        </Modal>;
    }
}


function mapStateToProps(state: IApplicationState, ownProps: any) {
    return {
        isOpen: state.userState.isLoginFormOpen,       
        error: state.userState.error,
        fetching: state.userState.isFetching
    }
}

function mapDispatchToProps(dispatch: Dispatch<AnyAction>) {
    return {
        loginAction: bindActionCreators(LoginAction, dispatch),
        openLoginForm: bindActionCreators(OpenLoginFormAction, dispatch),
        openRegisterForm: bindActionCreators(OpenRegisterFormAction, dispatch)
    }
}


export default connect(mapStateToProps, mapDispatchToProps)(LoginContainer);