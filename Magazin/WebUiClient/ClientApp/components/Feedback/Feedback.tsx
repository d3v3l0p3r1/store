import * as React from "react"
import { RouteComponentProps } from 'react-router';

export class FeedbackComponent extends React.Component<RouteComponentProps<{}>, {}> {

    public render() {
        return <div>
            <h1>Feedback</h1>
        </div>;
    }
}