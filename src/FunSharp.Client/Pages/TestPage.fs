namespace FunSharp.Client

open Bolero.Html
open Elmish
open Radzen
open Radzen.Blazor
open FunSharp.Client.Components
open FunSharp.Common

[<RequireQualifiedAccess>]
module TestPage =
    
    type Model = {
        HoverText: string
    }
    
    type Message =
        | SetHoverText of string
        | ClearHoverText
        
    let update message model : Model * Cmd<Message>=
        match message with
        | SetHoverText text -> { model with HoverText = text }, Cmd.none
        | ClearHoverText -> { model with HoverText = String.empty }, Cmd.none
    
    let view model dispatch=

        comp<RadzenStack> {
            "Orientation" => Orientation.Vertical

            comp<RadzenStack> {
                "Orientation" => Orientation.Horizontal
                
                comp<HoverArea> {
                    "OnMouseOver" => fun () -> dispatch (SetHoverText "Item 1")
                    "OnMouseOut" => fun () -> dispatch ClearHoverText
                }

                comp<HoverArea> {
                    "OnMouseOver" => fun () -> dispatch (SetHoverText "Item 2")
                    "OnMouseOut" => fun () -> dispatch ClearHoverText
                }

                comp<HoverArea> {
                    "OnMouseOver" => fun () -> dispatch (SetHoverText "Item 3")
                    "OnMouseOut" => fun () -> dispatch ClearHoverText
                }
            }
            
            p { model.HoverText }
        }
