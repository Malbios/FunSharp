namespace FunSharp.Client

type ThemeMode =
    | Light
    | Dark

type UserSettings = {
    Theme: ThemeMode option
}

type Model = {
    Page: Page
    Error: string option
    UserSettings: UserSettings
    TestPage: TestPage.Model
}
