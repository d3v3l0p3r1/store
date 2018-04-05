import './css/site.css';
import 'bootstrap';
import 'font-awesome/css/font-awesome.min.css';
import * as React from 'react';
import * as ReactDOM from 'react-dom';

import { ConnectedRouter } from "react-router-redux"
import { Provider } from "react-redux"
import configureStore, { history } from "./stores/configureStore"
import App from "./containers/App"

const configuredStore = configureStore();

function renderApp() {

    ReactDOM.render(
        <Provider store={configuredStore}>
            <ConnectedRouter history={history}>
                <App/>
            </ConnectedRouter>
        </Provider>,
        document.getElementById('react-app')
    );
}

renderApp();

if (module.hot) {
    module.hot.accept('./containers/App',
        () => {
            ReactDOM.render(
                <Provider store={configuredStore}>
                    <ConnectedRouter history={history}>
                        <App />
                    </ConnectedRouter>
                </Provider>,
                document.getElementById('react-app')
            );
        });
}

