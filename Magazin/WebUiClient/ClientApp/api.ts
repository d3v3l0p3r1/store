import { Promise } from 'es6-promise';
import { Settings } from "./Settings"
import { News } from "./models/News"
import { Product } from "./models/Product"
import { Category } from "./models/Category"
import { IBascetState } from "./containers/bascet/BascetState"
import { IUserState } from "./containers/user/UserState"

const apiRoot = "http://localhost:51145";

var headers = {
    'Accept': 'application/json',
    'Content-Type': 'application/json',
    'Authorization': 'Bearer ${localStorage.jwt}'
};

function call(endpoint: string) {
    const fullUrl = apiRoot + endpoint;

    return fetch(fullUrl);
}



export function readProducts(category: number): Promise<ReadonlyArray<Product>> {
    var url = Settings.ProductURL + "?cat=" + category;


    var ret = new Promise<ReadonlyArray<Product>>((resolve, reject) => {
        call(url)
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
        call(Settings.NewsUrl)
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
        call(Settings.CatsUrl)
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

    var ret = new Promise((resolve, reject) => {
        fetch(apiRoot + Settings.RegisterUrl,
            {
                method: "POST",
                headers: headers,
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
                if (response.ok) {
                    return Promise.resolve(response.json());
                }
                return Promise.reject(response);
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
    var ret = new Promise((resolve, reject) => {

        var authHeaders = headers;

        fetch(apiRoot + Settings.LoginUrl,
            {
                method: "POST",
                headers: authHeaders,
                body: JSON.stringify({
                    email: email,
                    password: password
                }),
            })
            .then(response => {
                if (response.ok) {
                    return response.json();
                }
                return Promise.reject(response);
            })
            .then(json => {
                return resolve(json);
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



export function order(bascet: IBascetState, user: IUserState) {

    var products = bascet.products.map((x) => {
        return { ProductID: x.product.id, Count: x.count }
    });


    var model = {
        Products: products
    }

    var ret = new Promise((resolve, reject) => {
        fetch(apiRoot + Settings.OrderUrl,
            {
                method: "POST",
                headers: headers,
                body: JSON.stringify({
                    model
                })
            })
            .then((response) => {
                if (response.ok) {
                    return response.json();
                } else {
                    return Promise.reject(response);
                }
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