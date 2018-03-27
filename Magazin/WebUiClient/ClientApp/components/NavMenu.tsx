import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';


export class NavMenu extends React.Component<{}, {}> {
    constructor() {
        super({});
    }


    public render() {
        return <nav className="navbar navbar-expand-lg main-nav navbar-dark">
            <div className="d-lg-none">
                <Link className="navbar-brand " to={"/"}>
                    <span className="navbar-brand">AAAAAAAAAAA</span>
                </Link>
            </div>
            <button id="toggler" type="button" className="navbar-toggler navbar-toggler-right" data-toggle="collapse" data-target="#main-navbar" aria-expanded="false" aria-controls="navbar">
                <span className="navbar-toggler-icon"></span>
            </button>

            <div id="main-navbar" className="navbar-collapse collapse">

                <ul className='nav navbar-nav'>
                    <li>
                        <Link to={'/About'}>
                            <span className="nav-link">О нас</span>
                        </Link>
                    </li>
                    <li>
                        <Link to={'/Delivery'}>
                            <span className="nav-link">Доставка и оплата</span>
                        </Link>
                    </li>
                    <li>
                        <Link to={'/Promo'}>
                            <span className="nav-link">Акции</span>
                        </Link>
                    </li>

                    <li>
                        <Link to={'/Feedback'}>
                            <span className="nav-link">Отзывы</span>
                        </Link>
                    </li>

                    <li>
                        <Link to={'/Contacts'}>
                            <span className="nav-link">Контакты</span>
                        </Link>
                    </li>

                    <li>
                        <Link to={'/News'}>
                            <span className="nav-link">Статьи|Новости</span>
                        </Link>
                    </li>

                    <li>
                        <Link to={'/Login'}>
                            <span className="nav-link">Вход|Регистрация</span>
                        </Link>
                    </li>

                </ul>

            </div>
        </nav>;


    }
}
