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

	@Html.Password("OnlinePassword")

	=> 

	<input id="OnlinePassword" name="OnlinePassword" type="password">

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
