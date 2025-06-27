namespace FunSharp.Client 

open Bolero

[<RequireQualifiedAccess>]
type Page =
    | [<EndPoint "/">] Root
    | [<EndPoint "/test-page">] TestPage
    | [<EndPoint "/invalidUrl">] NotFound
    | [<EndPoint "/accessDenied">] AccessDenied
