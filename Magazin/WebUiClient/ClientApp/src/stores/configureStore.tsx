import configureStoreDev from "./configureStore.dev";
import configureStoreProd from "./configureStore.prod";
import { createBrowserHistory } from "history"

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href') as string;
export const history = createBrowserHistory({ basename: baseUrl });

const configure =
    process.env.NODE_ENV === "production"
        ? configureStoreProd
        : configureStoreDev;

export default configure;
