# SnackHub

This is the source code for SnackHub, designed to be the first step of FIAP's SOAT Tech Challenges.<br>
It intends to apply the concepts presented so far, such as Domain Driven Design, Containerization and Hexagonal Architecture.<br>

The API handles a few operations on a small fast food restaurant, including: <br>
The application touches some domains and let the user execute some use cases, they are:

- Clients domain
  - Create new clients;
  - Get a registered client by `Id` or by `CPF`;
- Products domain
  - Create, Update and Delete products;
  - Get a product by Id;
  - Get a `list of products` by `Category`;
  - Get a list containing all registered products;
- Order domain
  - Confirm a new order by passing a client identifier and registered products;
  - Cancel a non-accept(non-paid) order;
  - `Checkout/Pay` the order, creating a new `Kitchen Order`;
- Kitchen Order domain
  - Get a `list containing all registered Kitchen orders approved/created`;
  - Update the `Kitchen order state`;

## Documentation
This application is based on DDD and you can find the `Event Storm` diagrams and `Ubiquitous Language` by following those links:  
 - [Event Storm](https://miro.com/app/board/uXjVKUq0krI=/?share_link_id=69852294691)
 - [Ubiquitous Language](https://funny-language-87c.notion.site/Linguagem-Ub-qua-4edadb2ca03e438e8df846940bfe90b3)

## Running the Application

This application is a webapi, based on `dotnet core`, to make it easy to execute it, it makes use of `docker/docker compose`.
So, `please, make sure you have docker and docker compose installed at your environment`.

### Using Docker Compose

For a simpler setup, `navigate to the solutions root folder in your terminal and execute`:

```sh
docker compose up -d
```
This command will automatically build and start all the necessary services, including the `SnackHub api` and `MongoDB`, based on the configuration in the `docker-compose.yml` file.
The api will expose two main endpoint, they are:

 - A http endpoint, that can be access by: http://localhost:5000

In order to make it simpler to test, it exposes swagger as a way to interact with the api, it can be accessed at:
- http://localhost:5000/swagger

## Application folder Structure
<details>

```sh
.
├── deploy
│   ├── docker-compose.yml
│   └── Dockerfile
├── docker-compose.dcproj
├── docker-compose.override.yml
├── docker-compose.yml
├── launchSettings.json
├── LICENSE
├── Makefile
├── README.md
├── SnackHub.sln
├── src
│   ├── SnackHub.Api
│   │   ├── appsettings.Development.json
│   │   ├── appsettings.json
│   │   ├── Configuration
│   │   │   ├── MongoDbSettings.cs
│   │   │   └── StorageSettings.cs
│   │   ├── Controllers
│   │   │   ├── ClientController.cs
│   │   │   ├── KitchenOrderController.cs
│   │   │   ├── OrderController.cs
│   │   │   └── ProductController.cs
│   │   ├── Dockerfile
│   │   ├── Extensions
│   │   │   ├── AddNotificationsExtensions.cs
│   │   │   ├── MongoDbExtensions.cs
│   │   │   ├── RepositoriesExtensions.cs
│   │   │   ├── ServicesExtensions.cs
│   │   │   ├── UseCasesExtensions.cs
│   │   │   └── ValidatorsExtensions.cs
│   │   ├── Program.cs
│   │   ├── Properties
│   │   │   └── launchSettings.json
│   │   ├── SnackHub.Api.csproj
│   │   └── SnackHub-Poc.http
│   ├── SnackHub.Application
│   │   ├── Client
│   │   │   ├── Contracts
│   │   │   │   ├── IGetClientUseCase.cs
│   │   │   │   ├── IRegisterClientUseCase.cs
│   │   │   │   └── IRegisterClientValidator.cs
│   │   │   ├── Models
│   │   │   │   ├── GetClientResponse.cs
│   │   │   │   ├── RegisterClientRequest.cs
│   │   │   │   └── RegisterClientResponse.cs
│   │   │   └── UseCases
│   │   │       ├── GetClientUseCase.cs
│   │   │       ├── RegisterClientUseCase.cs
│   │   │       └── RegisterClientValidator.cs
│   │   ├── KitchenOrder
│   │   │   ├── Contracts
│   │   │   │   ├── ICreateKitchenOrderUseCase.cs
│   │   │   │   ├── IListKitchenOrdertUseCase.cs
│   │   │   │   └── IUpdateKitchenOrderStatusUseCase.cs
│   │   │   ├── Models
│   │   │   │   ├── CreateKitchenOrderRequest.cs
│   │   │   │   ├── CreateKitchenOrderResponse.cs
│   │   │   │   ├── KitchenOrderResponse.cs
│   │   │   │   ├── UpdateKitchenOrderStatusRequest.cs
│   │   │   │   └── UpdateKitchenOrderStatusResponse.cs
│   │   │   └── UseCases
│   │   │       ├── CreateKitchenOrderUseCase.cs
│   │   │       ├── ListKitchenOrdertUseCase.cs
│   │   │       └── UpdateKitchenOrderStatusUseCase.cs
│   │   ├── Order
│   │   │   ├── Contracts
│   │   │   │   ├── ICancelOrderUseCase.cs
│   │   │   │   ├── ICheckoutOrderUseCase.cs
│   │   │   │   ├── IConfirmOrderUseCase.cs
│   │   │   │   └── IListOrderUseCase.cs
│   │   │   ├── Models
│   │   │   │   ├── CancelOrderRequest.cs
│   │   │   │   ├── CancelOrderResponse.cs
│   │   │   │   ├── CheckoutOrderRequest.cs
│   │   │   │   ├── CheckoutOrderResponse.cs
│   │   │   │   ├── ConfirmOrderRequest.cs
│   │   │   │   ├── ConfirmOrderResponse.cs
│   │   │   │   └── OrderResponse.cs
│   │   │   └── UseCases
│   │   │       ├── CancelOrderUseCase.cs
│   │   │       ├── CheckoutOrderUseCase.cs
│   │   │       ├── ConfirmOrderUseCase.cs
│   │   │       └── ListOrderUseCase.cs
│   │   ├── Payment
│   │   │   ├── Contracts
│   │   │   │   └── IPaymentGatewayService.cs
│   │   │   ├── Models
│   │   │   │   ├── CreditCard.cs
│   │   │   │   ├── OnTheHouse.cs
│   │   │   │   ├── PaymentMethod.cs
│   │   │   │   ├── PaymentRequest.cs
│   │   │   │   ├── PaymentResponse.cs
│   │   │   │   └── PaymentStatus.cs
│   │   │   └── Services
│   │   │       └── FakePaymentGatewayService.cs
│   │   ├── Product
│   │   │   ├── Contracts
│   │   │   │   ├── IGetByCategoryUseCase.cs
│   │   │   │   ├── IGetProductUseCase.cs
│   │   │   │   └── IManageProductUseCase.cs
│   │   │   ├── Models
│   │   │   │   ├── GetProductResponse.cs
│   │   │   │   ├── ManageProductRequest.cs
│   │   │   │   └── ManageProductResponse.cs
│   │   │   └── UseCases
│   │   │       ├── GetByCategoryUseCase.cs
│   │   │       ├── GetProductUseCase.cs
│   │   │       └── ManageProductUseCase.cs
│   │   └── SnackHub.Application.csproj
│   ├── SnackHub.Domain
│   │   ├── Base
│   │   │   ├── DomainException.cs
│   │   │   ├── Entity.cs
│   │   │   ├── IAggregateRoot.cs
│   │   │   └── ValueObject.cs
│   │   ├── Contracts
│   │   │   ├── IClientRepository.cs
│   │   │   ├── IKitchenOrderRepository.cs
│   │   │   ├── IOrderRepository.cs
│   │   │   └── IProductRepository.cs
│   │   ├── Entities
│   │   │   ├── CategoryEnum.cs
│   │   │   ├── Client.cs
│   │   │   ├── KitchenOrder.cs
│   │   │   ├── Order.cs
│   │   │   └── Product.cs
│   │   ├── SnackHub.Domain.csproj
│   │   └── ValueObjects
│   │       ├── CPF.cs
│   │       ├── KitchenOrderStatus.cs
│   │       ├── KitchenOrdertItem.cs
│   │       ├── OrderItem.cs
│   │       └── OrderStatus.cs
│   └── SnackHub.Infra
│       ├── Repositories
│       │   ├── Abstractions
│       │   │   └── IBaseRepository.cs
│       │   ├── InMemory
│       │   │   ├── ClientRepository.cs
│       │   │   └── ProductRepository.cs
│       │   └── MongoDB
│       │       ├── BaseRepository.cs
│       │       ├── ClientRepository.cs
│       │       ├── KitchenOrderRepository.cs
│       │       ├── OrderRepository.cs
│       │       └── ProductRepository.cs
│       └── SnackHub.Infra.csproj
└── test
    ├── SnackHub.Application.Tests
    │   ├── Services
    │   │   └── FakePaymentGatewayServiceShould.cs
    │   ├── SnackHub.Application.Tests.csproj
    │   └── UseCases
    │       ├── CancelOrderShould.cs
    │       ├── CheckoutOrderShould.cs
    │       ├── ConfirmOrderShould.cs
    │       ├── GetClientShould.cs
    │       ├── GetProductsByCategoryShould.cs
    │       ├── RegisterClientShould.cs
    │       └── RegisterClientValidatorShould.cs
    └── SnackHub.Domain.Tests
        ├── Entities
        │   └── OrderShould.cs
        ├── SnackHub.Domain.Tests.csproj
        └── ValueObjects
            ├── CPFShould.cs
            └── OrderItemShould.cs
```
</details>

## Testing this application
First of all, the system should have a client to be used. It could be an anonymous one if you wish.
Bellow, we have a curl command that will create a user name 

### Creating a new user

```shell
curl -X 'POST' \
  'http://localhost:5000/api/Client/v1' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
  "name": "Anonymous Client",
  "cpf": "10097159093"
}'
```

As a response, you should have a return similar to this:
```json
{"id":"e46569a7-635b-4d05-b488-fa92d9f9fc41","notifications":[],"isValid":true}
```

### Get client by Id

```shell
curl -X 'GET' \
  'http://localhost:5000/api/Client/v1/{client-id}' \
  -H 'accept: text/plain'
```

### Get client by CPF
```shell
curl -X 'GET' \
  'http://localhost:5000/api/Client/v1/{client-cpf}' \
  -H 'accept: text/plain'
```

## Stack

- C# 12
- .Net 8
- mongodb
- Docker
- Docker compose

## External dependencies

At this moment, this application rely only on [MongoDb](https://www.mongodb.com/) as it is the main storage of this
application.

## Variables/Docker Env

The application need to be configured with some variables, as follows

| Variable Name            | Value/default         | Type    |
|--------------------------|-----------------------|---------|
| Storage:MongoDb:Host     | localhost / 127.0.0.1 | string  |
| Storage:MongoDb:Port     | 27017                 | integer |
| Storage:MongoDb:UserName | admin                 | string  |
| Storage:MongoDb:Password | admin                 | string  |
| Storage:MongoDb:Database | snackhub              | string  |

When executing the api through the `docker compose`, it will be configured by using the `.env` file found at the solutions root folder.