import { Promise } from 'es6-promise';
import { Settings } from "./Settings"
import { Product } from "./models/Product"

const apiRoot = "http://localhost:51145";

function call(endpoint: string) {
    const fullUrl = apiRoot + endpoint;

    return fetch(fullUrl);
}

export function readProducts(category: number): Promise<ReadonlyArray<Product>> {

    var ret = new Promise<ReadonlyArray<Product>>((resolve, reject) => {
        call(Settings.ProductURL)
            .then((response) => {                
                return  response.json();                                
            }).then((json) => {                
                const arr = json.Data.map((x : any) => {
                    let product = new Product();

                    product.id = x.Id;
                    product.description = x.Description;
                    product.img = x.File;
                    product.price = x.Price;
                    product.title = x.Title;

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