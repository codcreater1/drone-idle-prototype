# Drone Idle Prototype

A small 3D idle simulation made for the iNFORMERS internship task.
Scout drone scans the map and reveals fog. Gatherer drones collect
resources and bring them back to base.

## Requirements

- Unity **6 LTS (6000.4.5f1)** — same version the project was made in.
  Other 6.x versions should also open it but might re-import assets.
- Windows / Mac / Linux. Tested on Windows 11.

## How to run from source

1. Clone the repo or download the zip.
2. Open Unity Hub → **Add project from disk** → pick the
   `DroneIdlePrototype` folder.
3. Open the project (Unity will compile scripts and import assets the
   first time, this can take a few minutes).
4. In the Project window go to `Assets/MainScene.unity` and double-click
   to load the scene.
5. Press **Play** (the triangle button at the top of the editor).

## How to run the build

If a build is provided in `Builds/Windows/`:

1. Unzip the build folder somewhere on disk.
2. Run `DroneIdlePrototype.exe`.
3. To quit, press `Alt+F4` or close the window.

## Controls

| Action | Input |
|--------|-------|
| Move scout drone | Left-click on the ground |
| Send a gatherer to a resource | Left-click on a resource cube |
| Pan camera | `WASD` or arrow keys |
| Zoom camera | Mouse scroll wheel |

## What you should see

- A flat 10x10 ground covered by a dark fog layer.
- A small blue cube (the scout) on top.
- Click on the ground to move it. Tiles in a small radius around the
  scout get cleared.
- Click a resource to send a gatherer drone.
- The gatherer goes to the resource, waits a couple of seconds while
  gathering, then returns to base. Counter in the top-left increases.
- Resources shrink as they get gathered and disappear when empty.

## Project layout

```
Assets/
├── MainScene.unity          main playable scene
├── Scripts/
│   ├── ScoutController.cs   click-to-move scout
│   ├── FogOfWar.cs          spawns fog tiles, removes them near scout
│   ├── DroneManager.cs      assigns gatherers to clicked resources
│   ├── GathererController.cs state machine for gather/deliver loop
│   ├── Resource.cs          resource node, shrinks on gather
│   ├── GameUI.cs            HUD text (TMP)
│   └── CameraController.cs  WASD pan + scroll zoom
└── Settings/                URP render pipeline assets
```

## Known issues

- On the very first compile Unity may show a Burst-compiler warning
  about a cached DLL not being loadable. It is harmless and goes away
  after a clean recompile.
- Fog tiles are destroyed permanently once revealed (no fog regrowth).
  This was intentional for the scope of the prototype.

## Repository

https://github.com/codcreater1/drone-idle-prototype
