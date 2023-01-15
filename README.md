# BasicGUI

A simplistic GUI library loosely inspired by Godot's node graph system and GTK+'s object oriented system.
It's a work in progress, and has a looooong way to go.

FEATURES: <br>
- extremely extendable. <br>
- easily integrates into nearly any existing application

## Git structure
the BasicGUI folder contains the actual library itself. This is the folder you should copy into your C# project.
The folder by itself can be found at https://github.com/bluesillybeard/BasicGUI. It is a submodule of this git repo.
The library is designed to be independent, however integrating its sources into your project allows the best customizability.

VRenderExamples contains a series of examples that use VRender as the rendering backend. VRender is of course VRender itself.

## Getting Started

To start, you'll need BasicGUI.<br>

If you want to keep the library separate, then you have two options:
- locally publish the library on your computer
- Copy the BasicGUI folder next to the folder that your project is in.

Then, in your csproj file (or using the dotnet CLI) add a reference to BasicGUI.

Adding the sources into your project is easier, although can lead to messy code if you don't know what you are doing. To do this, simply copy the BasicGUI folder into your project and delete the .csproj file.

## Actually making it work

First, you need to create a class that implents the IDisplay interface. Each method is documented in the interface.<br>
If you're using VRender, there are pre-made display implementations that you can simply copy, 
however I would reccommend looking at the VRender examples since there are some more steps you must follow
in order to get text rendering to work.

Once you have implemented IDisplay, you can create your own test application. The simplest possible one where there is a single text element in the center is like so:

Somewhere during initialization:<br>
```
IDisplay display = new WhateverDisplayYouWantIGuess();
//make sure the plane is accessible in the main loop
BasicGUIPlane p = new BasicGUIPlane(windowWidth, windowHeight, display);
// The constructor of ALL nodes will add themself to the parent.
CenterContainer center = new CenterContainer(p.GetRoot());
// The text element uses the display in order to determine its bounds
TextElement text = new TextElement(center, color, fontSize, text, font, display);
```
<br>
Then, during updates (when the program takes input and the like)

```
//This makes sure that the GUI root has the same size as the window / rendering area
p.SetSize(windowWidth, windiwHeight)
//the Iterate method places the elements.
p.Iterate();
```
<br>
And when it's time to draw the GUI:

```
//recursively draws all of the GUI items to the screen.
p.Draw();
```
