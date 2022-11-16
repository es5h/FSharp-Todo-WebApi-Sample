// Service

module FSharp_Todo_WebApi_Sample.TodoDb

open Microsoft.Extensions.DependencyInjection
open MongoDB.Driver

let getAll (collection: TodoCollection) : Todo list =
    collection.Find(fun _ -> true).ToEnumerable() |> List.ofSeq

let getById (collection: TodoCollection) (id: TodoId) : Todo =
    collection.Find(fun x -> x.Id = id).FirstOrDefault()

let save (collection: TodoCollection) (todo: Todo) : Todo =
    collection.InsertOne todo
    todo

let update (collection: TodoCollection) (todo: Todo) : Todo =
    let condition = Builders<Todo>.Filter.Eq((fun x -> x.Id), todo.Id)
    let data = Builders<Todo>.Update
                .Set((fun x -> x.Text), todo.Text)
                .Set((fun x -> x.IsComplete), todo.IsComplete)
    collection.UpdateOne(condition, data) |> ignore
    todo

let delete (collection: TodoCollection) (id: TodoId): bool =
    collection.DeleteOne(Builders<Todo>.Filter.Eq((fun x -> x.Id), id)).DeletedCount > 0
    
type IServiceCollection with
    member this.AddTodoDb (collection: TodoCollection) =
        this.AddSingleton<TodoSave>(save collection) |> ignore
        this.AddSingleton<TodoDelete>(delete collection) |> ignore
        this.AddSingleton<TodoUpdate>(update collection) |> ignore
        this.AddSingleton<TodoGetAll>(getAll collection) |> ignore
        this.AddSingleton<TodoGetById>(getById collection) |> ignore