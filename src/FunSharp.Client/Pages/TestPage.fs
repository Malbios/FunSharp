namespace FunSharp.Client.Pages

open Bolero
open Bolero.Html
open Elmish
open FunSharp.Client.Model
open Radzen
open Radzen.Blazor
open FunSharp.Client.Components
open FunSharp.Common

[<RequireQualifiedAccess>]
module TestPage =

    let update message (model: TestPage.Model) =
        match message with
        | TestPage.Message.SetText text -> { model with HoverText = text }, Cmd.none
        | TestPage.Message.ClearText -> { model with HoverText = String.empty }, Cmd.none

    let view (model: TestPage.Model) dispatch : Node =

        div {
            attr.``class`` "center-wrapper"

            comp<RadzenStack> {
                "Orientation" => Orientation.Vertical

                comp<RadzenStack> {
                    "Orientation" => Orientation.Horizontal

                    comp<HoverArea> {
                        "OnMouseOver" => fun () -> dispatch (TestPage.Message.SetText "Item 1")
                        "OnMouseOut" => fun () -> dispatch TestPage.Message.ClearText
                    }

                    comp<HoverArea> {
                        "OnMouseOver" => fun () -> dispatch (TestPage.Message.SetText "Item 2")
                        "OnMouseOut" => fun () -> dispatch TestPage.Message.ClearText
                    }

                    comp<HoverArea> {
                        "OnMouseOver" => fun () -> dispatch (TestPage.Message.SetText "Item 3")
                        "OnMouseOut" => fun () -> dispatch TestPage.Message.ClearText
                    }
                }

                div {
                    attr.``class`` "center-text"
                    
                    cond (System.String.IsNullOrWhiteSpace model.HoverText) <| function
                        | true -> p { "<hover over a tile>" }
                        | false -> p { model.HoverText }
                }
            }
        }
