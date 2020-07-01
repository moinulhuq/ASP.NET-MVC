Views
-----
Views are stored in Views folder. Different action methods of a controller can have different views. Views folder contains a separate folder for each controller, name that folder name as per controller name, to store multiple views. Now, name the view same as controller’s action method so that you don't have to specify view name explicitly.

Naming view according to the controller name

Scaffolding structure
---------------------
Controllers
    . HomeController.cs – Methods (Index, Edit, …)

Views
    . Shared
    . Home
        . Index.cshtml
        . Edit.cshtml

Shared folder contains views, layouts or partial views which will be shared among multiple views

In razor view, we can use both html tags and server side code. Razor uses @ character for server side code instead of traditional <% %>. You can use C# or Visual Basic syntax to write server side code inside razor view. Razor views files have .cshtml or vbhtml extension.

ASP.NET MVC supports following types of view files

+---------+----------------------------------------------------------------+
| .cshtml |           C# Razor view. Supports C# with html tags.           |
| .vbhtml | Visual Basic Razor view. Supports Visual Basic with html tags. |
| .aspx   | ASP.Net web form                                               |
| .ascx   | ASP.NET web control                                            |
+---------+----------------------------------------------------------------+
 
Best way to create view 

1.  Open HomeController class -> right click inside Index method -> click Add View
2.  Keep the view name as Index
3.  From template select Empty view
4.  From Model Select Home Model
5.  Check "Use a layout page" checkbox and select _Layout.cshtml page for this view

Example
-------
	@model IEnumerable<MVCWebApp.Models.Student>

	@{
	    ViewBag.Title = "Index";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

	<h2>Index</h2>
	<p>
	    @Html.ActionLink("Create New", "Create")
	</p>

	<table class="table">
	    <tr>
	        <th>
	            @Html.DisplayNameFor(model => model.StudentName)
	        </th>
	        <th>
	            @Html.DisplayNameFor(model => model.Age)
	        </th>
	        <th></th>
	    </tr>

	@foreach (var item in Model) {
	    <tr>
	        <td>
	            @Html.DisplayFor(modelItem => item.StudentName)
	        </td>
	        <td>
	            @Html.DisplayFor(modelItem => item.Age)
	        </td>
	        <td>
	            @Html.ActionLink("Edit", "Edit", new { id=item.StudentId }) |
	            @Html.ActionLink("Details", "Details", new { id=item.StudentId }) |
	            @Html.ActionLink("Delete", "Delete", new { id=item.StudentId })
	        </td>
	    </tr>
	}
	</table>

Inline expression
-----------------
Start with @ symbol to write server side C# or VB code with Html code. A single line expression does not require a semicolon at the end of the expression.

	<h1>Razor syntax demo</h1>
	<h2>@DateTime.Now.ToShortDateString()</h2>

Multi-statement Code block
--------------------------

	@{
	    var date = DateTime.Now.ToShortDateString();
	    var message = "Hello World";
	}

	<h2>Today's date is: @date </h2>
	<h3>@message</h3>

Declare Variables
-----------------

	@{
	    string str = "";

	    if (1 > 0)
	    {
	        str = "Hello World!";
	    }
	}

	<p>@str</p>

Comment
-------
To comment in view, HTML along with other code use @*  *@.

@*
    @{
        string str = "";

        if (1 > 0)
        {
            str = "Hello World!";
        }
    }

    <p>@str</p>

*@

Display Text from Code Block
----------------------------
Use @: or <text>/<text> to display texts within code block.

	@{
	    var date = DateTime.Now.ToShortDateString();
	    string message = "Hello World!";
	    @:Today's date is: @date
	    <br />
	    @message
	}

		(OR)

	@{
	    var date = DateTime.Now.ToShortDateString();
	    string message = "Hello World!";
	    <text>Today's date is:</text> @date
	    <br />
	    @message
	}

if-else condition
-----------------
Start with @ symbol and block must be enclosed in braces { }, even for single statement.

	@if (DateTime.IsLeapYear(DateTime.Now.Year))
	{
	    @DateTime.Now.Year @:is a leap year.
	}
	else
	{
	    @DateTime.Now.Year @:is not a leap year.
	}

for loop
--------

	@for (int i = 0; i < 5; i++)
	{
	    @i.ToString()
	    <br />
	}

Model data display
------------------
Use @model to use model object anywhere in the view.

Example - Model
---------------
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }
    }

Example - Controller
--------------------
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
			Student std = new Student
			{
				StudentId = 101,
				StudentName = "Bouchers",
				Age = 36
			};

			return View(std);
        }
	}

Example - View
--------------
	@model MVCWebApp.Models.Student

	<h2>Student Detail:</h2>
	<ul>
	    <li>Student Id: @Model.StudentId</li>
	    <li>Student Name: @Model.StudentName</li>
	    <li>Age: @Model.Age</li>
	</ul>

HtmlHelper
----------
HtmlHelper class generates html elements. It binds the model object to html elements to display value and also assigns the value of the html elements to the model properties while submitting web form.

	<h2>Index</h2>
	<p>
	    @Html.ActionLink("Create New", "Create")
	</p>
	<table class="table">
	    <tr>
	        <th>
	            @Html.DisplayNameFor(model => model.StudentName)
	        </th>
	        <th>
	            @Html.DisplayNameFor(model => model.Age)
	        </th>
	        <th></th>
	    </tr>

	@foreach (var item in Model) {
	    <tr>
	        <td>
	            @Html.DisplayFor(modelItem => item.StudentName)
	        </td>
	        <td>
	            @Html.DisplayFor(modelItem => item.Age)
	        </td>
	        <td>
	            @Html.ActionLink("Edit", "Edit", new { id=item.StudentId }) |
	            @Html.ActionLink("Details", "Details", new { id=item.StudentId }) |
	            @Html.ActionLink("Delete", "Delete", new { id=item.StudentId })
	        </td>
	    </tr>
	}
	</table>

@Html is an object of HtmlHelper class and ActionLink() and DisplayNameFor() is extension methods.

	@Html.ActionLink("Create New", "Create") => <a href="/Student/Create">

	@Html.DisplayNameFor(model => model.StudentName) => StudentName

Textbox
-------
Html.TextBox(string name, string value, object htmlAttributes) is loosely typed method because name parameter is a string. The name parameter can be a property name of model object.

	@Html.TextBox("StudentName", null, new { @class = "form-control" }) => <input class="form-control" id="StudentNam" name="StudentNam" type="text" value="">

	(OR)

	@Html.TextBox("StudentName", “This is value”, new { @class = "form-control" }) => <input class="form-control" id="StudentNam" name="StudentNam" type="text" value=" This is value ">

TextBoxFor
----------
Html.TextBoxFor(Expression<Func<TModel,TValue>> expression, object htmlAttributes) is strongly typed method because it generates text input for model property using lambda expression.

	@Html.TextBoxFor(m => m.StudentName, new { @class = "form-control" }) => <input class="form-control" id="StudentName" name="StudentName" type="text" value="Bouchers">


TextArea
--------
Html.TextArea(string name, string value, object htmlAttributes) will creates textarea with rows=2 and cols=20.

	@Html.TextArea("Description", null, new { @class = "form-control" }) => <textarea class="form-control" cols="20" id="Description" name="Description" rows="2"></textarea>

TextAreaFor
-----------
@Html.TextAreaFor(<Expression<Func<TModel,TValue>> expression, object htmlAttributes) generates a multi line <textarea> element using lambda expression.

	@Html.TextAreaFor(m => m.Description, new { @class = "form-control" }) => <textarea class="form-control" cols="20" id="StudentName" name="StudentName" rows="2"> </textarea> 

CheckBox
--------
Html.CheckBox(string name, bool isChecked, object htmlAttributes)

	@Html.CheckBox("isNewlyEnrolled", true) => <input checked="checked" id="isNewlyEnrolled" name="isNewlyEnrolled" type="checkbox" value="true">

CheckBoxFor
-----------
Html.CheckBoxFor(<Expression<Func<TModel,TValue>> expression, object htmlAttributes)

	@Html.CheckBoxFor(m => m.isNewlyEnrolled) => 
	<input data-val="true" data-val-required="The isNewlyEnrolled field is required." id="isNewlyEnrolled" name="isNewlyEnrolled" type="checkbox" value="true">
	<input name="isNewlyEnrolled" type="hidden" value="false">

Above, notice that, it generated additional hidden field with same 'name' and value=false. This is because when you submit a form, if the checkbox is checked ‘true’ will be sent otherwise ‘false’ will be sent because of that hidden field. If there is no hidden field, nothing will be sent to the server.

RadioButton 
-----------
Html.RadioButton(string name, object value, bool isChecked, object htmlAttributes)

	Male:   @Html.RadioButton("Gender","Male")  
	Female: @Html.RadioButton("Gender","Female")  

	=> 

	Male:   <input id="Gender" name="Gender" type="radio" value="Male">
	Female: <input id="Gender" name="Gender" type="radio" value="Female">

RadioButtonFor
--------------
@Html. RadioButtonFor(<Expression<Func<TModel,TValue>> expression, object value, object htmlAttributes)

	@Html.RadioButtonFor(m => m.Gender,"Male")
	@Html.RadioButtonFor(m => m.Gender,"Female") 

	=>

	Male:   <input data-val="true" data-val-required="The Gender field is required." id="Gender" name="Gender" type="radio" value="Male">
	Female: <input id="Gender" name="Gender" type="radio" value="Female">

DropDownList 
------------
Html.DropDownList(string name, IEnumerable<SelectLestItem> selectList, string optionLabel, object htmlAttributes)

	@Html.DropDownList("StudentGender", new SelectList(Enum.GetValues(typeof(Gender))), "Select Gender", new { @class = "form-control" })

	=> 

	<select class="form-control" data-val="true" data-val-required="The StudentGender field is required." id="StudentGender" name="StudentGender">
		<option value="">Select Gender</option>
		<option selected="selected">Male</option>
		<option>Female</option>
	</select>

DropDownListFor
---------------
Html.DropDownListFor(Expression<Func<dynamic,TProperty>> expression, IEnumerable<SelectLestItem> selectList, string optionLabel, object htmlAttributes)

	@Html.DropDownListFor(m => m.StudentGender, new SelectList(Enum.GetValues(typeof(Gender))), "Select Gender")

	=>

	<select data-val="true" data-val-required="The StudentGender field is required." id="StudentGender" name="StudentGender">
		<option value="">Select Gender</option>
		<option selected="selected">Male</option>
		<option>Female</option>
	</select>


Example - Model
---------------

	namespace MVCWebApp.Models
	{    
	    public class Student
	    {
	        public int StudentId { get; set; }
	        public string StudentName { get; set; }
	        public int Age { get; set; }
	        public bool isNewlyEnrolled { get; set; }
	        public Gender StudentGender  { get; set; }
	    }

	    public enum Gender
	    {
	        Male,
	        Female
	    }
	}

Example – Controller
--------------------

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
			Student std = new Student
			{
				StudentId = 101,
				StudentName = "Bouchers",
				Age = 36
			};

			return View(std);
        }
	}

Example – View
--------------

	@using MVCWebApp.Models
	@model MVCWebApp.Models.Student

	@{
	    ViewBag.Title = "Index";
	}

	<h2>Index</h2>
	<table class="table">
	    <tr>
	        <th>
	            HTML ELement
	        </th>
	    </tr>
	    <tr>
	        <th>
	            @Html.DropDownList("StudentGender", new SelectList(Enum.GetValues(typeof(Gender))), "Select Gender", new { @class = "form-control" })
	        </th>
	    </tr>
	</table>

Hidden
------
Html.Hidden(string name, object value, object htmlAttributes)

	@Html.Hidden("StudentId")

	=> 

	<input data-val="true" data-val-number="The field StudentId must be a number." data-val-required="The StudentId field is required." id="StudentId" name="StudentId" type="hidden" value="101" />

HiddenFor
---------
Html.HiddenFor(Expression<Func<dynamic,TProperty>> expression)

	@Html.HiddenFor(m => m.StudentId)

	=>

	<input data-val="true" data-val-number="The field StudentId must be a number." data-val-required="The StudentId field is required." id="StudentId" name="StudentId" type="hidden" value="101" />

Password
--------
Html.Password(string name, object value, object htmlAttributes)

	@Html.Password("OnlinePassword", null, new { @class = "form-control" })

	=> 

	<input class="form-control" id="OnlinePassword" name="OnlinePassword" type="password">

PasswordFor
-----------
Html.PasswordFor(Expression<Func<dynamic,TProperty>> expression, object htmlAttributes)

	@Html.PasswordFor(m => m.Password)

	=>

	<input id="OnlinePassword" name="OnlinePassword" type="password">

Display
-------
To create html string literal. Html.Display("StudentName")

	@Html.Display("StudentName")

	=> 

	“Moin”

DisplayFor
----------
Html.DisplayFor(<Expression<Func<TModel,TValue>> expression)

	@Html.DisplayFor(m => m.StudentName)

	=>

	“Moin”

Label
-----
Html.Label(string expression, string labelText, object htmlAttributes)

	@Html.Label("StudentName")

	=> 

	<label for="StudentName">StudentName</label>

You can specify another text instead of property name

	@Html.Label("StudentName", "Student-Name")

	=> 

	<label for="StudentName">Student-Name</label>

LabelFor
--------
Html.LabelFor(<Expression<Func<TModel,TValue>> expression)

	@Html.LabelFor(m => m.StudentName)

	=>

	<label for="StudentName">StudentName</label>

Editor
------
Different HtmlHelper methods used to generated different html elements but we can use ‘Editor’ methods which will generates html input elements based on the datatype. Html.Editor(string propertyname)

The following Html elements can be created by Editor() or EditorFor().

+----------------+--------------------------+
| string         | <input type="text" >     |
| int            | <input type="number" >   |
| decimal, float | <input type="text" >     |
| boolean        | <input type="checkbox" > |
| Enum           | <input type="text" >     |
| DateTime       | <input type="datetime" > |
+----------------+--------------------------+


	@Html.Editor("StudentId") 

	=> 
	
	<input class="text-box single-line" data-val="true" data-val-number="The field StudentId must be a number." data-val-required="The StudentId field is required." id="StudentId" name="StudentId" type="number" value="101">

	@Html.Editor("StudentName") 

	=> 

	<input class="text-box single-line" id="StudentName" name="StudentName" type="text" value="Bouchers">

	@Html.Editor("Age") 

	=> 

	<input class="text-box single-line" data-val="true" data-val-number="The field Age must be a number." data-val-required="The Age field is required." id="Age" name="Age" type="number" value="36">

	@Html.Editor("OnlinePassword") 

	=> 

	<input class="text-box single-line" id="OnlinePassword" name="OnlinePassword" type="text" value="">

	@Html.Editor("isNewlyEnrolled") 

	=> 

	<input class="check-box" data-val="true" data-val-required="The isNewlyEnrolled field is required." id="isNewlyEnrolled" name="isNewlyEnrolled" type="checkbox" value="true"><input name="isNewlyEnrolled" type="hidden" value="false">

	@Html.Editor("Gender") 

	=> 

	<input class="text-box single-line" id="Gender" name="Gender" type="text" value="">

	@Html.Editor("DoB") 

	=> 

	<input class="text-box single-line" data-val="true" data-val-date="The field DoB must be a date." data-val-required="The DoB field is required." id="DoB" name="DoB" type="datetime" value="1/01/0001 12:00:00 am">

EditorFor
---------
Html.EditorFor(<Expression<Func<TModel,TValue>> expression)

	@Html.EditorFor(m => m.StudentId) 

	=> 

	<input class="text-box single-line" data-val="true" data-val-number="The field StudentId must be a number." data-val-required="The StudentId field is required." id="StudentId" name="StudentId" type="number" value="101">

	@Html.EditorFor(m => m.StudentName) 

	=> 

	<input class="text-box single-line" id="StudentName" name="StudentName" type="text" value="Bouchers">

	@Html.EditorFor(m => m.Age) 

	=> 

	<input class="text-box single-line" data-val="true" data-val-number="The field Age must be a number." data-val-required="The Age field is required." id="Age" name="Age" type="number" value="36">

	@Html.EditorFor(m => m.Password) 

	=> 

	<input class="text-box single-line" id="OnlinePassword" name="OnlinePassword" type="text" value="">

	@Html.EditorFor(m => m.isNewlyEnrolled) 

	=> 

	<input class="check-box" data-val="true" data-val-required="The isNewlyEnrolled field is required." id="isNewlyEnrolled" name="isNewlyEnrolled" type="checkbox" value="true"><input name="isNewlyEnrolled" type="hidden" value="false">

	@Html.EditorFor(m => m.Gender) 

	=> 

	<input class="text-box single-line" data-val="true" data-val-required="The StudentGender field is required." id="StudentGender" name="StudentGender" type="text" value="Male">

	@Html.EditorFor(m => m.DoB) 

	=> 

	<input class="text-box single-line" data-val="true" data-val-date="The field DoB must be a date." data-val-required="The DoB field is required." id="DoB" name="DoB" type="datetime" value="1/01/0001 12:00:00 am">

ActionLink
------------
Html..ActionLink("LinkText", "ActionName", "ControllerName", "Parameter", htmlAttributes)

	@Html.ActionLink("LinkText", "ActionName", "ControllerName", new { id = "123" }, new { @class = "form-control" }) => <a class="form-control" href="/ControllerName/ActionName/123">LinkText</a>

Form Validation
---------------
DataAnnotations attributes are used to implement validations. DataAnnotations includes built-in validation attributes like Required, Range for different type of validation which can be applied to the properties of model class.

List of DataAnnotations attributes

+-------------------+---------------------------------------------------------------------------------------+
|     Attribute     |                                      Description                                      |
+-------------------+---------------------------------------------------------------------------------------+
| Required          | Indicates that the property is a required field                                       |
| StringLength      | Defines a maximum length for string field                                             |
| Range             | Defines a maximum and minimum value for a numeric field                               |
| RegularExpression | Specifies that the field value must match with specified Regular Expression           |
| CreditCard        | Specifies that the specified field is a credit card number                            |
| CustomValidation  | Specified custom validation method to validate the field                              |
| EmailAddress      | Validates with email address format                                                   |
| FileExtension     | Validates with file extension                                                         |
| MaxLength         | Specifies maximum length for a string field                                           |
| MinLength         | Specifies minimum length for a string field                                           |
| Phone             | Specifies that the field is a phone number using regular expression for phone numbers |
+-------------------+---------------------------------------------------------------------------------------+

Form validation can be done in three ways

	a) ValidtionMessage
	b) ValidationMessageFor
	c) ValidationSummary

ValidtionMessage
----------------
Html.ValidateMessage(string modelName, string validationMessage, object htmlAttributes)

	@model MVCWebApp.Models.Student

    <div class="form-group">
        @Html.LabelFor(m => m.StudentName)
        @Html.TextBoxFor(m => m.StudentName, new { @class = "form-control" })
        @Html.ValidationMessage("StudentName", "", new { @class = "text-danger" })
    </div>

	=>

	<div class="form-group">
		<label for="StudentName">StudentName</label>
		<input class="input-validation-error form-control" data-val="true" data-val-required="The StudentName field is required." id="StudentName" name="StudentName" type="text" value="">
		<span class="field-validation-error text-danger" data-valmsg-for="StudentName" data-valmsg-replace="true">The StudentName field is required.</span>
	</div>

Above, First parameter is Model property name, second parameter is custom error message and third parameter is html attributes.

Example - ValidtionMessage
--------------------------

	View
	----
	@model MVCWebApp.Models.Student

	<body>
	    @using (Html.BeginForm("Index", "Home", FormMethod.Post))
	    {
		    @Html.AntiForgeryToken()
		    <div class="form-horizontal">
		        <h4>Student</h4>
			    <div class="form-group">
			        @Html.LabelFor(m => m.StudentFName)
			        @Html.TextBoxFor(m => m.StudentFName, new { @class = "form-control" })
			        @Html.ValidationMessage("StudentFName", "", new { @class = "text-danger" })
			    </div>

			    <div class="form-group">
			        @Html.LabelFor(m => m.StudentLName)
			        @Html.TextBoxFor(m => m.StudentLName, new { @class = "form-control" })
			        @Html.ValidationMessage("StudentLName", "", new { @class = "text-danger" })
			    </div>

			    <div class="form-group">
			        @Html.LabelFor(m => m.Age)
			        @Html.TextBoxFor(m => m.Age, new { @class = "form-control" })
			        @Html.ValidationMessage("Age", "", new { @class = "text-danger" })
			    </div>

		        <div class="form-group">
		            <div class="col-md-offset-2 col-md-10">
		                <input type="submit" value="Create" class="btn btn-default" />
		            </div>
		        </div>
		    </div>
	    }
	</body>

	Model
	-----
	using System.ComponentModel.DataAnnotations;

	public class Student
	{
	    public int StudentId { get; set; }
	    [Required]
	    public string StudentFName { get; set; }
	    [Required]
	    public string StudentLName { get; set; }
	    [Required]
	    public int Age { get; set; }
	}

	Controller
	----------
	using MVCWebApp.Models;

	public class HomeController : Controller
	{
	    public ActionResult Index()
	    {
	        return View("Index");
	    }

	    [HttpPost]
	    public ActionResult Index(Student std)
	    {
	        return View("Index");
	    }
	}

ValidationMessageFor
--------------------
Html.ValidateMessageFor(Expression<Func<dynamic,TProperty>> expression, string validationMessage, object htmlAttributes)

	@model MVCWebApp.Models.Student

    <div class="form-group">
        @Html.LabelFor(m => m.StudentName)
        @Html.TextBoxFor(m => m.StudentName, new { @class = "form-control" })
        @Html.ValidationMessageFor(model => model.StudentName, "", new { @class = "text-danger" })
    </div>

    =>

	<div class="form-group">
		<label for="StudentName">StudentName</label>
		<input class="form-control" data-val="true" data-val-required="The StudentName field is required." id="StudentName" name="StudentName" type="text" value="">
		<span class="field-validation-valid text-danger" data-valmsg-for="StudentName" data-valmsg-replace="true"></span>
	</div>

Example - ValidationMessageFor
------------------------------

	View
	----
	@model MVCWebApp.Models.Student

	<body>
	    @using (Html.BeginForm("Index", "Home", FormMethod.Post))
	    {
	        @Html.AntiForgeryToken()
	        <div class="form-horizontal">
	            <h4>Student</h4>
	            <div class="form-group">
	                @Html.LabelFor(m => m.StudentFName)
	                @Html.TextBoxFor(m => m.StudentFName, new { @class = "form-control" })
	                @Html.ValidationMessageFor(model => model.StudentFName, "", new { @class = "text-danger" })
	            </div>

	            <div class="form-group">
	                @Html.LabelFor(m => m.StudentLName)
	                @Html.TextBoxFor(m => m.StudentLName, new { @class = "form-control" })
	                @Html.ValidationMessageFor(model => model.StudentLName, "", new { @class = "text-danger" })
	            </div>

	            <div class="form-group">
	                @Html.LabelFor(m => m.Age)
	                @Html.TextBoxFor(m => m.Age, new { @class = "form-control" })
	                @Html.ValidationMessageFor(model => model.Age, "", new { @class = "text-danger" })
	            </div>

	            <div class="form-group">
	                <div class="col-md-offset-2 col-md-10">
	                    <input type="submit" value="Create" class="btn btn-default" />
	                </div>
	            </div>
	        </div>
	    }
	</body>

	Model
	-----
	using System.ComponentModel.DataAnnotations;

	public class Student
	{
	    public int StudentId { get; set; }
	    [Required]
	    public string StudentFName { get; set; }
	    [Required]
	    public string StudentLName { get; set; }
	    [Required]
	    public int Age { get; set; }
	}

	Controller
	----------
	using MVCWebApp.Models;

	public class HomeController : Controller
	{
	    public ActionResult Index()
	    {
	        return View("Index");
	    }

	    [HttpPost]
	    public ActionResult Index(Student std)
	    {
	        return View("Index");
	    }
	}


Custom Error Message
--------------------
You can display your own error message instead of the default. Put it either

in the second parameter of ValidationMessageFor()

	@model MVCWebApp.Models.Student

    <div class="form-group">
        @Html.LabelFor(m => m.StudentName)
        @Html.TextBoxFor(m => m.StudentName, new { @class = "form-control" })
        @Html.ValidationMessage("StudentName", "Please enter name", new { @class = "text-danger" })
    </div>

    (OR)

	@model MVCWebApp.Models.Student

    <div class="form-group">
        @Html.LabelFor(m => m.StudentName)
        @Html.TextBoxFor(m => m.StudentName, new { @class = "form-control" })
        @Html.ValidationMessageFor(model => model.StudentName, "Please enter name", new { @class = "text-danger" })
    </div>


Example - Custom Error Message in ValidationMessage/ValidationMessageFor
------------------------------------------------------------------------

	View
	----
	@model MVCWebApp.Models.Student

	<body>
	    @using (Html.BeginForm("Index", "Home", FormMethod.Post))
	    {
	        @Html.AntiForgeryToken()
	        <div class="form-horizontal">
	            <h4>Student</h4>
	            <div class="form-group">
	                @Html.LabelFor(m => m.StudentFName)
	                @Html.TextBoxFor(m => m.StudentFName, new { @class = "form-control" })
	                @Html.ValidationMessageFor(model => model.StudentFName, "Please enter StudentFName", new { @class = "text-danger" })
	            </div>

	            <div class="form-group">
	                @Html.LabelFor(m => m.StudentLName)
	                @Html.TextBoxFor(m => m.StudentLName, new { @class = "form-control" })
	                @Html.ValidationMessageFor(model => model.StudentLName, "Please enter StudentLName", new { @class = "text-danger" })
	            </div>

	            <div class="form-group">
	                @Html.LabelFor(m => m.Age)
	                @Html.TextBoxFor(m => m.Age, new { @class = "form-control" })
	                @Html.ValidationMessageFor(model => model.Age, "Please enter Age", new { @class = "text-danger" })
	            </div>

	            <div class="form-group">
	                <div class="col-md-offset-2 col-md-10">
	                    <input type="submit" value="Create" class="btn btn-default" />
	                </div>
	            </div>
	        </div>
	    }
	</body>

	Model
	-----
	using System.ComponentModel.DataAnnotations;

	public class Student
	{
	    public int StudentId { get; set; }
	    [Required]
	    public string StudentFName { get; set; }
	    [Required]
	    public string StudentLName { get; set; }
	    [Required]
	    public int Age { get; set; }
	}

	Controller
	----------
	using MVCWebApp.Models;

	public class HomeController : Controller
	{
	    public ActionResult Index()
	    {
	        return View("Index");
	    }

	    [HttpPost]
	    public ActionResult Index(Student std)
	    {
	        return View("Index");
	    }
	}

or in the DataAnnotations attribute like "[Required], [Range(5,50)], etc..."

	using System.ComponentModel.DataAnnotations;

    public class Student
    {
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public string StudentName { get; set; }
        [Range(5,50)]
        public int Age { get; set; }
    }

Example - Custom Error Message in Model Object
----------------------------------------------

	View
	----
	@model MVCWebApp.Models.Student

	<body>
	    @using (Html.BeginForm("Index", "Home", FormMethod.Post))
	    {
	        @Html.AntiForgeryToken()
	        <div class="form-horizontal">
	            <h4>Student</h4>
	            <div class="form-group">
	                @Html.LabelFor(m => m.StudentFName)
	                @Html.TextBoxFor(m => m.StudentFName, new { @class = "form-control" })
	                @Html.ValidationMessageFor(model => model.StudentFName, "", new { @class = "text-danger" })
	            </div>

	            <div class="form-group">
	                @Html.LabelFor(m => m.StudentLName)
	                @Html.TextBoxFor(m => m.StudentLName, new { @class = "form-control" })
	                @Html.ValidationMessageFor(model => model.StudentLName, "", new { @class = "text-danger" })
	            </div>

	            <div class="form-group">
	                @Html.LabelFor(m => m.Age)
	                @Html.TextBoxFor(m => m.Age, new { @class = "form-control" })
	                @Html.ValidationMessageFor(model => model.Age, "", new { @class = "text-danger" })
	            </div>

	            <div class="form-group">
	                <div class="col-md-offset-2 col-md-10">
	                    <input type="submit" value="Create" class="btn btn-default" />
	                </div>
	            </div>
	        </div>
	    }
	</body>

	Model
	-----
	using System.ComponentModel.DataAnnotations;

    public class Student
    {
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Required StudentFName")]
        public string StudentFName { get; set; }
        [Required(ErrorMessage = "Required StudentLName")]
        public string StudentLName { get; set; }
        [Required(ErrorMessage = "Required Age")]
        public int Age { get; set; }
    }

	Controller
	----------
	using MVCWebApp.Models;

	public class HomeController : Controller
	{
	    public ActionResult Index()
	    {
	        return View("Index");
	    }

	    [HttpPost]
	    public ActionResult Index(Student std)
	    {
	        return View("Index");
	    }
	}

ValidationSummary
-----------------
ValidateMessage(bool excludePropertyErrors, string message, object htmlAttributes)

ValidationSummary help to generate an unordered list (ul element) of validation messages for all the fields. It can also be used to display custom error messages. If it is set to true it will show only model-level errors and exclude model property-level but If it's set to false then it shows both model-level and model property-level errors. Model-level errors are those that are not specific to a particular property of model.

    ModelState.AddModelError(string.Empty, "This is model level error!");
    ModelState.AddModelError("StudentName", "This is model property level error!");

Example
-------
	@model MVCWebApp.Models.Student

    @Html.ValidationSummary(false, "", new { @class = "text-danger" })
    <div class="form-group">
        @Html.LabelFor(m => m.StudentName)
        @Html.TextBoxFor(m => m.StudentName, new { @class = "form-control" })
    </div>

    =>

	<div class="validation-summary-errors text-danger" data-valmsg-summary="true">
		<ul>
			<li>Please enter student name.</li>
			<li>The Age field is required.</li>
		</ul>
	</div>

	<div class="form-group">
		<label for="StudentName">StudentName</label>
		<input class="input-validation-error form-control" data-val="true" data-val-required="Please enter student name." id="StudentName" name="StudentName" type="text" value="" />
	</div>

It will display list of error messages as a summary at the top of the page. Please make sure that you don't have a ValidationMessageFor method for each of the fields.

Example - ValidationSummary
---------------------------

	View
	----
	@model MVCWebApp.Models.Student

	<body>
	    @using (Html.BeginForm("Index", "Home", FormMethod.Post))
	    {
	        @Html.AntiForgeryToken()
	        <div class="form-horizontal">
	            <h4>Student</h4>
	            @Html.ValidationSummary(false, "", new { @class = "text-danger" })
	            <div class="form-group">
	                @Html.LabelFor(m => m.StudentFName)
	                @Html.TextBoxFor(m => m.StudentFName, new { @class = "form-control" })
	            </div>

	            <div class="form-group">
	                @Html.LabelFor(m => m.StudentLName)
	                @Html.TextBoxFor(m => m.StudentLName, new { @class = "form-control" })
	            </div>

	            <div class="form-group">
	                @Html.LabelFor(m => m.Age)
	                @Html.TextBoxFor(m => m.Age, new { @class = "form-control" })
	            </div>

	            <div class="form-group">
	                <div class="col-md-offset-2 col-md-10">
	                    <input type="submit" value="Create" class="btn btn-default" />
	                </div>
	            </div>
	        </div>
	    }
	</body>

	Model
	-----
	using System.ComponentModel.DataAnnotations;

    public class Student
    {
        public int StudentId { get; set; }
        [Required]
        public string StudentFName { get; set; }
        [Required]
        public string StudentLName { get; set; }
        [Required]
        public int Age { get; set; }
    }

	Controller
	----------
	using MVCWebApp.Models;

	public class HomeController : Controller
	{
	    public ActionResult Index()
	    {
	        return View("Index");
	    }

	    [HttpPost]
	    public ActionResult Index(Student std)
	    {
	        return View("Index");
	    }
	}

Custom Error Message
--------------------
Suppose we want to display a message if Student First Name is same as Last name then add custom errors message to 'ModelState'

	@model MVCWebApp.Models.Student

    @Html.ValidationSummary(false, "", new { @class = "text-danger" })
    <div class="form-group">
        @Html.LabelFor(m => m.StudentFName)
        @Html.TextBoxFor(m => m.StudentFName, new { @class = "form-control" })
    </div>

    <div class="form-group">
        @Html.LabelFor(m => m.StudentLName)
        @Html.TextBoxFor(m => m.StudentLName, new { @class = "form-control" })
    </div>

    =>

	<div class="validation-summary-errors text-danger" data-valmsg-summary="true">
		<ul>
			<li>The StudentFName field is required.</li>
			<li>The StudentLName field is required.</li>
			<li>The last name cannot be the same as the first name.</li>
			<li>The Age field is required.</li>
		</ul>
	</div>

	<div class="form-group">
		<label for="StudentFName">StudentFName</label>
		<input class="input-validation-error form-control" data-val="true" data-val-required="The StudentFName field is required." id="StudentFName" name="StudentFName" type="text" value="">
	</div>

	<div class="form-group">
		<label for="StudentLName">StudentLName</label>
		<input class="input-validation-error form-control" data-val="true" data-val-required="The StudentLName field is required." id="StudentLName" name="StudentLName" type="text" value="">
	</div>


Example - ValidationSummary Custom Error Message - model property-level error message
-------------------------------------------------------------------------------------

	View
	----
	@model MVCWebApp.Models.Student

	<body>
	    @using (Html.BeginForm("Index", "Home", FormMethod.Post))
	    {
	        @Html.AntiForgeryToken()
	        <div class="form-horizontal">
	            <h4>Student</h4>
	            @Html.ValidationSummary(false, "", new { @class = "text-danger" })
	            <div class="form-group">
	                @Html.LabelFor(m => m.StudentFName)
	                @Html.TextBoxFor(m => m.StudentFName, new { @class = "form-control" })
	            </div>

	            <div class="form-group">
	                @Html.LabelFor(m => m.StudentLName)
	                @Html.TextBoxFor(m => m.StudentLName, new { @class = "form-control" })
	            </div>

	            <div class="form-group">
	                @Html.LabelFor(m => m.Age)
	                @Html.TextBoxFor(m => m.Age, new { @class = "form-control" })
	            </div>

	            <div class="form-group">
	                <div class="col-md-offset-2 col-md-10">
	                    <input type="submit" value="Create" class="btn btn-default" />
	                </div>
	            </div>
	        </div>
	    }
	</body>

	Model
	-----
	using System.ComponentModel.DataAnnotations;

    public class Student
    {
        public int StudentId { get; set; }
        [Required]
        public string StudentFName { get; set; }
        [Required]
        public string StudentLName { get; set; }
        [Required]
        public int Age { get; set; }
    }

	Controller
	----------
	using MVCWebApp.Models;

	public class HomeController : Controller
	{
	    public ActionResult Index()
	    {
	        return View("Index");
	    }

        [HttpPost]
        public ActionResult Index(Student std)
        {
            
            if (std.StudentFName == std.StudentLName)
            {
                ModelState.AddModelError("StudentLName", "The last name cannot be the same as the first name.");
            }

            if (ModelState.IsValid)
            {
                return View("Index");
            }
            
            return View("Index");
        }
	}

Example - ValidationSummary Custom Error Message - model level error message
----------------------------------------------------------------------------

	View
	----
	@model MVCWebApp.Models.Student

	<body>
	    @using (Html.BeginForm("Index", "Home", FormMethod.Post))
	    {
	        @Html.AntiForgeryToken()
	        <div class="form-horizontal">
	            <h4>Student</h4>
	            @Html.ValidationSummary(true, "", new { @class = "text-danger" })
	            <div class="form-group">
	                @Html.LabelFor(m => m.StudentFName)
	                @Html.TextBoxFor(m => m.StudentFName, new { @class = "form-control" })
	            </div>

	            <div class="form-group">
	                @Html.LabelFor(m => m.StudentLName)
	                @Html.TextBoxFor(m => m.StudentLName, new { @class = "form-control" })
	            </div>

	            <div class="form-group">
	                @Html.LabelFor(m => m.Age)
	                @Html.TextBoxFor(m => m.Age, new { @class = "form-control" })
	            </div>

	            <div class="form-group">
	                <div class="col-md-offset-2 col-md-10">
	                    <input type="submit" value="Create" class="btn btn-default" />
	                </div>
	            </div>
	        </div>
	    }
	</body>

	Model
	-----
	using System.ComponentModel.DataAnnotations;

    public class Student
    {
        public int StudentId { get; set; }
        [Required]
        public string StudentFName { get; set; }
        [Required]
        public string StudentLName { get; set; }
        [Required]
        public int Age { get; set; }
    }

	Controller
	----------
	using MVCWebApp.Models;

	public class HomeController : Controller
	{
	    public ActionResult Index()
	    {
	        return View("Index");
	    }

        [HttpPost]
        public ActionResult Index(Student std)
        {
            
            if (std.StudentFName == std.StudentLName)
            {
                ModelState.AddModelError(string.Empty, "The last name cannot be the same as the first name.");
            }

            if (ModelState.IsValid)
            {
                return View("Index");
            }
            
            return View("Index");
        }
	}


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
---------------
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

Exception handling
------------------
Exception handling is the process of responding to the occurrence of exceptional conditions requiring special processing. There are two critical things that you need accomplish while handling error

	a) Gracefully handling errors and show users a friendly error page instead of standard yellow ASP.NET error screen.
	b) Logging errors so that you can take care of them

There are 6 ways of handling exceptions in ASP.NET MVC.

	a) Try-catch-finally
	b) Overriding OnException method
	c) Using the [HandleError] attribute on actions and controllers
	d) Setting a global exception handling filter
	e) Handling Application_Error event
	f) Extending HandleErrorAttribute
	g) Handling HTTP errors

Error Logging
-------------
Logging is a method of tracking/monitoring when an application is running. If something goes wrong in your application Logging will help to locate that error. We will use 'Log4Net' to do that. It has following methods

	a) Debug
	b) Information
	c) Warnings
	d) Error
	e) Fatal

Step01: Install 'Log4Net' from Tools->Nuget Package manager->Manage Nuget Package.
------

Step02: Add the below code to 'Web.config'.
------
	<configuration>
		<configSections>
			<section name="log4net" type="log4net.Config.Log4NetConfigurationSectionHandler, log4net" />
		</configSections>
		<log4net>
			<appender name="RollingLogFileAppender" type="log4net.Appender.RollingFileAppender">
				<file value="C:\\logfile.txt" />
				<appendToFile value="false" />
				<rollingStyle value="Size" />
				<maxSizeRollBackups value="-1" />
				<maximumFileSize value="50GB" />
				<layout type="log4net.Layout.PatternLayout">
					<conversionPattern value="%date [%thread] %-5level %logger [%property{NDC}] - %message%newline" />
				</layout>
			</appender>
			<root>
				<level value="DEBUG" />
				<appender-ref ref="RollingLogFileAppender" />
			</root>
		</log4net>
		....
	</configuration>

Step03: Add the below code to 'Global.asax'.
------
    protected void Application_Start()
    {
        AreaRegistration.RegisterAllAreas();
        log4net.Config.XmlConfigurator.Configure(); // This line
        RouteConfig.RegisterRoutes(RouteTable.Routes);
    }

Step04: Add the below code to your controller 'HomeController.cs'.
------
	using log4net;

	public class HomeController : Controller
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(HomeController));

		public ActionResult Index()
		{
			try
			{
				int i = 10;
				i = i / 0;
				return View();
			}
			catch (Exception ex)
			{
				log.Info("Info => " + ex);
				log.Fatal("Fatal => " + ex);
				log.Error("Error => " + ex);
				log.Warn("Warn => " + ex);
				log.Debug("Debug => " + ex);
				return View("Error", ex);
			}
		}
	}

Step05: Look for 'C:\\logfile.txt' to check the message you log.
------

Try-catch-finally
-----------------
Tradintional way of handling Exception. When exception happens catch block gets executed and it redirects to the error view. However, you can have multiple catch blocks for a try block. Even you can have Try-Catch blocks inside a Try block. In an actual project we can also use the catch block to log the error.

To do this, create error view under Views/Shared/Error.cshtml

	View - Error.cshtml
	-------------------
	@{
	    ViewBag.Title = "Error";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

	<h2>Error</h2>

	Controller
	----------
		using log4net;

	    public class HomeController : Controller
	    {
	        public ActionResult Index()
	        {
	            try
	            {
	                int i = 10;
	                i = i / 0;
	                return View();
	            }
	            catch (Exception ex)
	            {
	                log.Error("Error => " + ex);
	                return View("Error", ex);
	            }
	            finally
	            {

	            }
	        }
	    }

Overriding OnException method
-----------------------------
Handle controller level exceptions, does not require to enable the <customErrors> config in web.config. It handles all unhandled errors with error code 500. It also gives you the ability to log the errors. 'OnException' is a void method that takes an argument, ExceptionContext that has all the information about exception which can be used to log error. We can handle the exception generated from all the actions from a specific controller.

To do this, create two error view 

	a) Views/Shared/Error.cshtml and 
	b) Views/Error/InternalError.cshtml

	View - Error.cshtml
	-------------------
	@{
	    ViewBag.Title = "Error";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

	<h2>Error</h2>

	View - InternalError.cshtml
	---------------------------
	@{
	    ViewBag.Title = "InternalError";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

	<h2>InternalError</h2>


	Controller
	----------
	using log4net;

    public class HomeController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(HomeController));

        public ActionResult Index()
        {
            int i = 10;
            i = i / 0;
            return View();
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;

            // log the error using log4net.
            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];
            var statuscode = filterContext.HttpContext.Response.StatusCode;
            log.Error(filterContext.Exception.Message + "+" + controllerName + "+" + actionName + "+" + statuscode, filterContext.Exception);


            //Redirect to default error view. RedirectToAction(“ActionName”,”ControllerName”);
            filterContext.Result = RedirectToAction("ErrorHandler", "Home");

            // (OR)

            //return specific view
            filterContext.Result = new ViewResult()
            {
                ViewName = "~/Views/Error/InternalError.cshtml"
            };

            // (OR)

            //the above method can write this way
            ViewResult view = new ViewResult();
            view.ViewName = "~/Views/Error/InternalError.cshtml";
            filterContext.Result = view;
        }

        public ActionResult ErrorHandler()
        {
            return View("Error");
        }

    }

In the above scenarios, we have to handle the exception in every controller. To overcome this, we can create a BaseController class which will implement the Controller class and all controllers will implement Base Contoller class


	View - Error.cshtml
	-------------------
	@{
	    ViewBag.Title = "Error";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

	<h2>Error</h2>

	View - InternalError.cshtml
	---------------------------
	@{
	    ViewBag.Title = "InternalError";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

	<h2>InternalError</h2>


	Controller - BaseController
	---------------------------
	using log4net;

    public class BaseController : Controller
    {
    	private static readonly ILog log = LogManager.GetLogger(typeof(HomeController));

        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;

            // log the error using log4net.
            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];
            var statuscode = filterContext.HttpContext.Response.StatusCode;
            log.Error(filterContext.Exception.Message + "+" + controllerName + "+" + actionName + "+" + statuscode, filterContext.Exception);


            //Redirect to default error view. RedirectToAction(“ActionName”,”ControllerName”);
            filterContext.Result = RedirectToAction("ErrorHandler", "Home");

            // (OR)

            //return specific view
            filterContext.Result = new ViewResult()
            {
                ViewName = "~/Views/Error/InternalError.cshtml"
            };

            // (OR)

            //the above method can write this way
            ViewResult view = new ViewResult();
            view.ViewName = "~/Views/Error/InternalError.cshtml";
            filterContext.Result = view;
        }
    }

	Controller - HomeController
	---------------------------
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            int i = 10;
            i = i / 0;
            return View();
        }
 
        public ActionResult ErrorHandler()
        {
            return View("Error");
        }
    }

[HandleError] attribute on actions and controllers
--------------------------------------------------
HandleError attribute provides a built-in exception filter. It can be applied to an entire controller or individual action methods. It can only handle 500 level errors and it does not provide error logging.

Step01: Add <customErrors mode="On" ></customErrors> in 'web.config' under <system.web>
------

Step02: Decorate the action with [HandleError] as
------
    [HandleError]
    public ActionResult Index()
    {
        int i = 10;
        i = i / 0;
        return View();
    }

Create error view under Views/Shared/Error.cshtml

	View - Error.cshtml
	-------------------
	@{
	    ViewBag.Title = "Error";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

	<h2>Error</h2>

	Controller
	----------
    public class HomeController : Controller
    {
        [HandleError]
        public ActionResult Index()
        {
            int i = 10;
            i = i / 0;
            return View();
        }
    }

We can add it at controller level and it will be applicable to all the action methods present in the controller

	View - Error.cshtml
	-------------------
	@{
	    ViewBag.Title = "Error";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

	<h2>Error</h2>

	Controller
	----------
	[HandleError]
    public class HomeController : Controller
    {        
        public ActionResult Index()
        {
            int i = 10;
            i = i / 0;
            return View();
        }
        
        public ActionResult Div()
        {
            int i = 10;
            i = i / 0;
            return View();
        }

    }

We can handle a different exception with a different view

	View - Error.cshtml
	-------------------
	@{
	    ViewBag.Title = "Error";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

	<h2>Error</h2>

	View - MathRelatedError.cshtml
	------------------------------
	@{
	    ViewBag.Title = "Error";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

	<h2>Math Related Error</h2>

	View - InternalError.cshtml
	---------------------------
	@{
	    ViewBag.Title = "Error";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

	<h2>Internal Error</h2>

	Controller
	----------
    public class HomeController : Controller
    {
        [HandleError]
        [HandleError(ExceptionType = typeof(DivideByZeroException), View = "~/Views/Error/MathRelatedError.cshtml")]
        [HandleError(ExceptionType = typeof(ArgumentOutOfRangeException), View = "~/Views/Error/InternalError.cshtml")]
        public ActionResult Index()
        {
            int i = 10;
            i = i / 0;
            return View();
        }
    }

Setting a global exception handling filter
------------------------------------------
For this we need to ensure that 'HandleErrorAttribute' is added in 'RegisterGlobalFilters' of the 'FilterConfig' file of 'App_start' and registered in 'Application_Start'. No need to decorate our action and controller with [HandleError].

Step01: Add <customErrors mode="On" ></customErrors> in 'web.config' under <system.web>
------

Step02: Create 'App_Start\FilterConfig.cs' with below code
------
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}

Step03: Add 'FilterConfig.cs' in 'Global.asax'
------
    protected void Application_Start()
    {
        AreaRegistration.RegisterAllAreas();
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        RouteConfig.RegisterRoutes(RouteTable.Routes);
    }

Create error view under Views/Shared/Error.cshtml

	View - Error.cshtml
	-------------------
	@{
	    ViewBag.Title = "Error";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

	<h2>Error</h2>

	Controller
	----------
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            int i = 10;
            i = i / 0;
            return View();
        }
    }

Extending HandleErrorAttribute
------------------------------
We can also create our own Exception Handler by inheriting from HandleErrorAttribute

	View - InternalError.cshtml
	---------------------------
	@{
	    ViewBag.Title = "Error";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

	<h2>Internal Error</h2>

	Handler
	-------
	'MyExceptionHandler' should be inside the 'Controllers' folder

	    public class MyExceptionHandler: HandleErrorAttribute
	    {
	        private static readonly ILog log = LogManager.GetLogger(typeof(HomeController));

	        public override void OnException(ExceptionContext filterContext)
	        {
	            filterContext.ExceptionHandled = true;

	            // log the error using log4net.
	            var controllerName = (string)filterContext.RouteData.Values["controller"];
	            var actionName = (string)filterContext.RouteData.Values["action"];
	            log.Error(filterContext.Exception.Message + "+" + controllerName + "+" + actionName, filterContext.Exception);

	            //return specific view
	            filterContext.Result = new ViewResult()
	            {
	                ViewName = "~/Views/Error/InternalError.cshtml"
	            };

	            // (OR)

	            //the above method can write this way
	            ViewResult view = new ViewResult();
	            view.ViewName = "~/Views/Error/InternalError.cshtml";
	            filterContext.Result = view;
	        }
	    }

	Controller
	----------
    public class HomeController : Controller
    {
        [MyExceptionHandler]
        public ActionResult Index()
        {
            int i = 10;
            i = i / 0;
            return View();
        }
    }

Pass Exception, Controller Name and Method name to error view to display error page more informative.

	View - FormattedError.cshtml
	----------------------------
	@{
	    ViewBag.Title = "FormattedError";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

	<div>  
	    <h2>Formatted Error</h2>

		@model System.Web.Mvc.HandleErrorInfo  
		  
		@if (Model != null)  
		{  
		    <div>  
	            <h2>
	                @Model.Exception.Message in @Model.ControllerName and @Model.ActionName
	            </h2>  
		  
		    </div>  
		}  
	  
	</div>

	Handler
	-------
    public class MyExceptionHandler: HandleErrorAttribute
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(HomeController));

        public override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;

            // log the error using log4net.
            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];
            log.Error(filterContext.Exception.Message + "+" + controllerName + "+" + actionName, filterContext.Exception);

            var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

            //return specific view
            filterContext.Result = new ViewResult()
            {
                ViewName = "~/Views/Error/FormattedError.cshtml",
                ViewData = new ViewDataDictionary(model)
            };
        }
    }

	Controller
	----------
    public class HomeController : Controller
    {
        [MyExceptionHandler]
        public ActionResult Index()
        {
            int i = 10;
            i = i / 0;
            return View();
        }
    }

Handling HTTP errors
--------------------
We will handle HTTP errors like file not found 404, HTTP 500 error and other 4xx and 5xx

Step01: Add below code in 'web.config' under <system.web>
------
	  <customErrors mode="On" defaultRedirect="~/Error/Index">
		  <error statusCode="404" redirect="~/Error/NotFound"/>
	  </customErrors>

Step02: Create 'ErrorController'
------

Step03: Add 'Error.cshtml' and 'NotFound.cshtml' in 'Views/Shared'
------

Step04: Create 'HomeController' from where error occured first
------

	Web.config
	----------
	<system.web>
	  <customErrors mode="On" defaultRedirect="~/Error/Index">
		  <error statusCode="404" redirect="~/Error/NotFound"/>
	  </customErrors>
	  ...
	</system.web>


	View - Views/Shared/Error.cshtml
	--------------------------------
	@{
	    ViewBag.Title = "Error";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

	<h2>Error</h2>


	View - Views/Shared/NotFound.cshtml
	-----------------------------------
	@{
	    ViewBag.Title = "Error";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

	<div>
	    <h2>Not Found</h2>

	    @model System.Web.Mvc.HandleErrorInfo

	    @if (Model != null)
	    {
	        <div>
	            <h2>
	                @Model.Exception.Message in @Model.ControllerName and @Model.ActionName
	            </h2>

	        </div>
	    }
	</div>


	Controller - ErrorController
	----------------------------
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View("Error");
        }

        public ActionResult NotFound()
        {            
            return View("NotFound", new HandleErrorInfo(new HttpException(404, "page not found"), "ErrorController", "NotFound"));
        }
    }

	Controller - HomeController
	---------------------------
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            int i = 10;
            i = i / 0;
            return View();
        }
    }

Handling Application_Error event
--------------------------------
If you want to do global exception handling across your application, you can override the “Application_Error” event in “Global.asax”. If error handling is not done at the controller level “Application_Error” event can take care of it. You can log all unhandled exceptions that may occur within your application.

	Global.asax
	-----------
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error() 
        {
            var Exception = Server.GetLastError();
            Server.ClearError();
            Response.Redirect("Error"); //Views/Shared/Error.cshtml
        }
    }

Logging error across your application

	Global.asax
	-----------
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MvcApplication));

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            log4net.Config.XmlConfigurator.Configure();
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error() 
        {
            var Exception = Server.GetLastError();
            Server.ClearError();
            
            //Logging error
            log.Error(Exception.Message, Exception);

            //Redirecting to error page
            Response.Redirect("Error");
        }
    }

Layout View
-----------
The layout view is a common site template, which can be inherited in multiple views. It eliminates duplicate coding. Layout views are shared with multiple views, so it must be stored in the Shared folder.

	<!DOCTYPE html>
	<html>
	<head>
	    <meta charset="utf-8" />
	    <meta name="viewport" content="width=device-width, initial-scale=1.0">
	    <title>@ViewBag.Title - My ASP.NET Application</title>
	    <link href="~/Content/Site.css" rel="stylesheet" type="text/css" />
	    <link href="~/Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
	    <script src="~/Scripts/modernizr-2.8.3.js"></script>
	</head>
	<body>
	    <div class="navbar navbar-inverse navbar-fixed-top">
	        <div class="container">
	            <div class="navbar-header">
	                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
	                    <span class="icon-bar"></span>
	                    <span class="icon-bar"></span>
	                    <span class="icon-bar"></span>
	                </button>
	                @Html.ActionLink("Application name", "Index", "Home", new { area = "" }, new { @class = "navbar-brand" })
	            </div>
	            <div class="navbar-collapse collapse">
	                <ul class="nav navbar-nav">
	                </ul>
	            </div>
	        </div>
	    </div>

	    <div class="container body-content">
	        @RenderBody()
	        <hr />
	        <footer>
	            <p>&copy; @DateTime.Now.Year - My ASP.NET Application</p>
	        </footer>
	    </div>

	    <script src="~/Scripts/jquery-3.4.1.min.js"></script>
	    <script src="~/Scripts/bootstrap.min.js"></script>
	</body>
	</html>

RenderBody
----------
RenderBody() acts like a placeholder in 'Layout View'. 'Index.cshtml' will be injected and rendered in the layout view. 

We can set the layout view in multiple ways

	a) Using _ViewStart.cshtml 
	b) Setting Layout property in individual view 
	c) Specify Layout Page in ActionResult Method

Using _ViewStart.cshtml
-----------------------
'_ViewStart.cshtml' is included in the root of Views folder. '_ViewStart.cshtml' can also be included in sub folder of View folder to set the default layout page of that particular subfolder only.

	_ViewStart.cshtml
	-----------------
	@{
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

Setting Layout property in individual view
------------------------------------------
You can override default layout page set by _ViewStart.cshtml by setting Layout property in each individual view

	Index.cshtml
	------------
	@{
	    ViewBag.Title = "Index";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

    <div class="jumbotron">
        <h1>ASP.NET</h1>
        <p>ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS and JavaScript.</p>
    </div>

Specify Layout Page in ActionResult Method
------------------------------------------
You can specify layout page in action method using View() method

	HomeController
	--------------
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Index", "~/Views/Shared/_Layout.cshtml");
        }
    }

RenderSection
-------------
RenderSection separate content from layout, allows us to designate a place where content will be rendered and it is different from RenderBody().

	_Layout.cshtml
	--------------
	<!DOCTYPE html>
	<html>
	<head>
	    <meta charset="utf-8" />
	    <meta name="viewport" content="width=device-width, initial-scale=1.0">
	    <title>@ViewBag.Title - My ASP.NET Application</title>
	    <link href="~/Content/Site.css" rel="stylesheet" type="text/css" />
	    <link href="~/Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
	    <script src="~/Scripts/modernizr-2.8.3.js"></script>
	</head>
	<body>
	    <div class="navbar navbar-inverse navbar-fixed-top">
	        <div class="container">
	            <div class="navbar-header">
	                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
	                    <span class="icon-bar"></span>
	                    <span class="icon-bar"></span>
	                    <span class="icon-bar"></span>
	                </button>
	                @Html.ActionLink("Application name", "Index", "Home", new { area = "" }, new { @class = "navbar-brand" })
	            </div>
	            <div class="navbar-collapse collapse">
	                <ul class="nav navbar-nav">
	                </ul>
	            </div>
	        </div>
	    </div>

	    <div class="container body-content">
	        @RenderBody()
	        <hr />
	        <footer>
	            @RenderSection("footer", required: false)
	            <p>&copy; @DateTime.Now.Year - My ASP.NET Application</p>
	        </footer>
	    </div>

	    <script src="~/Scripts/jquery-3.4.1.min.js"></script>
	    <script src="~/Scripts/bootstrap.min.js"></script>	    
	</body>
	</html>

	Index.cshtml
	------------
	@{
	    ViewBag.Title = "Index";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

	<div class="jumbotron">
	    <h1>ASP.NET</h1>
	    <p>ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS and JavaScript.</p>
	</div>

	@section Footer
	{
	    <p>Section/Index page</p>
	}

Partial View
------------
Partial view is a reusable view. It can be used as partial view in the layout view, as well as other content views. You can render the partial view in the parent view (_Layout.cshtml) using html helper methods Partial() or RenderPartial() or Action() or RenderAction()

Partial or RenderPartial
------------------------
Seperate navigation bar code from '_Layout.cshtml' and save that code as partial view. To do that right click on Shared folder -> select Add -> click on View. In Add View dialogue, enter View name and select "Create as a partial view" checkbox and click Add.

+------------------------------------------------------------------------------+--------------------------------------------------------+
|                                Html.Partial()                                |                  Html.RenderPartial()                  |
+------------------------------------------------------------------------------+--------------------------------------------------------+
| Html.Partial returns html string.                                            | Html.RenderPartial returns void.                       |
| Html.Partial injects the html string of the partial view into the main view. | Html.RenderPartial writes html in the response stream. |
| Performance is slow.                                                         | Perform is faster compared with HtmlPartial().         |
| Html.Partial() need not to be inside the braces.                             | Html.RenderPartial must be inside braces @{ }.         |
+------------------------------------------------------------------------------+--------------------------------------------------------+

	_HeaderNavBar.cshtml
	--------------------
	<div class="navbar navbar-inverse navbar-fixed-top">
	    <div class="container">
	        <div class="navbar-header">
	            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
	                <span class="icon-bar"></span>
	                <span class="icon-bar"></span>
	                <span class="icon-bar"></span>
	            </button>
	            @Html.ActionLink("Application name", "Index", "Home", new { area = "" }, new { @class = "navbar-brand" })
	        </div>
	        <div class="navbar-collapse collapse">
	            <ul class="nav navbar-nav">
	            </ul>
	        </div>
	    </div>
	</div>

	_Layout.cshtml
	--------------
	<!DOCTYPE html>
	<html>
	<head>
	    <meta charset="utf-8" />
	    <meta name="viewport" content="width=device-width, initial-scale=1.0">
	    <title>@ViewBag.Title - My ASP.NET Application</title>
	    <link href="~/Content/Site.css" rel="stylesheet" type="text/css" />
	    <link href="~/Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
	    <script src="~/Scripts/modernizr-2.8.3.js"></script>
	</head>
	<body>
		
		@Html.Partial("_HeaderNavBar")

		(OR)

	    @{
	      Html.RenderPartial("_HeaderNavBar");   
	    }

	    <div class="container body-content">
	        @RenderBody()
	        <hr />
	        <footer>
	            @RenderSection("footer", required: false)
	            <p>&copy; @DateTime.Now.Year - My ASP.NET Application</p>
	        </footer>
	    </div>

	    <script src="~/Scripts/jquery-3.4.1.min.js"></script>
	    <script src="~/Scripts/bootstrap.min.js"></script>	    
	</body>
	</html>

Action or RenderAction
----------------------
Seperate 'Student registration form' from 'Index.cshtml' and save that code as partial view. To do that right click on Shared folder -> select Add -> click on View. In Add View dialogue, enter View name and select "Create as a partial view" checkbox and click Add.

+------------------------------------------------------------------------------+--------------------------------------------------------+
|                                Html. Action ()                               |                  Html. RenderAction ()                 |
+------------------------------------------------------------------------------+--------------------------------------------------------+
| Html.Action returns html string.                                             | Html.RenderAction returns void.                        |
| Html.Action injects the html string of the partial view into the main view.  | Html.RenderAction writes html in the response stream.  |
| Performance is slow.                                                         | Perform is faster compared with Action().              |
| Html.Partial() need not to be inside the braces.                             | Html. RenderAction must be inside braces @{ }.         |
+------------------------------------------------------------------------------+--------------------------------------------------------+

	_StudentForm.cshtml
	-------------------
	@model MVCWebApp.Models.Student

	@using (Html.BeginForm("Index", "Home", FormMethod.Post))
	{
	    @Html.AntiForgeryToken()
	    <div class="form-horizontal">
	        <h4>Student</h4>
	        @Html.ValidationSummary(false, "", new { @class = "text-danger" })
	        <div class="form-group">
	            @Html.LabelFor(m => m.StudentFName)
	            @Html.TextBoxFor(m => m.StudentFName, new { @class = "form-control" })
	        </div>

	        <div class="form-group">
	            @Html.LabelFor(m => m.StudentLName)
	            @Html.TextBoxFor(m => m.StudentLName, new { @class = "form-control" })
	        </div>

	        <div class="form-group">
	            @Html.LabelFor(m => m.Age)
	            @Html.TextBoxFor(m => m.Age, new { @class = "form-control" })
	        </div>

	        <div class="form-group">
	            <div class="col-md-offset-2 col-md-10">
	                <input type="submit" value="Create" class="btn btn-default" />
	            </div>
	        </div>
	    </div>
	}

	Index.cshtml
	------------
	@{
	    ViewBag.Title = "Index";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

	<div class="jumbotron">
	    <h1>ASP.NET</h1>
	    <p>ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS and JavaScript.</p>
	</div>
	@Html.Action("StudentForm")

	(OR)

	@{
	    Html.RenderAction("StudentForm");
	}  

	HomeController
	--------------
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult StudentForm()
        {
            return PartialView("_StudentForm");
        }
    }

    Model
    -----
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentFName { get; set; }
        public string StudentLName { get; set; }
        public int Age { get; set; }
    }

ViewBag
-------
If you want to send a small amount of temporary data to the view use 'ViewBag'. ViewBag only transfers data from controller to view, not visa-versa. ViewBag values will be null if redirection occurs. The ViewBag life only lasts during the current http request. 'ViewBag' doesn't require typecasting while retriving data from it.

Example01
---------
	Index.cshtml
	------------
	@{
	    ViewBag.Title = "Index";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

	<div class="jumbotron">
	    <h1>ASP.NET</h1>
	    <p>@ViewBag.name</p>
	</div>

	HomeController
	--------------
	public class HomeController : Controller
	{
	    public ActionResult Index()
	    {
	        ViewBag.name = "moin";
	        return View("Index");
	    }
	}

Example02
---------
	Index.cshtml
	------------
	@using MVCWebApp.Models
	@model MVCWebApp.Models.Student

    <div class="jumbotron">
        <h1>ASP.NET</h1>
        <p>@ViewBag.Student</p>
        <ul>
        	@* ViewBag doesn't require typecasting while retriving data from it *@

            @foreach (var std in ViewBag.Student) 
            {
                <li>
                    @std.StudentFName
                </li>
            }
        </ul>
    </div>

	HomeController
	--------------
    public class HomeController : Controller
    {
        public ActionResult Index()
        {            
            IList<Student> studentList = new List<Student>();
            studentList.Add(new Student() { StudentId = 1, StudentFName = "moin", StudentLName = "huq", Age = 37 });
            studentList.Add(new Student() { StudentId = 2, StudentFName = "tanim", StudentLName = "bhuiyan", Age = 37 });
            studentList.Add(new Student() { StudentId = 3, StudentFName = "shajib", StudentLName = "hassan", Age = 37 });
            
            //(OR)

            IList<Student> studentList = new List<Student>() {
                new Student() { StudentId = 1, StudentFName = "moin", StudentLName = "huq", Age = 37 },
                new Student() { StudentId = 2, StudentFName = "tanim", StudentLName = "bhuiyan", Age = 37 },
                new Student() { StudentId = 3, StudentFName = "shajib", StudentLName = "hassan", Age = 37 }
            };

            ViewBag.Student = studentList;
            return View("Index");
        }
    }

ViewData
--------
'ViewData' is similar to 'ViewBag'. 'ViewData' is a dictionary which can contain key-value pairs where each key must be string. 'ViewData' only transfers data from controller to view, not visa-versa. The 'ViewData' life only lasts during the current http request. 'ViewData' require typecasting while retriving data from it.

Example01
---------
	Index.cshtml
	------------
	@{
	    ViewBag.Title = "Index";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

	<div class="jumbotron">
	    <h1>ASP.NET</h1>
	    <p>@ViewData["name"]</p>
	</div>

	HomeController
	--------------
	public class HomeController : Controller
	{
	    public ActionResult Index()
	    {
	        ViewData["name"] = "moin";
	        return View("Index");
	    }
	}

Example02
---------
	Index.cshtml
	------------
	@using MVCWebApp.Models
	@model MVCWebApp.Models.Student

    <div class="jumbotron">
        <h1>ASP.NET</h1>
        <p>@ViewData["Student"]</p>
        <ul>
        	@* ViewData require typecasting while retriving data from it *@
        	
            @foreach (var std in ViewData["Student"] as IList<Student>)
            {
                <li>
                    @std.StudentFName
                </li>
            }
        </ul>
    </div>

	HomeController
	--------------
    public class HomeController : Controller
    {
        public ActionResult Index()
        {            
            IList<Student> studentList = new List<Student>();
            studentList.Add(new Student() { StudentId = 1, StudentFName = "moin", StudentLName = "huq", Age = 37 });
            studentList.Add(new Student() { StudentId = 2, StudentFName = "tanim", StudentLName = "bhuiyan", Age = 37 });
            studentList.Add(new Student() { StudentId = 3, StudentFName = "shajib", StudentLName = "hassan", Age = 37 });
            
            //(OR)

            IList<Student> studentList = new List<Student>() {
                new Student() { StudentId = 1, StudentFName = "moin", StudentLName = "huq", Age = 37 },
                new Student() { StudentId = 2, StudentFName = "tanim", StudentLName = "bhuiyan", Age = 37 },
                new Student() { StudentId = 3, StudentFName = "shajib", StudentLName = "hassan", Age = 37 }
            };

            ViewData["Student"] = studentList;
            return View("Index");
        }
    }

You can also add a 'KeyValuePair' into 'ViewData' as shown below

Example03
---------
	Index.cshtml
	------------
	@{
	    ViewBag.Title = "Index";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

    <div class="jumbotron">
        <h1>ASP.NET</h1>
        <p>@ViewData["Name"]</p>
        <p>@ViewData["Age"]</p>
    </div>

	HomeController
	--------------
    public class HomeController : Controller
    {
        public ActionResult Index()
        {            
            ViewData.Add(new KeyValuePair<string, object>("Name", "moin"));
            ViewData.Add(new KeyValuePair<string, object>("Age", 37));
            return View("Index");
        }
    }

'ViewData' and 'ViewBag' both use the same dictionary internally, So you cannot have 'ViewData Key' matches with the 'property name of ViewBag', otherwise it will throw a runtime exception.

	HomeController
	--------------
    public class HomeController : Controller
    {
        public ActionResult Index()
        {            
            ViewBag.Id = 1;
            ViewData.Add("Id", 1); // throw runtime exception as it already has "Id" key

            ViewData.Add(new KeyValuePair<string, object>("Name", "moin"));
            ViewData.Add(new KeyValuePair<string, object>("Age", 37));

            return View("Index");
        }
    }

TempData
--------
'TempData' can be used in subsequent request. 'TempData' will be cleared out after the completion of a subsequent request. It is useful when transferring data from one action method to another action method of the same or a different controller as well as redirects. It is dictionary type like 'ViewData'.

	Index.cshtml
	------------
	@{
	    ViewBag.Title = "Index";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

    <div class="jumbotron">
        <h1>ASP.NET</h1>
        <h3><u>Index</u></h3>
        <p>@TempData["Name"]</p>
        <p>@TempData["Age"]</p>

        <h3><u>About</u></h3>
        <p>@ViewBag.AboutName</p>
        <p>@ViewBag.AboutAge</p>

        <h3><u>Contact</u></h3>
        <p>@ViewBag.ContactName</p>
        <p>@ViewBag.ContactAge</p>
    </div>

	HomeController
	--------------
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            TempData["Name"] = "moin";
            TempData["Age"] = 37;

            return View("Index");
        }

        public ActionResult About()
        {
            ViewBag.AboutName = TempData["Name"];
            ViewBag.AboutAge = TempData["Age"];

            return View("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.ContactName = TempData["Name"];
            ViewBag.ContactAge = TempData["Age"];

            return View("Index");
        }
    }

When you request "https://localhost:44334/Home/Index", It will print the value of 'TempData["Name"]' and 'TempData["Age"]' then TempData will be cleared out. no data will be displayed on request of "https://localhost:44334/Home/About". To keep value of 'TempData' we can use 'TempData.Keep' inside 'Index' method so that we can get value of 'TempData' on request of "https://localhost:44334/Home/About". And we can do same for other function.

	HomeController
	--------------
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            TempData["Name"] = "moin";
            TempData["Age"] = 37;
			TempData.Keep();
            return View("Index");
        }

        public ActionResult About()
        {
            ViewBag.AboutName = TempData["Name"];
            ViewBag.AboutAge = TempData["Age"];
            TempData.Keep();
            return View("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.ContactName = TempData["Name"];
            ViewBag.ContactAge = TempData["Age"];

            return View("Index");
        }
    }

Example02
---------

	Index.cshtml
	------------
	@{
	    ViewBag.Title = "Index";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

    <div class="jumbotron">
        <h1>ASP.NET</h1>
    </div>

	HomeController
	--------------
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            TempData["Name"] = "moin";
            TempData["Age"] = 37;

            return View("Index");
        }

        public ActionResult About()
        {
            string userName;
            int userAge;

            if (TempData.ContainsKey("Name"))
                userName = TempData["Name"].ToString();

            if (TempData.ContainsKey("Age"))
                userAge = int.Parse(TempData["Age"].ToString());

            // do something with userName or userAge here 
            return View("Index");
        }
    }

TempData can be used to store only one time messages like error messages, validation messages

Filters
-------
If want to execute some logic before or after of an action method executes, you can use Filters. MVC provides different types of filters and must be implemented to create a custom filter class. Types of Filters in ASP.NET MVC and their Sequence of Execution

+-----------------------+---------------------------------------------------------------------------------------------------------------------+-----------------------------+---------------------------------------------+
|      Filter Type      |                                                     Description                                                     |       Built-in Filter       |                  Interface                  |
+-----------------------+---------------------------------------------------------------------------------------------------------------------+-----------------------------+---------------------------------------------+
| Authorization filters | Performs authentication and authorizes before executing action method.                                              | [Authorize], [RequireHttps] | IAuthorizationFilter, IAuthenticationFilter |
| Action filters        | Performs some operation before and after an action method executes.                                                 |                             | IActionFilter                               |
| Result filters        | Performs some operation before or after the execution of view result.                                               | [OutputCache]               | IResultFilter                               |
| Exception filters     | Performs some operation if there is an unhandled exception thrown during the execution of the ASP.NET MVC pipeline. | [HandleError]               | IExceptionFilter                            |
+-----------------------+---------------------------------------------------------------------------------------------------------------------+-----------------------------+---------------------------------------------+

Authentication VS Authorization
-------------------------------
Authentication and Authorization are separate concepts. Authentication is where a user provides credentials to access a resource, whereas authorization allows access to particular resources based on user’s role. Authentication filters run before Authorization filter. 

Authorization filters
---------------------
By default, all the action methods are accessible to both anonymous and authenticated users but, if you want the action methods to be available only for authenticated and authorized users, then you need to use the Authorization filters.

	web.config
	----------
	<system.web>
	  <authentication mode="Forms">
		  <forms loginUrl="/Home/Login"></forms>
	  </authentication>
	  ...
	</system.web>

	Index.cshtml
	------------
	@{
	    ViewBag.Title = "Index";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

	    <div class="jumbotron">
	        <h1>ASP.NET</h1>
	    </div>


	NonSecured.cshtml
	-----------------
	@{
	    ViewBag.Title = "NonSecured";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

	<h2>NonSecured</h2>

	Secured.cshtml
	--------------
	@{
	    ViewBag.Title = "Secured";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

	<h2>Secured</h2>

	Login.cshtml
	------------
	@{
	    ViewBag.Title = "Login";
	}

	<div>
	    @using (Html.BeginForm("Index", "Home", FormMethod.Post))
	    {
	        @Html.AntiForgeryToken()
	        <div class="form-horizontal">
	            <h2>Student Login</h2>
	            <div class="form-group">
	                @Html.TextBox("StudentName", null, new { @class = "form-control", placeholder = "User Name" })
	            </div>

	            <div class="form-group">                
	                @Html.Password("Password", null, new { @class = "form-control", placeholder = "Password" })
	            </div>

	            <div class="form-group">
	                <div class="col-md-offset-2 col-md-10">
	                    <input type="submit" value="Login" class="btn btn-default" />
	                </div>
	            </div>
	        </div>
	    }
	</div>

	HomeController.cs
	-----------------
	public class HomeController : Controller
	{
	    public ActionResult Index()
	    {
	        return View("Index");
	    }

	    [AllowAnonymous]
	    public ActionResult NonSecured()
	    {
	        return View("NonSecured");
	    }

	    [Authorize]
	    public ActionResult Secured()
	    {
	        return View("Secured");
	    }

	    public ActionResult Login()
	    {
	        return View("Login");
	    }
	}

If you want to use '[Authorize]' to the controller then system can give 401 error to overcome this problem go to the project property pane and change the ‘Window Authentication’ setting to ‘Enabled’.

	HomeController.cs
	-----------------
	[Authorize]
	public class HomeController : Controller
	{
	    public ActionResult Index()
	    {
	        return View("Index");
	    }

	    [AllowAnonymous]
	    public ActionResult NonSecured()
	    {
	        return View("NonSecured");
	    }

	    public ActionResult Secured()
	    {
	        return View("Secured");
	    }

	    public ActionResult Login()
	    {
	        return View("Login");
	    }
	}

Custom Authorization Filter
---------------------------
The Authorization Filters executed after the Authentication Filter. This filter is used to check whether the user has the rights to access the particular resource or page. The built-in AuthorizeAttribute and RequireHttpsAttribute are examples of Authorization Filters.

	public interface IAuthorizationFilter
	{
	    void OnAuthorization(AuthorizationContext filterContext);
	}

We are going to implement 'IAuthorizationFilter' below for Custom Authorization

	Login.cshtml
	------------
	@{
	    ViewBag.Title = "Index";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

	<div class="jumbotron">
	    <h1>Head</h1>
	</div>

	
	CustomAuthorize.cs
	------------------
	public class CustomAuthorize : AuthorizeAttribute, IAuthorizationFilter
	{
	    public string Permissions { get; set; }

	    public override void OnAuthorization(AuthorizationContext filterContext)
	    {
	        /*
	        var session = filterContext.HttpContext.Session;
	        var isLoggedIn = Convert.ToBoolean(session["loggedin"]);
	        var ControllerName = filterContext.Controller.GetType().Name;
	        var ActionName = filterContext.ActionDescriptor.ActionName;
	        */

	        var userName = filterContext.HttpContext.User.Identity.Name;
	        bool isAuthorized = CheckUserPermission(userName, Permissions);

	        if (!isAuthorized)
	        {
	            filterContext.Result = new HttpUnauthorizedResult();
	        }
	    }

	    private bool CheckUserPermission(string user, string permission)
	    {
	        //Check in Database for permission status
	        //var assignedPermissionsForUser = MockData.UserPermissions.Where(x => x.Key == userName).Select(x => x.Value).ToList();
	        var assignedPermissionsForUser = "Read";

	        // Page access permission from controller
	        var requiredPermissions = permission.Split(',');
	        
	        bool flag = false;

	        foreach (var x in requiredPermissions)
	        {
	            if (assignedPermissionsForUser.Contains(x))
	            {
	                flag = true;
	            }
	        }
	        return flag;
	    }
	}

	HomeController.cs
	-----------------
	public class HomeController : Controller
	{
	    [CustomAuthorize(Permissions = "Read")]
	    public ActionResult Index()
	    {
	        return View("Index");
	    }
	}

Authorization Filter
--------------------
ASP.NET MVC does not provide any built-in authentication filter. So, if you want to use authentication filter, then there is a way is to create a custom authentication filter.

Custom Authentication Filter
----------------------------
To create a custom authentication filter, we need to create a class by implementing the 'IAuthenticationFilter' Interface.

	public interface IAuthenticationFilter
	{
		void OnAuthentication (AuthenticationContext filterContext);

		void OnAuthenticationChallenge (AuthenticationChallengeContext filterContext);
	}

If there is any result set from 'OnAuthentication', then 'OnAuthenticationChallenge' will execute, after that if there is any other result set from 'OnAuthorization', then again 'OnAuthenticationChallenge' will execute.

	web.config
	----------
	<system.web>
	  <authentication mode="Forms">
		  <forms loginUrl="/Account/Login"></forms>
	  </authentication>
	  ...
	</system.web>

	Index.cshtml
	------------
	@{
	    ViewBag.Title = "Index";
	    Layout = "~/Views/Shared/_Layout.cshtml";
	}

    <div class="jumbotron">
	    <h2>Welcome you are loggedIn</h2>
	    @Html.ActionLink("LogOut", "LogOut", "Account")
    </div>

	Error.cshtml
	-----------------
    <div>
        <h2>You are not authorize to see this page.</h2>
        @Html.ActionLink("GoBack to login page", "Login", "Account")
    </div>

	Login.cshtml
	------------
	@model MyWebApp.Models.User

    <div>
        @using (Html.BeginForm("Login", "Account", FormMethod.Post))
        {
            @Html.AntiForgeryToken()
            <div class="form-horizontal">
                <h2>Student Login</h2>
                <div class="form-group">
                    @Html.TextBoxFor(m => m.UserName, new { @class = "form-control", placeholder = "User Name" })
                    @Html.ValidationMessageFor(m => m.UserName, "", new { @class = "text-danger" })
                </div>

                <div class="form-group">
                    @Html.PasswordFor(m => m.Password, new { @class = "form-control", placeholder = "Password" })
                    @Html.ValidationMessageFor(m => m.Password, "", new { @class = "text-danger" })
                </div>

                <div class="form-group">
                    <div class="col-md-offset-2 col-md-10">
                        <input type="submit" value="Login" class="btn btn-default" />
                    </div>
                </div>
            </div>
        }
    </div>

    User.cs
    -------
    public class User
	{
		public int ID { get; set; }
		[Required]
		public string UserName { get; set; }
		[Required]
		public string Password { get; set; }
	}

	HomeController.cs
	-----------------
	public class HomeController : Controller
	{
        [CustomAuthentication]
        public ActionResult Index()
        {
			return View("Index");
		}
	}

	Controllers/CustomAuthentication.cs
	-----------------------------------
    public class CustomAuthentication : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            bool isLoggedIn = Convert.ToBoolean(session["isLoggedIn"]);

            if (!isLoggedIn) {

                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                //Redirect to Login page

                //filterContext.Result = new RedirectToRouteResult( new RouteValueDictionary { { "controller", "Account" }, { "action", "Login" } } );

                //(OR)

                //Redirect to Error page

                filterContext.Result = new ViewResult { ViewName = "Error" };
            }
        }
    }

	AccountController.cs
	--------------------
	public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View ();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            // To Avoid NullReferenceException we can use user.UserName? and user.Password?
            if (user.UserName?.ToLower() == "admin" && user.Password?.ToLower() == "admin") {
                Session["isLoggedIn"] = "true";
                return RedirectToAction("Index", "Home");
            }
            return View("Login");
        }

        public ActionResult LogOut(User user)
        {
            Session["isLoggedIn"] = "false";
            return View("Login");
        }
    }
