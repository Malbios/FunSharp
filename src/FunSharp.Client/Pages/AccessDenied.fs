namespace FunSharp.Client

open Bolero.Html
open Radzen.Blazor

module AccessDenied =

    let page =
        
        comp<RadzenStack> {
            "Orientation" => Radzen.Orientation.Vertical
            "Gap" => "40"
            "JustifyContent" => Radzen.JustifyContent.Center
            "AlignItems" => Radzen.AlignItems.Center

            comp<RadzenCard> {
                comp<RadzenStack> {
                    "Orientation" => Radzen.Orientation.Vertical
                    "Gap" => "40"
                    "JustifyContent" => Radzen.JustifyContent.Center
                    "AlignItems" => Radzen.AlignItems.Center

                    comp<RadzenStack> {
                        "Gap" => "10"
                        "JustifyContent" => Radzen.JustifyContent.Center
                        "AlignItems" => Radzen.AlignItems.Center

                        comp<RadzenText> {
                            "TextStyle" => TextStyle.H3
                            "You don't have access to this page."
                        }
                        
                        comp<RadzenLink> {
                            "Path" => "/api/v1/account/signout"
                            "Text" => "Logout"
                        }

                        comp<RadzenText> {
                            "TextStyle" => TextStyle.Subtitle1
                            "Access Denied!"
                        }
                    }
                }
            }
        }
