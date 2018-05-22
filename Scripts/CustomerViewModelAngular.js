function CustomerViewModel($scope, $http) //$scope inside brckts mns js fn asks anglr for pwr of scope Mngemnt service..(and http svce)
    // ie scoping vars to html elmnts*@ 
{
       $scope.Customer =
    {
               "CustomerCode:": "",
               "CustomerName": "",
               "CustomerAmount": "",
               "CustomerAmountColor": ""
};

       $scope.Customers = {};

       $scope.Errors = {};

$scope.$watch("Customers", function () {

    for (x = 0; x < $scope.Customers.length; x++) {
        var cust = $scope.Customers[x];
        cust.CustomerAmountColor = $scope.getColor
        (cust.CustomerAmount);
    }
});

$scope.getColor = function (Amount) {
    if (Amount == 0) { return "Green"; }
    else if (Amount > 100) { return "Blue"; }
    else { return "Red"; }
}

$scope.$watch("Customer.CustomerAmount", function ()
{
    $scope.Customer.CustomerAmountColor = $scope.getColor($scope.Customer.CustomerAmount);
}); 
    $scope.Add = function () {//make a call to the server to add data
        $http({ method: "POST", data: $scope.Customer, url: /*"Submit"*/"http://localhost:63288/Api/Customer" })
        .success(function (data, status, headers, config) {
            if (data.IsValid) {
                //Load the collection 'customer'
                $scope.Customers = data.Data; //'data' is JSON cllctn returned by 'Submit' ActnRslt// now angular has customers vrble to display
                //all dbase contents on entercustmr.cshtml page
                $scope.Customer =
          {
              "CustomerCode:": "",
              "CustomerName": "",
              "CustomerAmount": "",  
              "CustomerAmountColor": ""
          };

                $scope.Load();
            }
            else {
                $scope.Errors = data.Data;//add .Errors to change $scope.Errors from single JSON object  to 
                //object(Errors property of error class) which is list of strings ????
                //ie data.Data only represents the Error object err which is passed back by web api ctrlr
                //it is an Object of type object.....which contains the error strings    TRICKY!!!!!!!!!!!!!!!!!!!!
                 }
        });
    }
    $scope.Update = function () {//make a call to the server to update data
        $http({ method: "PUT", data: $scope.Customer, url: /*"Submit"*/"http://localhost:63288/Api/Customer" })
        .success(function (data, status, headers, config) {
            //Load the collection 'customer'
            $scope.Customers = data; //'data' is JSON cllctn returned by 'Submit' ActnRslt// now angular has ustomers vrble to display
            //all dbase contents on entercustmr.cshtml page
            $scope.Customer =
      {
          "CustomerCode:": "",
          "CustomerName": "",
          "CustomerAmount": "",
          "CustomerAmountColor": ""
      };

            $scope.Load();
        });
    }
    $scope.Delete = function ()
    {//make a call to the server to add data
        $http.defaults.headers["delete"] = { 'Content-Type': 'application/json;charset=utf-8' };
        $http({ method: "DELETE", data: $scope.Customer, url: /*"Submit"*/"http://localhost:63288/Api/Customer" })
        .success(function (data, status, headers, config) {
            //Load the collection 'customer'
            $scope.Customers = data; //'data' is JSON cllctn returned by 'Submit' ActnRslt// now angular has ustomers vrble to display
            //all dbase contents on entercustmr.cshtml page
            $scope.Customer =
      {
          "CustomerCode:": "",
          "CustomerName": "",
          "CustomerAmount": "",
          "CustomerAmountColor": ""
      };

            $scope.Load();
        });
    }

    $scope.Load = function ()
   {
        $http({ method: "GET", url:/*"GetCustomers"*/ "http://localhost:63288/Api/Customer" }).success(function (data, status, headers, config)
                      { $scope.Customers = data; });
    }
    $scope.LoadByName = function () {
   /*     $scope.Customer =
     {
         "CustomerCode:": "",
         "CustomerName": "SHIV",
         "CustomerAmount": "",
         "CustomerAmountColor": ""
     };  */
       
        var custsearch  = $scope.Customer;
        $http({
            method: "GET",/* params: custsearch,  ....NOW USING QUERY SYNTAX (see below qry in url:...)..
        IN LAB 21 WEBAPI DONT NEED TO PASS DATA custsearch IN HTTP GET METHOD*/
            url: /*"GetCustomersByName"*/"http://localhost:63288/Api/Customer?CustomerName=" + $scope.Customer.CustomerName
        })    //DATA IS NOW PASSED BY QUERY STRING LAB21....THIS IS USING QUERY STRING
            //THIS PART OF LAB21 IS SHOWING THAT HTTP GET IS USED TO SEND QUERY
            //HTTP POST IS USED FOR SENDING DATA...CURRENTLY IN WEBDSGN THEY ARE NOT USED PROPERLY LIKE DIS!!!
            .success(function (data, status, headers, config)
            {
                $scope.Customers = data;
            });
    }
    $scope.LoadByCode = function (CustomerCode)
    {
        $http({ method: "GET", url: "http://localhost:63288/Api/Customer?CustomerCode=" + CustomerCode })
            .success(function (data, status, headers, config) 
            {
                //$scope.Load();
               $scope.Customers = data;
                $scope.Customer = $scope.Customers[0]; //TRICKY ON LAB11 19 MIN TO GET WORKING  !!!...fils input boxes but only once!!!
                $scope.Load();
            });
    }
    $scope.Load(); 
    //$scope.LoadByName();
}
/*
//app
var app = angular.module("myApp", []);  //creating app
//controller
app.controller('CustomerViewModel', CustomerViewModel);   //registering the ViwModel object in the controller
*/