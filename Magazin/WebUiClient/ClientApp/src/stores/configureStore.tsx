import configureStoreDev from "./configureStore.dev";
import configureStoreProd from "./configureStore.prod";
import createHistory from 'history/createBrowserHistory'

export const history = createHistory();

const configure =
    process.env.NODE_ENV === "production"
        ? configureStoreProd
        : configureStoreDev;

export default configure;
