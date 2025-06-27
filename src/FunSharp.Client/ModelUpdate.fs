namespace FunSharp.Client

open System
open Elmish
open FunSharp.Common

type NavigationType =
    | Local
    | External

module ModelUpdate =

    let initModel = {
        Page = Page.Root
        Error = None
        UserSettings = {
            Theme = Some ThemeMode.Dark
        }
        TestPage = {
            HoverText = String.empty
        }
    }

    let update (navigate: NavigationType -> string -> unit) getCurrentUrl message model =

        match message with
        | Message.SetPage page ->
            let model, cmd =
                match page with
                | Page.Root -> { model with Page = page }, Cmd.none
                | _ -> { model with Page = page }, Cmd.none

            let model = { model with Error = None }
            model, cmd
            
        | Message.Error(HttpException error) ->
            { model with Error = error |> HttpError.getMessage |> Some }, Cmd.none
            
        | Message.Error exn ->
            Console.WriteLine "an error occurred"
            { model with Error = Some exn.Message }, Cmd.none

        | Message.ClearError -> { model with Error = None }, Cmd.none

        | Message.SetThemeMode theme ->
            let settings = { model.UserSettings with Theme = Some theme }

            { model with UserSettings = settings }, Cmd.none
            
        | Message.TestPageMessage msg ->
            let testPageModel, cmd = TestPage.update msg model.TestPage
            
            { model with TestPage = testPageModel }, cmd

        | Message.None -> model, Cmd.none
