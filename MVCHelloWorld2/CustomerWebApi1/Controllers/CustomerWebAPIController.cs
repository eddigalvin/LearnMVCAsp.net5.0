using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models;
using DataAccessLayer;

using System.Web.Mvc;
using System.Web.Http.Filters;

namespace MVCHelloWorld2.Controllers
{

    public class AllowCrossSite : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response != null)
                actionExecutedContext.Response.Headers.Add("Access-Control-Allow-Origin","*");
            base.OnActionExecuted(actionExecutedContext);
        }

    }

    public class Error
    {
        public List<string> Errors { get; set; } = new List<string> {};//this INITIALIZATION USING NEW KEYWORD TOOK DAYS!!!
    }                                                                //AND FIXES THE NULL REF PROBLEM !!!!!!!!
    public class ClientData
    {
        public bool IsValid { get; set; }
        public object Data { get; set; }
    }

    [AllowCrossSite]
    public class CustomerController : ApiController  //THIS CTRLR IS FOR DATA ............SEND/REQUEST.....
    {
      
        //WebApi INSERT mthd
        public /*List<Customer>*/Object Post(Customer obj)   //this is equiv of "Submit" actn in MVC
        {

            ClientData Data = new ClientData();
            if (ModelState.IsValid)
            {
                //insert the customer object to the database using Ent Frmwk DAL
                Dal Dal = new Dal();
                Dal.Customers.Add(obj); //in memory add to Customers collection
                Dal.SaveChanges(); // physical commit of change in collection to database
            }
            //     return View("Customer", obj);
            //comment out 2 lineS REF vm below coz are not using ViewModel now but JSON  LAB 13 AJAX
            // vm.customer = new Customer();
            else
            {
                //  object Errors = null;
                var Err = new Error();
       //         err.Errors { "aaa" "bbb" "ccc"  } ;
           
                foreach(var modelState in ModelState)
                {
                    foreach(var error in modelState.Value.Errors)
                    {
                        Err.Errors.Add(error.ErrorMessage);  //MAJOR PROB HERE WITH LAB 25 ...LIST...INITIALIZATION...SYNTAX!!!!
                    }   
                }
                
        Data.IsValid = false;//lab 24
                Data/*New ClntData object*/.Data = Err.Errors/*"BASIC error msg !!! fx error report,, null ref issue for Error.Errors.Add(List<string>"*/;
                //lab25 also changed from 'Data.Data = Err;' to.....Data.Data = Err.Errors; so that the $scope.Errors object
                //in CustomerViewModel angular ctrlr becomes collection of Errors insted of single Error(JSON) object.....
                //THIS IS KEY POINT FOR BINDING/JSON IN ANGULAR CTRLR AND UNDERSTAND ANGLR OBJECT CREATION/NOTATION
                return Data;
                //return err;
            }
            //re-load customer collection furnished from upated sql db to EnterCustomer View 
            Dal dal = new Dal();//the entity frmwrk object which contains the customer collection.....
            List<Customer> customerscoll = dal.Customers.ToList<Customer>();
            Data.IsValid = true;
            Data.Data = customerscoll;
            return Data;
            
        //    return customerscoll;   //dont need return Json(customerscoll,....) coz of CONTENT NEGOTIATION
                                    //key concept in WebAPI
}
       
        
        
        //WebApi SELECT/GET/REQUEST/RETRIEVE Method (all dbase)
        public List<Customer> Get()
        {
            //13 min on lab23 now query dbase using db context and query strings from angular based client
            //and only use one GET method for all.....operations(ie using webapi method using HTTP SERVICE.........)

            var allUrlKeyValues = ControllerContext.Request.GetQueryNameValuePairs();

            string CustomerCode = allUrlKeyValues.SingleOrDefault(x=> x.Key=="CustomerCode").Value;
            string CustomerName = allUrlKeyValues.SingleOrDefault(x => x.Key == "CustomerName").Value;

            //previous code
            Dal dal = new Dal();
            List<Customer> customerscoll = new List<Customer>();
            if (CustomerName != null)
            {
                customerscoll = (from t in dal.Customers
                                 where t.CustomerName == CustomerName
                                 select t).ToList<Customer>();
            }
            else if (CustomerCode != null)
            {
                customerscoll = (from t in dal.Customers
                                 where t.CustomerCode == CustomerCode
                                 select t).ToList<Customer>();
            }
            else
            {
                 customerscoll = dal.Customers.ToList<Customer>();
            }
            return customerscoll;
        }

        //WebApi SELECT/GET/REQUEST/RETRIEVE Method (search dbase)
        public List<Customer> Get(string CustomerName)//input var must be same name
                                                      //as the name in the string query in anglr method LoadByName ie 
                                                      //"/api/Customer?CustomerName="
        {
            Dal dal = new Dal();
            List<Customer> customerscoll = (from t in dal.Customers
                                            where t.CustomerName == CustomerName
                                            select t).ToList<Customer>();


            return customerscoll;

        }
        
        //WebApi Put Method
        public List<Customer> PUT(Customer obj)
        {
            Dal dal = new Dal();
            Customer custUpdate = (from t in dal.Customers
                                   where t.CustomerCode == obj.CustomerCode
                                   select t).ToList<Customer>()[0];
            custUpdate.CustomerAmount = obj.CustomerAmount;
            custUpdate.CustomerName = obj.CustomerName;

            dal.SaveChanges();

            List<Customer> customerscoll = dal.Customers.ToList<Customer>();
            return customerscoll;
        }

        //WebApi Delete Method

        public List<Customer> DELETE(Customer obj)
        {
            Dal dal = new Dal();
            Customer custDelete = (from t in dal.Customers
                                   where t.CustomerCode == obj.CustomerCode
                                   select t).ToList<Customer>()[0];
            dal.Customers.Remove(custDelete);
            dal.SaveChanges();


            List<Customer> customerscoll = dal.Customers.ToList<Customer>();
            return customerscoll;
        }
    }
    }
