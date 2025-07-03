namespace FunSharp.Client.Pages

open Bolero
open Bolero.Html
open Elmish
open FunSharp.Client
open FunSharp.Client.Model
open Microsoft.AspNetCore.Components
open Microsoft.AspNetCore.Components.Web
open Radzen
open Radzen.Blazor
open FunSharp.Client.Components
open FunSharp.Common

[<RequireQualifiedAccess>]
module TestPage =
    
    let gameStarted session =
        session
        |> TestPage.Message.GameStarted
        |> Message.TestPageMsg
        
    let gameError error =
        error
        |> TestPage.Message.GameError
        |> Message.TestPageMsg
    
    let update message (model: TestPage.Model) =
        match message with
        | TestPage.Message.SetText text -> { model with HoverText = text }, Cmd.none
        | TestPage.Message.ClearText -> { model with HoverText = String.empty }, Cmd.none
        | TestPage.Message.StartGame -> model, Cmd.OfAsync.either GameClient.startNewGame () gameStarted gameError
        | TestPage.Message.GameStarted session ->
            printfn $"GameStarted: {session.Id}"
            model, Cmd.none
        | TestPage.Message.GameError error ->
            printfn $"GameError: {error.Message}"
            model, Cmd.none

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
                
                comp<RadzenButton> {
                    attr.callback "Click" (fun (_: MouseEventArgs) -> dispatch TestPage.Message.StartGame)
                    
                    "Text" => "Click me!"
                }
            }
        }
