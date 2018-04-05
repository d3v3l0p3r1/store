import * as React from 'react';


export class SearchBox extends React.Component {


    public render() {

        return <div>
            <input type="text" className="text-input w-75" />            
            <button className="btn btn-danger mb-1">
                <span className="fa fa-search"></span>
            </button>

        </div>;
    }
}