namespace FunSharp.Model

open System

type FunSharpError =
    | Unknown of message: string
    | NoGameSession of id: Guid
