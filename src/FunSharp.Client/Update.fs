namespace FunSharp.Client

open System
open Elmish
open FunSharp.Abstraction
open FunSharp.Client.Model
open FunSharp.Common

module Update =

    let update (_: NavigationType -> string -> unit) _ message model =

        match message with
        | Message.SetPage page ->
            let model, cmd =
                match page with
                | Page.Root -> { model with Page = page }, Cmd.ofMsg (Message.SetPage Page.TestPage)
                | Page.TestPage ->
                    { model with Page = page }, Cmd.none
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
            
        | Message.TestPageMsg msg ->
            let subModel, cmd = Pages.TestPage.update msg model.TestPage
            
            { model with TestPage = subModel }, cmd

        | Message.None -> model, Cmd.none
