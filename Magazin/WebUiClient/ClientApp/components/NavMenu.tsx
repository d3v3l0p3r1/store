import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';


export class NavMenu extends React.Component<{}, {}> {    

    public render() {
        return <nav className="navbar navbar-expand-lg main-nav navbar-dark justify-content-center">
            <div className="d-lg-none">
                <Link className="navbar-brand " to={"/"}>
                    <img src="../images/logo.png" />
                </Link>
            </div>
            <button id="toggler" type="button" className="navbar-toggler navbar-toggler-right" data-toggle="collapse" data-target="#main-navbar" aria-expanded="false" aria-controls="navbar">
                <span className="navbar-toggler-icon"></span>
            </button>

            <div id="main-navbar" className="navbar-collapse collapse w-100">

                <ul className='navbar-nav w-100 justify-content-center'>
                    <li className="nav-item">
                        <Link to={'/About'} className={"nav-link"}>
                            <span className="">О нас</span>
                        </Link>
                    </li>
                    <li className="nav-item">
                        <Link to={'/Product'} className={"nav-link"}>
                            <span >Products</span>
                        </Link>
                    </li>
                    <li className="nav-item">
                        <Link to={'/Delivery'} className={"nav-link"}>
                            <span>Доставка и оплата</span>
                        </Link>
                    </li>
                    <li className="nav-item">
                        <Link to={'/Promo'} className={"nav-link"}>
                            <span>Акции</span>
                        </Link>
                    </li>

                    <li className="nav-item">
                        <Link to={'/Feedback'} className={"nav-link"}>
                            <span>Отзывы</span>
                        </Link>
                    </li>

                    <li className="nav-item">
                        <Link to={'/Contacts'} className={"nav-link"}>
                            <span>Контакты</span>
                        </Link>
                    </li>

                    <li className="nav-item">
                        <Link to={'/News'} className={"nav-link"}>
                            <span>Статьи | Новости</span>
                        </Link>
                    </li>

                    <li className="nav-item">
                        <Link to={'/Login'} className={"nav-link"}>
                            <span>Вход | Регистрация</span>
                        </Link>
                    </li>

                </ul>

            </div>
        </nav>;


    }
}
