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
* Entity Framework Core 6.0
* AutoMapper
* xUnit
* JavaScript
* jQuery
* Ajax request with partial view
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


### Database Diagram
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1671263618/DatabaseDiagrams_xfua0h.jpg)

### Code coverage with Xunit and Moq
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1671003907/Code_coverege_2022-12-14_09_bsmjg6.png)


## Screen Shots:

### Home Page:
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1670275196/home-page-internetERP_hp5zjo.jpg)

### Login Page:
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1670275518/Login_qacacc.jpg)

### Extra Login Providers:
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1670767966/ExtraLoginProviders_zimkr5.jpg)

### Admin DashBoard:
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1670767966/AdminDashBoard_npyysw.jpg)

### Admin DashBoard with VC Latest Users:
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1671112127/AdminDashBoardVCLatestUsers_nulicm.jpg)

### Manage Users:
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1670767966/ManageUsers_dwyhpo.jpg)

### Update Users Profiles:
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1670767966/UpdateUsersProfile_lat8ve.jpg)

### Manage Roles:
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1670767966/ManageRoles_yi0unn.jpg)

### Delete Town:
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1670767966/DeleteTown_kenufm.jpg)

### Create Product:
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1670767966/ManagerCreateProduct_yqmznp.jpg)

### Manage Products:
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1670767966/ManageProducts_gercho.jpg)

### Manage Failure Teams:
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1670767966/ManageFailureTeams_hwlted.jpg)

### Step 1 : Create New Sale:
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1670767965/CreateNewSale_kav6zz.jpg)

### Step 2 : Sell Products:
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1670767965/Step2SellingProducts_zq472m.jpg)

### Step 3 : Sell Service:
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1670767965/SellService_awsawp.jpg)

### Step 4 : Sale Failure:
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1670767965/SaleFailure_viaail.jpg)

### Step 4.1 : Sale Failure with Ajax, get failure history:
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1671100815/SaleFailureWithAjax_um34sv.jpg)

### Step 5 : Checkout:
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1670767965/Checkout_ok1ms8.jpg)

### Step 5 : BankTransfer:
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1670767965/BankTransfer_tfltoo.jpg)

### Step 5 : PayPal:
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1670767965/PayPal_sdjp5k.jpg)

### Create Invoice:
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1670767965/CreateInvoice_rbf34i.jpg)

### Invoice:
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1670767965/Invoice_ewpeyp.jpg)

### Create Failure:
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1670769223/CreateFailure_a6i4x5.jpg)

### All Failures:
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1670770599/AllFailures_cse4ti.jpg)

### Contact Us By Mail:
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1670769747/ContactUpByMail_budd3m.jpg)

### Contact Us By Phone Or Address:
![alt text](https://res.cloudinary.com/dqzm8tfvg/image/upload/v1670769662/ContactUpByPhoneOrAddress_pmdnrd.jpg)





