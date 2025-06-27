namespace FunSharp.Client.Components

open Bolero
open Bolero.Html
open Microsoft.AspNetCore.Components

type HoverArea() =
    inherit ElmishComponent<unit, unit>()
    
    [<Parameter>]
    member val Width: string = "50px" with get, set
    [<Parameter>]
    member val Height: string = "50px" with get, set
    
    [<Parameter>]
    member val OnMouseOver: unit -> unit = ignore with get, set
    [<Parameter>]
    member val OnMouseOut: unit -> unit = ignore with get, set

    override this.View _ _ =
        div {
            attr.``class`` "hover-area"
            attr.style $"width: {this.Width}; height: {this.Height};"
            
            on.mouseover (fun _ -> this.OnMouseOver ())
            on.mouseout (fun _ -> this.OnMouseOut ())
        }
