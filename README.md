# Holoplan
The goal of our project was to evaluate the usability of a prototype that serves as an interactive visualization of time-related dependencies between building sites. Those were displayed on up to three VR maps, which represented three different time levels. The visualization was developed for the city planning project iPlanB. 
With this first prototype we explored the possibility to facilitate planning processes by using the Oculus Rift VR headset and its interaction and visualization enhancements (e.g., hand gestures, 3rd dimension). 
Users can move, scale and rotate the maps using hand gestures and select sites by touching them with the index finger to display information on dependent sites. 
  
## Hardware 
We used the Oculus Rift in combination with two touch controller and two tracking sensors 
## Software 
We create your prototype in Unity (Version 2018.2.10f). 
For the interactions with the controller, we worked with the OVR API to detect if special buttons are pressed. 
In the implementation we used the convention to start public field names with kapitel letter and private fields with an underscore.
## Structure 
We used the OVRPlayerController, witch is a prefab out of the oculus integration asset. 
This prefab includes a camera and the handavatars. 
We attached following scripts to components of the prefab
```
OVRCameraRig – Activate Sites, VR Manipulation, Change GUI
LeftHandAnchor – VR Manipulative (with LeftCube – Detector) 
RightHandAnchor – VR Manipulative (with RightCube - Detector)
CenterEyeAnchor – GUI Swap
```
All sites has an instance of the site script, but just the sites on the middle map has connection as children and the tag "site" whitch enable the manipulation <br />
We add the left and right cubes to detect collisions with the sites.
We used an mapAnchor for the rotation and to change the material of the maps (ScaleChangesMap script). The parentMap allows use to manipulate all maps synchronously <br />
Names of the sites are random nummerated or named like the street
All cubes and connections are placed free hand (not based on a databank) <br />
For our studie we used a testside without connections (while deactivate all other sites on the middle map) to give the participant the option du explore the interactions without to much input <br /><br />
*for more information read the comments in the scripts* 
### last Update 28.01.2019
### Contact 
Sina Haselmann (6haselma@informatik.uni-hamburg.de) //
Joschka Eickhoff (5eickhof@informatik.uni-hamburg.de) //
Lea Steep (6steep@informatik.uni-hamburg.de) 
