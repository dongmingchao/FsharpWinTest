// Copyright 2018-2019 Fabulous contributors. See LICENSE.md for license.
namespace SqueakyApp

open System.Diagnostics
open Fabulous
open Fabulous.XamarinForms
open Fabulous.XamarinForms.LiveUpdate
open Xamarin.Forms

module App =
    /// The messages dispatched by the view
    type Msg =
        | Pressed

    /// The model from which the view is generated
    type Model =
        { Pressed: bool }

    /// Returns the initial state
    let init() = { Pressed = false }, Cmd.none

    /// The function to update the view
    let update (msg: Msg) (model: Model) =
        match msg with
        | Pressed -> { model with Pressed = true }, Cmd.none

    /// The view function giving updated content for the page
    let view (model: Model) dispatch =
        View.ContentPage(
            content=View.StackLayout(
                children=[
                    if model.Pressed then
                        yield View.Label(text="I was pressed!", backgroundColor = Color.Red)
                    else
                        yield View.Button(text="Press Me!", command=(fun () -> dispatch Pressed))
                ]
            )
        )
    let program = XamarinFormsProgram.mkProgram init update view

type App () as app = 
    inherit Application ()

    let runner = 
        App.program
#if DEBUG
        |> Program.withConsoleTrace
#endif
        |> XamarinFormsProgram.run app
