# Weapon-Crafting
A modular weapon crafting system that creates randomized weapons from base models and allows the user to craft their own weapon from parts they collected. This project is made of various systems that help it all come together and work in unison. Some of these systems are the interaction scripts with Interactable and PlayerInteract, the weapon generation scripts with Banshee45 and StatsCreation/StatSheet, the weapon functionality scripts with Weapon State Manager and all its associated states, and the UI scripts that allow for crafting like ScreenManager and BaseCrafting.
You can find all of these scripts and more in the Assets folder. Each file will have comments throughout to explain what is happening at each step. For an even more in-depth look at the project, see my portfolio here: 

**Weapon Stats**

Rate of Fire: Frequency at which this weapon can shoot
Damage: Maximum value that each bullet fired from this weapon can deal to a target
Range: Distance that this weapon can be fired at and still get the maximum damage value. Shooting a target out of the range will deal decreased damage
Stability: Determines how much horizontal recoil/shake this weapon experiences when shooting (Higher = lower shake; Lower = higher shake)
Recoil Control: Determines how much vertical recoil/bounce this weapon experiences when shooting (Higher = lower bounce; lower = higher bounce)
Handling: Speed at which this weapon can be aimed down sights and return to hip fire (Higher = faster; Lower = slower)
Reload Speed: Speed of reloading and how much time is required (Higher = faster; Lower = slower)
Zoom Factor: Determines how much the cameras will zoom in when aiming down sights
Magazine and Inventory Size: Number of bullets that can be fired before needing a reload and total amount of bullets stored

