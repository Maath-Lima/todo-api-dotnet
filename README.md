# dotnet-todo-api
A simple Todo REST api using .NET and Entity Framework

#### How to use:

Add item:  
`POST /api/TodoItems`  

Body (raw JSON):  
```json
{
    "id": 1,
    "name": "walk dog",
    "isComplete": true
}
```

Get all items:  
`GET /api/TodoItems/{id}`

Get Specific item by id:  
`GET /api/TodoItems/{id}`
