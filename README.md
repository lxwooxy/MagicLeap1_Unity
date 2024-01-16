# MagicLeap1_Unity

This repository contains the C# Unity scripts for the Magic Leap 1. Some are provided in the Magic Leap Unity Package, and others were written based on the Magic Leap documentation (https://ml1-developer.magicleap.com/en-us/learn/guides/unity-overview). 

Some of the documentation and the supporting video tutorials have discrepancies in the scripts or are no longer accessible, so I have modified them with much trial and error. I will use this README to document the changes that I have found to "work", or simply modifications I've made for my own apps as I make them. 


## Installations

I have found a precarious balance with Unity 2020.3.48f1 and Magic Leap Unity Package 0.26.0. which is what I am using to develop my Magic Leap 1 apps. 

- The Lab :  https://ml1-developer.magicleap.com/downloads 
    - Lumin SDK 0.26.0: https://creator.magicleap.com/downloads/lumin-sdk/overview
    - Zero Iteration: https://ml1-developer.magicleap.com/en-us/learn/guides/lab-zi

- Unity 2020.3.48f 1: https://unity3d.com/get-unity/download/archive
    - Unity MLTK: https://github.com/magicleap/Magic-Leap-Toolkit-Unity

- Magic Leap SDK: https://assetstore.unity.com/packages/tools/integration/magic-leap-sdk-for-unity-194780

## Scripts

### Dynamic Beam.cs 
- Use a `private MLInput.Controller` variable for the controller instead of a `public GameObject`;
- Get the position and rotation of the controller object with `.Position` and `.Orientation`;

### PlaceObject.cs

- Use a `private MLInput.Controller` variable for the controller instead of a `privateMLInputController`
- The change of the function name `MLInput.OnTriggerDown` instead of `MLInput.OnControllerButtonDown` is purely preference and not a neccessity.
- This functionality works with a world mesh with a mesh collider, or the objects will fall out of the world.

### MoveObject.cs
- A new script I wrote to manage moving objects, in conjunction with the `PlaceObject.cs` script.
- Using the bumper button while raycasting at an object will apply a force to the object. For this to work, the object must have a rigidbody component.

![](gifs/duckhit.gif)

### DestroyObject.cs
- A new script I wrote to manage destroying objects, in conjunction with the `PlaceObject.cs` script.
- Use the home button to destroy the object. After destroying an object, I save the tag of the destroyed object. The next time the home button is pressed while raycasting at a non object (the world mesh), every other object with the same tag will be destroyed.
- I did this because I wanted to be able to destroy multiple objects of the same type at once, and not just the one that is being raycasted at.

![](./gifs/duckdelete.gif)
### MeshingSnippet.cs
- Pressing the bumper button toggles the mesh, and the constructed mesh is instead represented with a transparent material (this can be set in the inspector).

### Playspace.cs
- I modified the script to allow for resetting the playing space, and to select corners with the raycast instead of the controller.
- For the raycast selection to work, I included the mesh collider in the world mesh object.
- While constructing the playing space and selecting the corners, pressing the bumper button will reset the selection. 
- After the playing space is constructed, pressing the home button will reset the playing space and return to the selection state.

### GestureScript.cs
- Modified to create the Cube game object when both hands are in the Fist pose, and deactivating when both are in the OK pose.
