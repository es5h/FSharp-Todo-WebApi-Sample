open System
open FSharp_Todo_WebApi_Sample
open FSharp_Todo_WebApi_Sample.TodoDb
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe
open MongoDB.Driver

let routes =
    choose [
        route "/" >=> text "Hello World from F#!"
    ]

let configureApp (app : IApplicationBuilder) =
    app.UseGiraffe routes

let configureServices (services : IServiceCollection) =
    let mongo = MongoClient ("mongodb://localhost:27017")
    let db = mongo.GetDatabase "todos"
  
    services.AddTodoDb(db.GetCollection<Todo>("todos"))
    services.AddGiraffe() |> ignore

[<EntryPoint>]
let main _ =
    WebHostBuilder()
        .UseKestrel()
        .Configure(Action<IApplicationBuilder> configureApp)
        .ConfigureServices(configureServices)
        .Build()
        .Run()
    0