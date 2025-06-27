namespace FunSharp.Client 

[<RequireQualifiedAccess>]
type Message =
    | None
    | SetPage of Page
    | Error of exn
    | ClearError
    | SetThemeMode of ThemeMode
