namespace FSharp_Todo_WebApi_Sample

open MongoDB.Driver

type TodoId = TodoID of int
type TodoText = TodoText of string

type Todo = {
    Id: TodoId
    Text: TodoText option
    IsComplete: bool
}

type TodoCollection = IMongoCollection<Todo>
type TodoSave = Todo -> Todo
type TodoDelete = TodoId -> bool
type TodoUpdate = Todo -> Todo
type TodoGetAll = Todo list
type TodoGetById = TodoId -> Todo
