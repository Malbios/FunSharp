module FunSharp.Server.Index

open Bolero.Html
open Bolero.Server.Html
open FunSharp

let page =
    doctypeHtml {
        head {
            meta { attr.charset "UTF-8" }
            meta { attr.name "viewport"; attr.content "width=device-width, initial-scale=1.0" }

            title { "FunSharp" }
            
            ``base`` { attr.href "/" }

            link { attr.rel "shortcut icon"; attr.href "assets/favicon.ico" }

            link { attr.rel "stylesheet"; attr.href "_content/Radzen.Blazor/css/material-base.css" }
            link { attr.rel "stylesheet"; attr.href "https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" }

            script { attr.src $"_content/Radzen.Blazor/Radzen.Blazor.js?v={typedefof<Radzen.Colors>.Assembly.GetName().Version}" }

            link { attr.rel "stylesheet"; attr.href "css/global.css" }
        }

        body {
            div {
                attr.id "main"
                comp<Client.Main.ClientApplication>
            }

            boleroScript
        }
    }
