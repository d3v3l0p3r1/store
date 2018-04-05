import * as React from "react"

export class FastOrderComponent extends React.Component {

    public render() {
        return <div>

            <input type="number" className="text-input w-50" />

            <button className="btn btn-danger w-50 mb-1">
                <span>Заказать в один клик</span>
            </button>

        </div>;
    }

}