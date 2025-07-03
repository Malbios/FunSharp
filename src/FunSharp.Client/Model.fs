namespace FunSharp.Client.Model

open FunSharp.Common
open FunSharp.Client
open FunSharp.Model

type NavigationType =
    | Local
    | External

type ThemeMode =
    | Light
    | Dark

type UserSettings = {
    Theme: ThemeMode option
}

module TestPage =

    type Model = {
        TestText: string
        HoverText: string
    }

    let initial = {
        TestText = String.empty
        HoverText = String.empty
    }

    type Message =
        | SetText of string
        | ClearText
        | StartGame
        | GameStarted of Game.Session
        | GameError of exn

type ClientState = {
    Page: Page
    Error: string option
    UserSettings: UserSettings
    TestPage: TestPage.Model
    Test: string
}

[<RequireQualifiedAccess>]
module ClientState =
    let initial = {
        Page = Page.Root
        Error = None
        UserSettings = {
            Theme = Some ThemeMode.Dark
        }
        TestPage = TestPage.initial
        Test = String.empty
    }

[<RequireQualifiedAccess>]
type Message =
    | None
    | SetPage of Page
    | Error of exn
    | ClearError
    | SetThemeMode of ThemeMode
    | TestPageMsg of TestPage.Message
