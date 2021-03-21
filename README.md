# Setting up database:

To set up the database download the repo on your local machine, and open it using Visual Studio.
Once in Visual Studio open the **Package Manager Console** (Tools **>** NuGet Package Manager **>** Package Manager Console)

Execute the following commands within the package manager console:
<pre>
PM> <b>Script-Migration</b>
PM> <b>Update-Database</b>
</pre>

The databse will be populated when you run the program for the first time, this will take some time. 
Note: There will be a number of exceptions thrown while the database is populated, fear not! this is normal.

# Work to do

## Implementing pages

1. Main View
2. Filters View
3. New Task
4. Login/Sign-up
5. My Tasks
6. Profile
7. TaskView - Owner
8. TaskView - Grabber
9. Tasks in communities

### 1. Main View

Location: `Pages/MainView/`

The main view consists of the map view and list view, this have already been implemented.

### 2. Filters View

Location: `Pages/FiltersPage.xaml`

Consists of:
-  Communities/distance
-  Categories
-  Price Range

Applied filters should be reflected in the displayed tasks

### 3. New Task

Location: `Pages/NewTaskPage.xaml`

Consists of a form where the user can fill out the required information and proceed with posting of the task.

### 4. Login/Signup

Location: `Pages/Login.xaml` & `Pages/Signup.xaml`

Consists of login and signup forms. 

### 5. My Tasks

Location: `Pages/MyTasks.xaml` 

Consists of:
- My Posted tasks
- My Saved Tasks

### 6. Profile

Location: `Pages/Profile.xaml`

Consists of: 
- User info
- Logout button

### 7. TaskView - Owner

Location: `Pages/TaskView/Owner.xaml`

Parameters: 
- `id -> the id of the task`
- `view -> what view to render (chat or info)`

Consists of:
- List of people who've meessage the task owner regarding the task
- Chat view 
- Information view
- Edit view

### 8. TaskView - Grabber

Location: `Pages/TaskView/Grabber.xaml`

Parameters: 
- `id -> the id of the task`
- `view -> what view to render (chat or info)`

Consits of:

- Info view
- Chat view

### 9. Tasks in communities:

Location: `Pages/CommunityTasks.xaml`

Parameters: `location -> the community requested`

Consists of:

List of tasks in specified community.

# Utilities

The project contains two utitilities thus far, `History` and `QueryString`

## History

The history utility allows easy navigation.

`history.GoTo(string path)`

 Go to the specified page. Path is relative to the project root, example: 

`history.GoTo("Pages/TaskView/Grabber.xaml")`

Parameters can be passed by way of url encoding, ex:

`history.GoTo("Pages/TaskView/Grabber.xaml?id=2&view=chat")`

`history.Replace(string path)`

Works like `history.GoTo` except that the given path replaces the current path in the history.

## QueryString

Allows parsing of url parameters

Getting specific parameter:

```
string id;
bool success = query_string.TryGetValue("id", out id);
```

# Skeletons

Each page has already been created and populated with helper fields:

- `MainWindow main` > Access the main window 
- `History history` > Access the history object
- `QueryString query_string` > Access the query parameters passed to the page

# Access Task Data

To acces the tasks database use the following:

```
TaskGrabContext task_grab_context = new TaskGrabContext();
DbSet<Data.Task> tasks = task_grab_context.Tasks;
```

Task data is defined in `Data/Task.cs`

To add content to database:

```
 task_grab_context.Tasks.Add(task);
 task_grab_context.SaveChanges();
```

# Helpful Links

- https://docs.microsoft.com/en-us/dotnet/?view=entity-framework-6.2.0
- https://docs.microsoft.com/en-us/dotnet/api/system.data.entity.dbcontext?view=entity-framework-6.2.0
- https://docs.microsoft.com/en-us/dotnet/api/system.data.entity.dbset?view=entity-framework-6.2.0