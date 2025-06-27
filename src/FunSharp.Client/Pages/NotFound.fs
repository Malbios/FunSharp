namespace FunSharp.Client

open Bolero.Html
open Radzen.Blazor

module NotFound =

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
                            "Not found"
                        }

                        comp<RadzenText> {
                            "TextStyle" => TextStyle.Subtitle1
                            "Oooops! This page does not exist!"
                        }
                    }
                }
            }
        }
