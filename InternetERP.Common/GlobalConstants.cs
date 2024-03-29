﻿namespace InternetERP.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "InternetERP";

        public const string AdministratorRoleName = "Administrator";
        public const string EmployeeRoleName = "Employee";
        public const string ManagerRoleName = "Manager";
        public const string TechnicianRoleName = "Technician";
        public const string SalesRoleName = "Sales";
        public const string InternetAccountRoleName = "InternetAccount";

        // Paging defaults
        public const int ItemsPerPageGrid = 8;
        public const int ItemsPerPageList = 5;

        // InputViewModel Error
        public const string TextError = "The {0} must be at least {2} and at max {1} characters long.";
        public const string RangeErrorPrice = "Value for {0} must be at least {1} and max {2}.";
        public const string PositiveNumberError = "Only positive number allowed";
        public const string EmailTextError = "The {0} must be max {1} characters long.";

        // Path for Products image
        public const string RootPathForImages = "images";
        public const string ProductsPathForImages = "Products";

        // Init status
        public const string BillFinishedStatus = "Finished";
        public const int BillNewStatusId = 1; // New Sale
        public const int BillAddProductsId = 2;
        public const int BillAddServicesId = 3;
        public const int BillCheckoutId = 4;
        public const int BillMakeInvoiceId = 5;
        public const int BillFinishedId = 6;


        public const int FailureNewStatusId = 1; // Register
        public const string FailureFinishedStatus = "Finished";
        public const int FailureFinishedStatusId = 3;

        public const string ConstMailSubject = "InternetERP form MailSubject";

        public const string DummyTestImage = "dummyTestImage.jpg";

        // Users data for Seeding
        public static class DataForSeeding
        {
            public const string PasswordHash = "AQAAAAEAACcQAAAAEOr5MhkFf5aTLQxqzFSfcqPFcoNXXsNWFfFG7xCzzgrPNJCFAlIZHh/WV8sBBZiMHw=="; // 123456;
            public const string AdminName = "Admin";
            public const string AdminEmail = "interneterp.adm@gmail.com";
            public const string EmployeeManagerName = "EmployeeManager";
            public const string EmployeeManagerEmail = "interneterp.employee.manager@gmail.com";
            public const string EmployeeSalesName = "EmployeeSales";
            public const string EmployeeSalesEmail = "interneterp.employee.sales@gmail.com";
            public const string InternetAccountName = "InternetAccount";
            public const string InternetAccountEmail = "interneterp.user@gmail.com";
        }
    }
}
