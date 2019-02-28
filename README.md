# Holoplan
The goal of our project was to evaluate the usability of a prototype that serves as an interactive visualization of time-related dependencies between building sites. Those were displayed on up to three VR maps which represent three different time levels. The visualization was developed for the city planning project iPlanB. 
With this first prototype we explored the possibility to facilitate planning processes by using the Oculus Rift VR headset and its interaction and visualization enhancements (e.g., hand gestures, 3rd dimension). 
Users can move, scale and rotate the maps using hand gestures and select sites by touching them with their index finger to display information on dependent sites. 
  
## Hardware 
We used the Oculus Rift in combination with two touch controllers and two tracking sensors 
## Software 
We created your prototype in Unity (version 2018.2.10f). 
For the interactions with the controller we worked with the OVR API to detect if buttons on the controllers are pressed. 
In the implementation we used the convention to start public class variable names with capital letter and private class variables with an underscore.<br /><br />
Use the Unity project to further develop the software or open the build version in the top folder to execute the application.
## Structure 
We used the OVRPlayerController, which is a prefab of the Oculus integration asset. 
This prefab includes a camera and the hand avatars. 
We attached the following scripts to components of the prefab
```
OVRCameraRig – ActivateSites, VRManipulation, ChangeGUI
LeftHandAnchor – VRManipulative (with LeftCube Detector) 
RightHandAnchor – VRManipulative (with RightCube Detector)
CenterEyeAnchor – GUISwap
```
All sites have an instance of the site script, but only the sites on the middle map have connections as child components and the tag "site" which enable activation. <br />
We added the cubes to the left and right hand anchors to detect collisions with the sites.
We used a mapAnchor for the rotation and for changing the material of the maps (ScaleChangesMap script). The parentMap allows synchronous manipulation of all maps. <br />
The names of the sites are random numbers or street names and have no specific meaning.
All cubes and connections are placed manually (not based on a database). <br />
<br />
*for more information read the comments in the scripts* 
### Contact 
Sina Haselmann (6haselma@informatik.uni-hamburg.de) //
Joschka Eickhoff (5eickhof@informatik.uni-hamburg.de) //
Lea Steep (6steep@informatik.uni-hamburg.de) 
#### last Update 28.01.2019
