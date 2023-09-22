# To-Do-List-API
This is my WEB API project (on .Net 5) where users can register, login and add To-Do List.
## Details
After registering and entering the system, the user is assigned a validation token,
which allows it to perform the following operations: view its To-Do-List, add a new To-Do, change the IsCompleted status, 
change the added To-Do completely (PUT), delete the added To-Do.

### User Register Form:
/api/User/Register
{
"firstName" : "string",
"lastName" : "string",
"userName" : "string",
"password" : "string",
}

### User Login Form:
/api/User/Login
{
  "username": "string",
  "password": "string"
}

### ToDoList:
/api/ToDoList/Add-To-Do-List                      <=(Add To-Do)
{
  "title": "string",                              <=(To-Do Title/Name)
  "description": "string",                        <=(To-Do Description)
  "finalDate": "2023-09-22T10:14:38.665Z",        <=(Final Date for To-Do)
  "isCompleted": true,                            <=(Is completed to do? true or false)
  "category": "string"                            <=(You can choose one of them: Home, Personal, Work, Education, Travel, Other)
}
/api/ToDoList/GetOwn-To-Do-List                   <=(User can get his own To-Do-List)
/api/ToDoList/SortListByCategory                  <=(User can sort his own To-Do-List by Category by adding: ?category={categoryName})
​/api​/ToDoList​/SortByCompleted                     <=(User can sort his own To-Do-List by completed status by adding: ?completed=true/false)

/api/ToDoList/UpdateCompletedStatus               <=(User can change his own To-Do-List completed status by adding: ?todoId={id}):
{
"iscompleted" : true/false;
}

/api/ToDoList/UpdateToDoList                      <=(User can change his own To-Do-List by adding: ?todoId={id}):
{
"title": "string",
  "description": "string",
  "finalDate": "2023-09-22T10:22:44.094Z",
  "isCompleted": true,
  "category": "string"
}

/api/ToDoList/DeleteToDo                          <=(User can delete his own To-Do-List by adding: ?todoId={id})

## What I have made:
I created models: User, ToDoListModel. I added authentication, I used: Microsoft.AspNetCore.Authentication.JwtBearer, Microsoft.AspNetCore.Identity.EntityFrameworkCore. 
I have done validation for new user registration and also for To-Do-List addition. I used FluentValidator. I created a TokenGenerator service,
which, after logging in, generates a token that can be used to perform specific operations. I tentatively chose 365 days as the symbolic validation time.
I added a logging model to the project, which means that when a user logs in, this information is stored in the database, which user logged in, what role this user has,
and the date it was logged into the system. I wrote a HashSettings whose function is to pass a fixed password in a hashed state to SQL, for this I used the TweetinviAPI.
I performed the migration, connected my project to the database. For this I used: Microsoft.EntityFrameworkCore.SqlServer, Microsoft.EntityFrameworkCore.Tools, Microsoft.EntityFrameworkCore.
I've tested the project with Swagger and Postman and it works fine, returning all the status codes I'd expect. I wrote tests in the Nunit project, 
where I created FakeServices and tested one by one the services I have in the project: TokenGenerator, UserService, ToDoListService.
Everything works perfectly.
