import { Settings } from "./Settings"
import { News } from "./models/News"
import { Product } from "./models/Product"
import { Category } from "./models/Category"
import { IBascetState } from "./containers/bascet/BascetState"
import { IOrderModel, Order } from "./models/OrderModel"
import { UserModel } from './models/UserModel';

const apiRoot = "http://localhost:51145";

var token = localStorage.getItem("jwt");

function headers() {

    var t = {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
        'Authorization': token ? "bearer " + token : ""
    };

    return t;
}


function call(endpoint: string, body: any) {
    const fullUrl = apiRoot + endpoint;

    return fetch(fullUrl, body);
}



export function readProducts(category: number): Promise<ReadonlyArray<Product>> {
    var url = Settings.ProductURL + "?cat=" + category;


    var ret = new Promise<ReadonlyArray<Product>>((resolve, reject) => {
        call(url, null)
            .then((response) => {
                return response.json();
            }).then((json) => {
                const arr = json.map((x: any) => {
                    let product = new Product();

                    product.id = x.Id;
                    product.description = x.Description;
                    product.img = x.File;
                    product.price = x.Price;
                    product.title = x.Title;
                    product.kindId = x.KindID;
                    product.kindName = x.KindTitle;

                    return product;
                });
                resolve(arr);
            })
            .catch((error) => {
                reject(error);
            });

    });

    return ret;
}

export function readNews(): Promise<ReadonlyArray<News>> {

    var ret = new Promise<ReadonlyArray<News>>((resolve, reject) => {
        call(Settings.NewsUrl, null)
            .then((response) => {
                return response.json();
            })
            .then((json) => {
                const arr = json.map((x: any) => {
                    let news = new News();

                    news.id = x.Id;
                    news.image = x.Image;
                    news.title = x.Title;

                    return news;
                });

                resolve(arr);
            })
            .catch((error) => {
                reject(error);
            });
    });

    return ret;

}

export function readCategories(): Promise<ReadonlyArray<Category>> {

    var ret = new Promise<ReadonlyArray<Category>>((resolve, reject) => {
        call(Settings.CatsUrl, null)
            .then((response) => {
                return response.json();
            })
            .then((json) => {
                const arr = json.map((x: any) => {
                    let cat = new Category();

                    cat.id = x.Id;
                    cat.title = x.Title;

                    return cat;
                });

                resolve(arr);
            })
            .catch((error) => {
                reject(error);
            });
    });

    return ret;
}

export function registerUser(name: string, email: string, password: string, passwordConfirm: string, address: string, phone: string) {

    var ret = new Promise<UserModel>((resolve, reject) => {
        call(Settings.RegisterUrl,
            {
                method: "POST",
                headers: headers(),
                body: JSON.stringify({
                    name: name,
                    email: email,
                    password: password,
                    passwordConfirm: passwordConfirm,
                    address: address,
                    phone: phone
                })
            })
            .then((response) => {
                return response.json();
            })
            .then((json) => {
                resolve(json);
            })
            // after error
            .catch((error) => {
                return error.json();
            })
            .then((error) => {
                reject(error);
            });
    });

    return ret;
}

export function loginUser(email: string, password: string) {
    var ret = new Promise<UserModel>((resolve, reject) => {

        var authHeaders = headers();

        call(Settings.LoginUrl,
            {
                method: "POST",
                headers: authHeaders,
                body: JSON.stringify({
                    email: email,
                    password: password
                }),
            })
            .then(response => {
                return response.json();
            })
            .then(json => {
                var user = JSON.parse(json) as UserModel;
                return resolve(user);
            })
            .catch((error) => {
                return error.json();
            })
            .then((error) => {
                reject(error);
            });

    });

    return ret;
}



export function order(model: IOrderModel) {

    var ret = new Promise<Order>((resolve, reject) => {
        fetch(apiRoot + Settings.OrderUrl,
            {
                mode: "cors",
                method: "POST",
                headers: headers(),
                body: JSON.stringify(model)
            })
            .then((response) => {
                return response.json();
            })
            .then((json) => {
                resolve(json);
            })
            .catch((error) => {
                reject(error);
            });
    });
    return ret;
}