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

configuredStore.subscribe(() => {    
    var state = configuredStore.getState();
    localStorage.setItem("bascetState", JSON.stringify(state.bascetState));    
    

    if (state.userState.user != null) {
        localStorage.setItem("user", JSON.stringify(state.userState.user));
    }
    
});

