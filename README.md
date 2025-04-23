# Lightburn

**A single‑player first-person 3D firefighting game with an educational narrative on incarceration policies.**

---

## Game Overview

You play as a firefighter navigating a burning urban street, extinguishing fires, rescuing civilians, and reading newspaper headlines that reveal laws harmful to incarcerated people. Limited resources and dynamic hazards force you to prioritize rescue versus containment. The game concludes with a dialogue‑driven epilogue reflecting on California’s harmful policies.

---

## Gameplay Overview

**Players & Interaction Patterns:**  
Single-player firefighter amid crew (Single‑Player VS. Game)

**Objectives:**  
- Extinguish fires (Solution)  
- Rescue civilians (Rescue)  
- Unlock areas by reading newspapers that reveal harmful incarceration laws (Educational)

**Rules:**  
- Limited water supply  
- Blocked zones open only after reading required newspapers  
- Touching hazards drains health

**Resources:**  
- Player model  
- Scoreboard UI  
- Fire billboard  
- Water cannon (hose — shoots water)  
- Hazards (electric and fire)  
- Newspapers (Interactable)  
- NPCs in peril (survivor model)

**Conflict:**  
Scarce water supply and moral trade‑offs (save people vs. put out fires)

**Boundaries:**  
Urban street map with fixed barriers and houses to enter and extinguish fires; ends in a non‑interactive epilogue reflecting on policy impact

**Outcome:**  
Count of fires extinguished, civilians saved (yet to implement) , and a narrative reflection on California’s incarceration policies

---

## Key Features

- **Physics-Based Gameplay:**  
  - Utilizes Unity’s physics engine with colliders, rigidbodies, hinge joints, and particle systems.
  - Interactive objects and hazards react to player actions and environmental forces.

- **Advanced AI Systems:**  
  - Finite State Machines (FSM) guide fire hazards, NPC firefighter behaviors, and other dynamic entities.
  - Rule-based systems control elements such as water usage and fire spawning (e.g., a new fire spawns every 15 seconds if conditions are met, up to a maximum of five on the map).
  - Custom pathfinding ensures NPCs navigate the environment intelligently.

- **Mecanim Animations:**  
  - Comprehensive animation systems for first-person (firefighter’s arms and hose) and third-person character models.
  - Blend trees and state transitions create smooth, responsive animations across various character actions.

- **Resource Management**  
  - Limited water supply mechanics force careful resource management.
  - Water refills at hydrants are critical to keep the firefighting effort going.

---

## Updated Level Design

- **Fire Hazards & Navigation:**  
  - Additional fire hazards have been added on the first floor to gradually introduce game rules to the player as they explore each room.  
  - The number of fire “enemies” is carefully varied: the first room features a low count to ease players in, while the last room of the first floor contains more hazards to let the player build confidence in extinguishing fires.

- **Pathfinding Survivor:**  
  - A survivor with pathfinding behavior exits the building first. This early demonstration teaches the player that future survivors (on upper floors) will need guidance, integrating both gameplay and narrative elements.

- **Fire Hydrant & Water Refill:**  
  - A fire hydrant is now positioned at the front of the building near a put-outable fire. When the player uses the water hose (draining their limited water supply), approaching this hydrant will automatically refill the supply.

- **Environmental Cues:**  
  - Orange lighting surrounds the fire hazards to signal danger.  
  - Blue lighting is used around electrical hazards, clearly indicating areas that can harm the player.

---

## Updated Character Design

- **Player (Firefighter):**  
  - The player is represented by a firefighter model that shows only the arms for a first-person perspective during water hose usage.  
  - The water hose is a simplified, front-cropped version of a traditional fire hose to streamline player interactions.

- **NPC Firefighters:**  
  - Other firefighter NPCs use a full-body model sharing similar visual design cues but with additional animations for movement and fire-fighting actions.

- **Frightened Survivors & Civilians:**  
  - Simplistic models (man and woman) have been added to enhance immersion. These models are designed to convey panic and distress within the burning apartment building and surrounding environment.

---

## Updated Physics

- **Fire Hazards:**  
  - Fire hazards emit realistic flame particles. They have colliders set up to detect when the player comes into contact, causing damage.  
- **Electrical Hazards:**  
  - Electrical hazards emit electric particles and similarly use colliders to apply damage upon collision.
- **Water Hose:**  
  - The water spray utilizes a particle system that not only shows continuous water effects but also uses gravity to control the water's fall distance.  
- **Fire Hydrant:**  
  - The hydrant is a stationary object with a capsule collider on the model for physical presence and a box collider for detecting player proximity. When the player enters its range, a script refills the water supply automatically.

---

## A7 Team and Contributions

- **Alberto Campuzano:**  
  - **Level Design:**  
    - Expanded and altered level design. (Made overall level larger as the prior demo's level design felt too cramped for the player to navigate)
    - Implemented the open door mechanic.
    - Added a level barrier.
    - Added a fire truck with lights.
  - **Sound Design:**  
    - Integrated firetruck sounds.
    - Added background music. (Ambience amidst the other sounds (fire, electricity, survivors))
  - **Additional Contributions:**  
    - Managed level lighting (apartment lights) and textures. (Improved lighting to make navigation for the player easier, as well as set the tone for the game)
    - Worked on Mecanim animation for survivor behaviors.

- **Semih Kesler:**  
  - **Gameplay & Mechanics:**  
    - Developed newspaper glow effect and implemented lock/unlock logic. (Feedback for players on status of the item, such as when it can be interacted with)
    - Enabled the pickup of the newspaper with an accompanying animation.
    - Fixed stair mesh issues.
    - Integrated a nav agent for rescuing citizens.
    - Controlled locked doors opening using the newspaper. (To stop the player from speeding all the way to another area when a prior area needs to be cleared)
  - **Sound Design:**  
    - Created fire hose spray effects.
    - Added equip and paper pickup sound effects.
  - **Additional Contributions:**  
    - Implemented water hose mechanics with a particle system influenced by gravity.
    - Programmed FSM/pathfinding for firefighter NPCs and integrated first-person Mecanim animations.

- **Bill Munkh-Erdene:**  
  - **Fire & Hazard Mechanics:**  
    - Gradually removed fire from the spray effect. (To make the game more challenging and introduce strategy to how fires should be handled and how water resources will be divided)
    - Developed fire AI logic that spawns/duplicates hazards when near the player. (To avoid player being bombarded with too many fire object enemies before even encountering them)
    - Enabled functionality where removing all fire from the level unlocks the newspaper. (To stop the player from speeding all the way to another area when a prior area needs to be cleared)
    - Completed final revisions and polish of the level.
  - **Sound Design:**  
    - Developed fire noise effects. (Better Feedback for the player to understand where danger is)
    - Created electrical hazard sound effects.
    - Integrated survivors' coughing sounds.
  - **Additional Contributions:**  
    - Developed AI constructs using RBS and FSM for fire hazards and enemy behaviors.
    - Created physics-driven hazards, including electrical and fire particle systems.

---

## A8 Team and Contributions

- **Alberto Campuzano:**
  - **Shader:**  
    - Custom shader for creating the fire which we already incorporated into the game design.
  - **Modifications:**  
    - Flashlight added to camera for clear view in dark areas to increase visibility and realness 
    - Added outside light to simulate city lights for added visibility outside.

- **Semih Kesler:**  
  - **Shader:**  
    - Newspaper glow (Force Field) shader helps to signify importance of the newspaper in terms of progressing in game
  - **Modifications:**  
    - Newspaper Fire Left on floor text helps with clarity of task
    - Replaced mouse over with proximity and turn toward for easier interactions for users
    - Made interactable objects glowing because users had trouble knowing what was and wasn’t
    - Connect door and newspaper with corresponding both light red if locked and both blue if unlocked
    - Aimable Hose follows players mouse on y axis users requested and allows more freedom and better field of view
    - Stair barrier so players don’t skip floors
    - Win condition complete all tasks in UI reach end credits
    - Set key R to Reset game when in credit scene
    - Scrollable End Screen

- **Bill Munkh-Erdene:**  
  - **Shader:**  
    - Added a shader to fire hazards and fire objects to make them more visually distinct and easily recognizable.
  - **Modifications:**  
    - Added a fire sizzling sound when a fire object / fire hazard is put out; giving more feedback to the player to know if a fire source has been extinguished or not.
    - Added a fire scoreboard that dynamically increases if fire objects duplicate or not, as well as Survivors and Newspapers scores to make objectives of the game more          understandable.
    - Lowered the amount of fire objects that can be present on each map and how often they clone themselves, as players were struggling with the amount of fire objects in        the Alpha release.
    - When water source is ran out, it now says to refill water at a fire hydrant, communicating effectively to the player on how to restore their water resources. This was       implemented for better visual feedback for players that struggled with water resource management.
    - Added a damage overlay for when player is attacked or damaged by a fire object / hazard or an electric hazard. 
    - Added an additional player hurt sound that plays each time player loses health: This was implemented as players struggled with keeping up when they were being               attacked, this more visually distinct feedback makes it more clear to the player when they are losing health. Additionally, as players were frustrated with losing           their health permanently, added a regeneration feature that heals 10 health points every 5 seconds after being damaged.
    - Made the First Form of Writing Contribution to the game by making a cutscene that gives exposition to the players about the serious educational point of the game.
    - Introduction of the main character - Jim Zorig; their background and tied their story to the objectives, Mecanims (NPC crew) in the game




Added a damage overlay for when player is attacked or damaged by a fire object / hazard or an electric hazard. 
Added an additional player hurt sound that plays each time player loses health
This was implemented as players struggled with keeping up when they were being attacked, this more visually distinct feedback makes it more clear to the player when they are losing health.
Additionally, as players were frustrated with losing their health permanently, added a regeneration feature that heals 10 health points every 5 seconds after being damaged.

    

---

## Credits
Developed by Alberto Campuzano, Semih Kesler, Bill Munkh-Erdene @ CS426, Spring 2025 by Liz Marai.
