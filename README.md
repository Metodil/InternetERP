## InternetERP
## Final Web-Project at SoftUni using ASP.NET Core


* InternetERP is a management, sales and contact system in a company related to Internet services.
* Offers division into 6 roles (Admin, Manager, Sales, Technician and Internet Users). 
* Each with specific activities and responsibilities. 
* Extends ASP.NET Core Identity with new capabilities.

### Description and Futures:
	Register and Login:
    - using ASP.NET Core Identity
    - login with Facebook and Google account 
    - Extent Identity with distinct, street, town ...
	Admin futures:
    - has dashboard
    - manage all users.
      -edit profile information
      -add or remove users from role
    - add, remove and edit roles in identity.
    - manage Addresses.
	Manager futures:
    - has dashboard
    - add new products.
    - edit/update product.
    - add/remove technician from failure teams
	Sales futures:
    - has dashboard
    - create new sale in 5 step.
      -creates a sale to a specific internet user
      -sale product
      -sale service(internet montly payment)
      -sale failure amount(If there is)
      -chekout for sale
        -choose payment:
          -PayPal integrated
          -Bank transfer, create invoice
      -create new failure to a specific internet user    
	Technician futures:
    - has dashboard
		- edit failure amount.
    - change status of failure
	Internet User futures:
    - has dashboard
    - create failure
    - take information for paymets and failures
   

```diff
+ In Technician area is implemented filtration for failures. 
+ Pagination implemented in all listing pages.
+ Third-party authentication include - register with Facebook or Google account.
```
## Using ASP.NET Core 6.0.5 Template by : [Nikolay Kostov](https://github.com/NikolayIT)

### Technologies and tools used:
* .NET Core 6.0
* ASP.NET Core 6.0
* SignalR
* Entity Framework Core 6.0
* AutoMapper
* xUnit
* JavaScript
* jQuery
* Bootstrap
* HTML 5
* CSS
* FontAwesome
* Cloudinary
* Using OAuth 2.0
* SendGrid

### Dependencies:
* [Cloudinary](https://www.cloudinary.com/)
* [SendGrid](https://www.sendgrid.com/)

## Screen Shots:

### Home Page:
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1670275196/home-page-internetERP_hp5zjo.jpg)

### Home Page:
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1670275518/Login_qacacc.jpg)




