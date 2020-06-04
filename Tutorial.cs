Controller
----------
Controller is a class that contains public methods called Action method and handles incoming browser requests, retrieves necessary model data and returns appropriate responses. Every controller class name must end with a word "Controller" Like HomeController and StudentController.

    public class StudentController : Controller
    {
        public string Index(string id)
        {
            return "ID =" + id;
        }
    }

Action Method
-------------

Index
        public ActionResult Index(){
            return View();
        }

Details
        public ActionResult Details(int id){
            return View();
        }

Create
        public ActionResult Create(){
            return View();
        }
Create
        [HttpPost]
        public ActionResult Create(FormCollection collection){
            try{
                return RedirectToAction("Index");
            }
            catch{
                return View();
            }
        }
Edit
        public ActionResult Edit(int id){
            return View();
        }
Edit
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection){
            try{
                return RedirectToAction("Index");
            }
            catch{
                return View();
            }
        }
Delete
        public ActionResult Delete(int id){
            return View();
        }
Delete
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection){
            try{
                return RedirectToAction("Index");
            }
            catch{
                return View();
            }
        }
        

Action Selectors
----------------
Action selector is the attribute that can be applied to the action methods.

1.ActionName
2.NonAction
3.ActionVerbs


ActionName
----------
ActionName attribute allows us to specify a different action name than the method name

Use http://localhost:62833/Home/About to call this

	public class HomeController : Controller
	{
		public ActionResult About(){
			ViewBag.Message = "Your application description page.";
			return View();
		}

	}

Use http://localhost:62833/Home/Detail to call this after using [ActionName("Detail ")]

	public class HomeController : Controller
	{
		[ActionName("Detail ")]
		public ActionResult About(){
			ViewBag.Message = "Your application description page.";
			return View();
		}
	}


NonAction 
---------
NonAction attribute indicates that not all the public method of a Controller is an action method.

Here public method “TimeString” cannot be invoked the same way as action method

	public class HomeController : Controller
	{
		public string GetCurrentTime(){
		 return TimeString();
		}

		[NonAction]
		public string TimeString(){
		 return "Time is " + DateTime.Now.ToString("T");
		}
	}


ActionVerbs
-----------
ActionVerbs used to control the selection of an action method based on a Http request method.

	•HttpGet - To retrieve the information from the server. Parameters will be appended in the query string.
	•HttpPost - To create a new resource
	•HttpPut - To update an existing resource
	•HttpDelete - To delete an existing resource
	•HttpOptions - Represents a request for information about the communication options supported by web server.
	•HttpPatch - To full or partial update the resource

you can define two different action methods with the same name but action method will respond to what (get, post, etc…) request used with HTTP. By default it is GET request.

	public class HomeController : Controller
	{
		[HttpPost]
		public ActionResult Search(string name){
		 var input = Server.HtmlEncode(name);
		 return Content(input);
		}

		[HttpGet]
		public ActionResult Search(){
		 var input = "Another Search action";
		 return Content(input);
		}

	}

Model
-----
Model is a class that contains domain specific data and business logic. It is used to interact with database. Model objects retrieve and store model state in the persistance store like a database.

    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime JoiningDate { get; set; }
        public int Age { get; set; }
    }

Model binding
-------------
Model binding is a mechanism which converts a query string to the action method parameters. These parameters can be of primitive type or complex type. http://localhost/Student/Edit?id=1&name=John would map to id and name parameter of the following Edit action method.

Single Value Binding - http://localhost/Student/Edit?id=1
---------------------------------------------------------

    public class HomeController : Controller
    {
		public ActionResult Edit(int id)
		{            
		    // do something here
		            
		    return View();
		}
	}

Multiple Values Binding - http://localhost/Student/Edit?id=1&name=John
---------------------------------------------------------------------

    public class HomeController : Controller
    {
		public ActionResult Edit(int id, string name)
		{            
		    // do something here
		            
		    return View();
		}
	}

Multiple Values Binding from html Form submission
-------------------------------------------------

    public class HomeController : Controller
    {
		[HttpPost]
		public string Edit(FormCollection std)
		{
			var id = std["StudentId"];
			var name = std["StudentName"];
			var age = std["Age"];

			return std["StudentName"] + "=" + std["Age"];
		}
	}

Complex types Value Binding
---------------------------

Model
-----

    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }
    }

Now we can use 'Student' type parameter in our Edit action method. Now, MVC will automatically maps Form collection values to Student type parameter when the form submits with POST request.

Controller
----------
    public class HomeController : Controller
    {
		[HttpPost]
		public string Edit(Student std)
		{			
			var id = std.StudentId;
			var name = std.StudentName;
			var age = std.Age;

			return std.StudentName+"="+ std.Age;
		}
	}

Bind attribute
--------------
MVC framework also enables you to specify which properties of a model class you want to bind. The [Bind] attribute will let you specify the exact properties of a model will be included or excluded.

Controller
----------
    public class HomeController : Controller
    {
		[HttpPost]
		public string Edit([Bind(Include = "StudentId, StudentName")] Student std)
		{			
			var id = std.StudentId;
			var name = std.StudentName;
			var age = std.Age;

			return std.StudentName+"="+ std.Age;
		}
    }

Above you will not get any value for ‘Age’ as it is not included in the Bind attribute.

Controller
----------
    public class HomeController : Controller
    {
		[HttpPost]
		public string Edit([Bind(Exclude = "StudentId, StudentName")] Student std)
		{			
			var id = std.StudentId;
			var name = std.StudentName;
			var age = std.Age;

			return std.StudentName+"="+ std.Age;
		}    
}

Above you will not get any value for ‘StudentId’ and ‘StudentName’ as it is excluded in the Bind attribute.

Routing
-------
Routing eliminate needs of mapping each URL with a physical file (http://domain/studentsinfo.aspx), with the help of routing we can do like http://domain/students. It can be used with ASP.NET Webform or MVC. Routing has request handler (Controller class for MVC and ‘.aspx’ file for ASP.NET Webform).

You can register a route in RouteConfig class, which is in RouteConfig.cs under App_Start folder.

RouteConfig
-----------

	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}

Controller
----------

	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}

The URL pattern is considered only after domain name part in the URL i.e localhost:1234/{controller}/{action}/{id}. Anything after "localhost:1234/" would be considered as controller name. The same way, anything after controller name would be considered as action name and then value of id parameter. If the URL doesn't contain anything after domain name then the default controller and action method will handle the request.

Different URLs considering above default route.

+-----------------------------------+-------------------+---------+------+
|                URL                |    Controller     | Action  |  Id  |
+-----------------------------------+-------------------+---------+------+
| http://localhost/home             | HomeController    | Index   | null |
| http://localhost/home/index/123   | HomeController    | Index   | 123  |
| http://localhost/home/about       | HomeController    | About   | null |
| http://localhost/home/contact     | HomeController    | Contact | null |
+-----------------------------------+-------------------+---------+------+

Multiple Routes
---------------
Custom route require at least two parameters in MapRoute, route name and url pattern. The Defaults parameter is optional. You can register multiple custom routes with different names
	
RouteConfig
-----------
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);

			routes.MapRoute(
				name: "Student",
				url: "students/{id}",
				defaults: new { controller = "Student", action = "Index" }
			);

		}
	}

Controller
----------

    public class StudentController : Controller
    {
        public string Index(string id)
        {
            return "ID =" + id;
        }
    }

From the above example, URL pattern for the Student route is students/{id}, which specifies that any URL that starts with domainName/students, must be handled by StudentController. Notice that we haven't specified {action} in the URL pattern because we want every URL that starts with student should always use Index action of StudentController. We have specified default controller and action to handle any request which starts from domainname/students.

MVC framework evaluates each route in sequence. It starts with first route and if incoming request doesn't satisfy the URL pattern then it will go to second route and so on.

Different URLs will be mapped to Student route

+------------------------------------+-------------------+--------+-----+
|                URL                 |    Controller     | Action | Id  |
+------------------------------------+-------------------+--------+-----+
| http://localhost/student/index/123 | StudentController | Index  | 123 |
| http://localhost/student?Id=123    | StudentController | Index  | 123 |
+------------------------------------+-------------------+--------+-----+

You can also apply restrictions on the value of parameter by configuring route constraints.

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Student",
				url: "student/{id}/{name}/{standardId}",
				defaults: new { controller = "Student", action = "Index", id = UrlParameter.Optional, name = UrlParameter.Optional, standardId = UrlParameter.Optional },
				constraints: new { id = @"\d+" }
			);

		}

From the above example, id must be numeric and if any non-numeric value found then "The resource could not be found" error will be thrown.

Register Routes
---------------
Now, after configuring all the routes in RouteConfig class, you need to register it in the Application_Start() event in the Global.asax. So that it includes all your routes into RouteTable.

	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
	}
