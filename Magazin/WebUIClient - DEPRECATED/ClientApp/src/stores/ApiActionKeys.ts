export enum ApiActionKeys {
    News_Read = "News_Read",
    News_Fetching = "News_Fetching",
    News_Error = "News_Error",


    Product_Read = "Product_Read",
    Product_Fetching = "Product_Fetching",
    Product_Error = "Product_Error",

    Category_Read = "Category_Read",
    Category_Fetching = "Category_Fetching",
    Category_Error = "Category_Error",


    Card_Add = "Card_Add",
    Card_Remove = "Card_Remove", // Добавить -1 к продукту
    Card_Remove_Product = "Card_Remove_Product",  // Удалить все по продукту

    Login_Fetching = "Login_Fetching",
    Login_Error = "Login_Error",
    Login_Success = "Login_Success",
    Login_Open_Form = "Login_Open_Form",
    Login_Register_Form = "Login_Register_Form",

    Register_Fetching = "Register_Fetching",
    Register_Complete = "Register_Complete",
    Register_Error = "Register_Error",


    Order_Fetching = "Order_Fetching",
    Order_Error = "Order_Error",
    Order_Complete = "Order_Complete",
}