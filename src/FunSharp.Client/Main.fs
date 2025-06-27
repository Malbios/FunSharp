module FunSharp.Client.Main

open System
open Elmish
open Bolero
open Bolero.Html
open FunSharp.Common
open Radzen.Blazor

type Main = Template<"wwwroot/main.html">
    
let currentTheme model =
    model.UserSettings.Theme |> Option.defaultWith (fun _ -> ThemeMode.Light)
    
let page model (dispatch: Message -> unit) =
    let testPageDispatch message =
        dispatch (Message.TestPageMessage message)
        
    Main()
        .Body(
            concat {
                comp<RadzenComponents>

                cond model.Page
                <| function
                    | Page.Root -> Root.page

                    | Page.NotFound -> NotFound.page
                    | Page.AccessDenied -> AccessDenied.page
                    
                    | Page.TestPage -> TestPage.view model.TestPage testPageDispatch
            }
        )
        .Theme(Union.toString (currentTheme model))
        .Elt()

let view model (dispatch: Message -> unit) =
    
    concat {
        page model dispatch

        match model.Error with
        | Some errorText ->
            div {
                attr.``class`` "error-wrapper"

                comp<RadzenAlert> {
                    attr.style "margin-bottom: 0.5rem; width: 99%;"
                    
                    "AlertStyle" => Radzen.AlertStyle.Danger
                    "Shade" => Radzen.Shade.Lighter
                    
                    errorText
                }
            }
        | None -> ()
    }

type ClientApplication() =
    inherit ProgramComponent<Model, Message>()

    let log (message: string) = Console.WriteLine message

    override _.CssScope = "FunSharp"

    override this.Program =

        let navigate navigationType url =
            let navigationManager = this.NavigationManager

            match navigationType with
            | Local -> navigationManager.NavigateTo(url, false, false)
            | External -> navigationManager.NavigateTo(url, true, false)

        let baseUrl = this.NavigationManager.BaseUri.TrimEnd('/')

        let getCurrentUrl = fun _ -> this.NavigationManager.Uri
        
        let update = ModelUpdate.update navigate getCurrentUrl

        let router =
            Router.infer Message.SetPage _.Page |> Router.withNotFound Page.NotFound

        log $"Serving client application from '{baseUrl}'"

        Program.mkProgram (fun _ -> ModelUpdate.initModel, Cmd.none) update view
        |> Program.withRouter router
