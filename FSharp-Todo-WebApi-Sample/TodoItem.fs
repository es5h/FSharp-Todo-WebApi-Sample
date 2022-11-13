namespace FSharp_Todo_WebApi_Sample

type TodoItem =
    {
        Id: int
        Name: string option
        IsComplete: bool
    }