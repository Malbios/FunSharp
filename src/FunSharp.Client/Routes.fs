namespace FunSharp.Client 

open Bolero

[<RequireQualifiedAccess>]
type Page =
    | [<EndPoint "/">] Root
    | [<EndPoint "/invalidUrl">] NotFound
    | [<EndPoint "/accessDenied">] AccessDenied
