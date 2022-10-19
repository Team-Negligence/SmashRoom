# SmashRoom
VR Smash Room by Team Negligence

# Compiling for Meta Quest Series
## Prerequisites
1. Download and install [Unity Hub](https://unity3d.com/get-unity/download)
2. Install Unity 2021.3.8f1 through the hub. Make sure to enable **Android Build Support** on the additional export modules page.
3. Follow [Oculus' instructions](https://developer.oculus.com/documentation/native/android/mobile-device-setup/) on enabling development mode on the Meta Quest device.
4. On Windows, install the Quest desktop software. Not necessary on Linux.

## Compiling
1. Import the project into Unity Hub, and open it.
2. Open Edit > Project Settings, and change to the XR Plug-in Management page on the left.
3. Switch to the Android tab, and verify that OpenXR is checked. If it is not, make sure to check the box, click on the resulting ⚠️ and select 'edit'. Finally click '+' and enable Oculus Quest support.
4. Close Project Settings.
5. Open File > Build Settings (Ctrl+Shift+B).
6. Verify that scenes are present in the top pane, and that the MainMenu is the top scene.
7. Select Android in the left pane, and then click 'Switch Platform'. This step may take a few minutes.
8. Plug in Meta Quest, and select 'yes' on its USB debugging prompt.
9. Select Build and Run. In the future, you can simply press File > Build (Ctrl+B) to compile and upload to the headset.


# Compiling for Windows
## Prerequisites
1. Download and install [Unity Hub](https://unity3d.com/get-unity/download)
2. Install Unity 2021.3.8f1 through the hub.
4. On Windows, install the relevant VR software and launch it. (Oculus software, Steam VR, etc)

## Compiling
1. Import the project into Unity Hub, and open it.
5. Open File > Build Settings (Ctrl+Shift+B).
6. Verify that scenes are present in the top pane, and that the MainMenu is the top scene.
7. Select Windows, Mac, Linux in the left pane if not already selected, and then click 'Switch Platform'. This step may take a few minutes.
8. Plug in your VR device.
9. Select Build, and select an appropriate output directory.
10. Once built, run SmashRoom.exe