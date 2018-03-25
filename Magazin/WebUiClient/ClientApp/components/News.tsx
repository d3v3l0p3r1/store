import * as React from "react"
import { RouteComponentProps } from 'react-router';

export class NewsComponent extends React.Component<RouteComponentProps<{}>, {}> {

    public render() {
        return <div><h1>News</h1></div>;
    }
}