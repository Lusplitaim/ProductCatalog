# Product Catalog App

A simple app that allows to work with different products. You can:
* Create and edit product category;
* Create and edit product;
* Filter products;
* Manage users.

## Build
To build a server app:
1. Open a console in `api` directory;
2. Run `dotnet build` to build server solution;
3. Run `dotnet ef database update --project ./ProductCatalog.API` to create a local database.

To build a client app:
1. Open a console in `client` directory;
2. Run `npm i`.

## Running the project
After the building steps are done, you can run the application through console.

To run server app:
1. Open a console in `api` directory;
2. Run `dotnet run --project ./ProductCatalog.API --launch-profile https`.

To run client app:
1. Open a console in `client` directory;
2. Run `npm run start`.
