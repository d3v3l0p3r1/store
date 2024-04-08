#Import

Для импорта нужно скачать из 1с нужные xml(offers, import). 
Сначала импортировать номенклатуру POST api/exchange/FileInput с form-data: { file : binary data(import.xml)}.
Затем импорт цен для этого POST api/exchange/FileInput/UpdatePrices с form-data: { file : binary data(offers.xml)}.
Загрузка картинок из 1С /api/exchange/FileInput/UpdatePictures