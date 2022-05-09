# Braitenberg vehicles & Machina speculatrix simulation scripts
  ### Info
  These are the scripts I created for use in my Braitenberg vehicles & Machina speculatrix simulations. The full Unity files are not provided as they have little to no relevance to my studies.
  
  The files are split into their own folders, files in the "Common" folder are used by all simulations.

  ## Common
  Common contains:
   - "CameraController" script which is used to keep the camera above the vehicle in the scene.
   - "SpawnLight" script which is used to spawn a light at the mouse position on left-click.

  ## Vehicle 1
  Vehicle 1 contains:
   - "VehicleOneController" script which, as the name suggests, controls this vehicle.
   - "GenerateHeatmap" script which generates the invisible heatmap which is used in "VehicleOneController".
  
  ## Vehicle 2
  Vehicle 2 is used for both vehicle 2 and vehicle 3 as they are nearly identical.
  Vehicle 2 contains:
   - "VehicleTwoController" script which is used to control the vehicle. Two toggles are used to toggle the mode of the current vehicle and the V3 toggle to toggle the vehicle to the vehicle 3 behaviour.
   - "GenerateMapV2" script which can be toggled to generate a random area filled with lights for use with the vehicles.

  ## Vehicle 4
  Vehicle 4 contains:
   - "VehicleFourController" script which is used to control the vehicle. This script uses some code from "VehicleTwoController" as they work in a similar way.

  ## Machina speculatrix
  Machina speculatrix contains:
   - "MSpecController" script which is used to control the vehicle.
   - "SpawnBlocker" script which is used to spawn a "blocker" at the mouse position on right-click. The "blocker" is only ever used and accounted for with this robot.
