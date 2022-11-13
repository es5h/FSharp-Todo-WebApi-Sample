namespace FSharp_Todo_WebApi_Sample.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open FSharp_Todo_WebApi_Sample

[<ApiController>]
[<Route("api/[controller]")>]
type TodoItemsController (logger : ILogger<TodoItemsController>) =
    inherit ControllerBase()

    [<HttpGet("Test")>]
    member _.Get() =
        "Hello, World"