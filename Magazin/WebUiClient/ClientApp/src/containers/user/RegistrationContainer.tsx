import * as React from "react"
import { Action, ActionCreator, bindActionCreators, Dispatch, AnyAction } from "redux";
import { ThunkAction } from "redux-thunk"
import { connect } from "react-redux"
import { IApplicationState } from "../../stores/IApplicationState";
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';
import { RegisterActions } from "./types"
import { IOpenLoginFormAction, IOpenRegisterAction, OpenLoginFormAction, OpenRegisterFormAction, RegisterAction } from "./actions"
import UserModel from "../../models/UserModel"
import "./Styles/user.css"

export interface IRegisterContainerProps {
    isOpen: boolean,
    closeModal: Function,
    fetching: boolean,
    error: string,
    registerAction: ActionCreator<ThunkAction<Promise<RegisterActions>, UserModel, null, RegisterActions>>,
    openLoginForm: ActionCreator<IOpenLoginFormAction>,
    openRegisterForm: ActionCreator<IOpenRegisterAction>,
}

interface IRegisterFormState {
    email: string,
    password: string,
    passwordConfirm: string,
    name: string,
    address: string,
    phone: string,
}

class RegisterContainer extends React.Component<IRegisterContainerProps, IRegisterFormState> {
    constructor(props: IRegisterContainerProps) {
        super(props);

        this.state = {
            email: "",
            password: "",
            passwordConfirm: "",
            address: "",
            phone: "",
            name: ""
        };
    }

    onNameChange = (e: any) => {
        var name = e.target.value;
        this.setState({ name: name });
    }

    onEmailChange = (e: any) => {
        var email = e.target.value;
        this.setState({ email: email });
    }

    onPasswordChange = (e: any) => {
        var pass = e.target.value;
        this.setState({ password: pass });
    }

    onPasswordConfirmChange = (e: any) => {
        var pass = e.target.value;
        this.setState({ passwordConfirm: pass });
    }

    onAddressChange = (e: any) => {
        var address = e.target.value;
        this.setState({address: address});
    }

    onPhoneChange = (e: any) => {
        var phone = e.target.value;
        this.setState({phone: phone});
    }

    register = () => {
        this.props.registerAction(this.state.email, this.state.name, this.state.password, this.state.passwordConfirm, this.state.address, this.state.phone);
    }

    public render() {
        
        var button = this.props.fetching
            ? (<button className="btn btn-danger btn-block" disabled={this.props.fetching}><span className="fa fa-circle-o-notch fa-spin"></span></button>)            
            : (<button className="btn btn-danger btn-block" disabled={this.props.fetching} onClick={() => { this.register(); }}>Зарегистрироваться</button>);
        
        var error = this.props.error
            ? <div className="alert alert-danger" role="alert"> {this.props.error} </div>
            : <div></div>;

        return <Modal isOpen={this.props.isOpen} className="modal-dialog-centered">
            <ModalHeader toggle={() => this.props.openRegisterForm(false)}>
                Регистрация
                   </ModalHeader>

            <ModalBody>
                <div>
                    <div className="form-group">
                        <input disabled={this.props.fetching}
                            className="form-control"
                            placeholder="Имя"
                            value={this.state.name}
                            onChange={this.onNameChange} />
                    </div>
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
                               className="form-control"
                               placeholder="Адрес доставки"
                               value={this.state.address}
                               onChange={this.onAddressChange} />
                    </div>

                    <div className="form-group">
                        <input disabled={this.props.fetching}
                               type="phone"
                               className="form-control"
                               placeholder="Телефон"
                               value={this.state.phone}
                               onChange={this.onPhoneChange} />
                    </div>

                    <div className="form-group">
                        <input disabled={this.props.fetching}
                            type="password"
                            className="form-control"
                            placeholder="Пароль"
                            value={this.state.password}
                            onChange={this.onPasswordChange} />
                    </div>

                    <div className="form-group">
                        <input disabled={this.props.fetching}
                            type="password"
                            className="form-control"
                            placeholder="Подтверждение пароля"
                            value={this.state.passwordConfirm}
                            onChange={this.onPasswordConfirmChange} />
                    </div>
                    {error}
                    {button}

                </div>
            </ModalBody>

            <ModalFooter>
                <p>Есть аккаунт? </p>
                <p className="control-text" onClick={() => { this.props.openLoginForm(true) }
                }>Войти</p>
            </ModalFooter>

        </Modal>;
    }


}

function mapStateToProps(state: IApplicationState, ownProps: any) {    
    return {
        isOpen: state.userState.isRegisterFormOpen,
        fetching: state.userState.isFetching,
        error: state.userState.error
    }
}

function mapDispatchProps(dispatch: Dispatch<AnyAction>) {
    return {

        registerAction: bindActionCreators(RegisterAction, dispatch),
        openLoginForm: bindActionCreators(OpenLoginFormAction, dispatch),
        openRegisterForm: bindActionCreators(OpenRegisterFormAction, dispatch)
    }
}

export default connect(mapStateToProps, mapDispatchProps)(RegisterContainer);





