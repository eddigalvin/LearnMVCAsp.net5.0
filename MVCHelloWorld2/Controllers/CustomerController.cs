using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
 //ie the folder containing CustomerDal.cs
using MVCHelloWorld2.ViewModel;
using System.Threading;
using DataAccessLayer;

namespace MVCHelloWorld2.Controllers
{

    public class CustomerBinder : IModelBinder
    {

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {

            //throw new NotImplementedException();
            HttpContextBase objContext = controllerContext.HttpContext;
            string custCode = objContext.Request.Form["TextCustomerCode"];
            string custName = objContext.Request.Form["TextCustomerName"];

            Customer obj = new Customer()
            {
                CustomerCode = custCode,
                CustomerName = custName
            };
            return obj;

        }
    }
    [Authorize]
    public class CustomerUIController : Controller   //this controller is for user interface(ie load EnterCustomer.cshtml)

    {

        public ActionResult EnterCustomer()
        {
            return View();
        }

        public ActionResult SearchCustomer()
        {
            return View();
        }
    }
}  
        // GET: LoadCustomerDisplay
//        public ActionResult Load()
//    {
//        Customer obj = new Customer();
//        obj.CustomerName = "TOM";
//        obj.CustomerCode = "C001";
//        //obj.Amount = 1000;  
//        return View("Customer", obj);
//    }

//    public ActionResult EnterCustomer()
//    {
//        CustomerViewModel obj = new CustomerViewModel();
//        obj.customer = new Customer();
//        /*     CustomerDal dal = new CustomerDal();
//             List<Customer> customerscoll = dal.Customers.ToList<Customer>();  //create list of customers from sql CustomerDB using DAL*****
//             obj.customers = customerscoll;     ///    LAB 11  modification  comment out above  section ,, now performed in GetCustomers  */

//        return View("EnterCustomer", obj);
//        //  return View("EnterCustomer" , new Customer() );
//    }



//    public ActionResult GetCustomers()  // Returns JSON collection
//    {
//        Dal.Dal dal = new Dal.Dal();  //   see    CustomerDal.cs  has one property ie. DbSet  "Customers"(ie dbase object in memory)
//                                      //"Customers" is created using CustomerDal class.........using OnModelBuildingCreation etc.
//        List<Customer> customerscoll = dal.Customers.ToList<Customer>(); //take dbset obj "Customers" in mem 
//                                                                         //and convert it to List<Customer> customerscoll
//                                                                         //  Thread.Sleep(10000);// lab 12 simulate long entity frmwrk process to demo sync v's async process.....
//        return Json(customerscoll, JsonRequestBehavior.AllowGet);  // return customerscoll as Json Collection........
//    }
//    [ActionName("GetCustomersByName")]
//    public ActionResult GetCustomers(/*string CustomerName = "SHIV"*//**/Customer obj)  // Returns JSON collection
//    {
//        Dal.Dal dal = new Dal.Dal();
//        List<Customer> customerscoll = (from temp in dal.Customers
//                                            //note Customers is dal class data member of type C:\MVCHelloWorld2\MVCHelloWorld2\Dal\Dal.cs dbset<Customer>
//                                        where temp.CustomerName == /**/obj.CustomerName
//                                        select temp).ToList<Customer>();
//        return Json(customerscoll, JsonRequestBehavior.AllowGet);
//    }
//    public ActionResult EnterSearch()
//    {
//        CustomerViewModel obj = new CustomerViewModel();
//        obj.customers = new List<Customer>();
//        return View("SearchCustomer", obj);
//    }

//    public ActionResult SearchCustomer()
//    {
//        CustomerViewModel obj = new CustomerViewModel();
//        Dal.Dal dal = new Dal.Dal();
//        string str;
//        str = Request.Form["txtCustomerName"].ToString();
//        List<Customer> customerscoll
//            = (from x in dal.Customers
//               where x.CustomerName == str
//               select x).ToList<Customer>();
//        obj.customers = customerscoll;
//        return View("SearchCustomer", obj);

//    }

//    public ActionResult Submit(/*[ModelBinder(typeof(CustomerBinder))] */ /*"this was b4 ebview lab11"Customer obj*/
//        Customer obj/*Customer obj input param is added in anglr lab 18 as post from entr cstmr sends JSON*/)  // VALIDATION RUNS
//    {
//        //comment out line below coz are not using ViewModel now but JSON  LAB 13 AJAX
//        //  CustomerViewModel vm = new CustomerViewModel();
//        /*        Customer obj = new Customer();
//                  obj.CustomerName = Request.Form["Customer.CustomerName"];
//                  obj.CustomerCode = Request.Form["Customer.CustomerCode"]; //THIS COMMENT OUT ENDING HERE DONE IN LAB 18 */

//        /* Customer obj = new Customer();
//        obj.CustomerName = Request.Form["CustomerName"];
//        obj.CustomerCode = Request.Form["CustomerCode"];
//      //  obj.Amount = Request.Form["CustomerAmount"]; */
//        if (ModelState.IsValid)
//        {
//            //insert the customer object to the database using Ent Frmwk DAL
//            Dal.Dal Dal = new Dal.Dal();
//            Dal.Customers.Add(obj); //in memory add to Customers collection
//            Dal.SaveChanges(); // physical commit of change in collection to database

//            //     return View("Customer", obj);
//            //comment out 2 lineS REF vm below coz are not using ViewModel now but JSON  LAB 13 AJAX
//            // vm.customer = new Customer();
//        }
//        //      else
//        //  {
//        //   vm.customer = obj;
//        //   }
//        //re-load customer collection furnished from upated sql db to EnterCustomer View 
//        Dal.Dal dal = new Dal.Dal();
//        List<Customer> customerscoll = dal.Customers.ToList<Customer>();
//        //   vm.customers = customerscoll;
//        //  {                                             
//        //return View("EnterCustomer", /*obj*/ vm);
//        //comment out return View() line above and replace with return JSON line below coz now only want to return JSON DATA
//        //and not full view EnterCustomer
//        return Json(customerscoll, JsonRequestBehavior.AllowGet);
//        //   }
//    }
//    public ActionResult SubmitBeforeLab18(/*[ModelBinder(typeof(CustomerBinder))] */ /*"this was b4 ebview lab11"Customer obj*/)  // VALIDATION RUNS
//    {
//        //comment out line below coz are not using ViewModel now but JSON  LAB 13 AJAX
//        //  CustomerViewModel vm = new CustomerViewModel();
//        Customer obj = new Customer();
//        obj.CustomerName = Request.Form["Customer.CustomerName"];
//        obj.CustomerCode = Request.Form["Customer.CustomerCode"];

//        /* Customer obj = new Customer();
//        obj.CustomerName = Request.Form["CustomerName"];
//        obj.CustomerCode = Request.Form["CustomerCode"];
//      //  obj.Amount = Request.Form["CustomerAmount"]; */
//        if (ModelState.IsValid)
//        {
//            //insert the customer object to the database using Ent Frmwk DAL
//            Dal.Dal Dal = new Dal.Dal();
//            Dal.Customers.Add(obj); //in memory add to Customers collection
//            Dal.SaveChanges(); // physical commit of change in collection to database

//            //     return View("Customer", obj);
//            //comment out 2 lineS REF vm below coz are not using ViewModel now but JSON  LAB 13 AJAX
//            // vm.customer = new Customer();
//        }
//        //      else
//        //  {
//        //   vm.customer = obj;
//        //   }
//        //re-load customer collection furnished from upated sql db to EnterCustomer View 
//        Dal.Dal dal = new Dal.Dal();
//        List<Customer> customerscoll = dal.Customers.ToList<Customer>();
//        //   vm.customers = customerscoll;
//        //  {                                             
//        //return View("EnterCustomer", /*obj*/ vm);
//        //comment out return View() line above and replace with return JSON line below coz now only want to return JSON DATA
//        //and not full view EnterCustomer
//        return Json(customerscoll, JsonRequestBehavior.AllowGet);
//        //   }
//    }
//}        

  
