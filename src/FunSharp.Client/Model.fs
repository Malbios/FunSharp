namespace FunSharp.Client.Model

open FunSharp.Common
open FunSharp.Client

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
        HoverText: string
    }

    let initial = {
        HoverText = String.empty
    }

    type Message =
        | SetText of string
        | ClearText

type ClientState = {
    Page: Page
    Error: string option
    UserSettings: UserSettings
    TestPage: TestPage.Model
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
    }

[<RequireQualifiedAccess>]
type Message =
    | None
    | SetPage of Page
    | Error of exn
    | ClearError
    | SetThemeMode of ThemeMode
    | TestPageMsg of TestPage.Message
