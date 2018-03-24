import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';

export class NavMenu extends React.Component<{}, {}> {
    public render() {
        return <div className='main-nav'>
            <div className='navbar navbar-inverse'>
                <div className='navbar-collapse collapse'>
                    <ul className='nav navbar-nav'>
                        <li>
                            <Link to={'/About'}>
                                <span>О нас</span>
                            </Link>
                        </li>
                        <li>
                            <Link to={'/Delivery'}>
                                <span>Доставка и оплата</span>
                            </Link>
                        </li>                        
                    </ul>
                </div>
            </div>
        </div>;
    }
}
