import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';

export class NavMenu extends React.Component<{}, {}> {
    public render() {
        return <nav className='navbar navbar-inverse bg-inverse'>
            <div className='collapse navbar-collapse' id="navbarNav">
                <ul className='navbar-nav mr-auto"'>
                    <li className="nav-item active">
                        <Link to={'/About'}>
                            <span className="nav-link">О нас</span>
                        </Link>
                    </li>
                    <li>
                        <Link to={'/Delivery'}>
                            <span className="nav-link">Доставка и оплата</span>
                        </Link>
                    </li>
                </ul>
            </div>
        </nav>;
    }
}
