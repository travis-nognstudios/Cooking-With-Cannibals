# Design Documentation

**Author:** Red Nur

**Version:** 1.0

**Written:** 9/24/2020

## Source Code Structure

Game files are organized into folders under the `Assets` folder. The `_Scripts` folder contains all the C# code.

## Levels

Each level is a separate scene. The pause menu is "embedded" into each scene instead of being its own scene. 

The `Main Menu` scene is the player's apartment. This is the hub from which they can choose any of the other kitchen levels to play. Only the first kitchen level is unlocked to begin with.

There are 3 kitchen levels, labeled LevelOne, LevelTwo and LevelThree.

## Game loop

The core game loop starts when the player loads into a scene. It ends when the player has completed all of the actions in the pre-defined sequence. 

The game loop is implemented as a behavior tree. In each scene, there is a `Gameloop Manager` object that contains the level specific behavior tree and all of its nodes.

## Core loop sequence

The core loop is made of the following sequence:

* Customer makes an order, which shows up as an order ticket

* The order ticket tells the player which recipe was ordered and what ingredients make up that recipe (and their cooking steps)
  
* The player gathers the ingredients from their surroundings, and chops and cooks them as needed
  
* The player puts all of the ingredients into the *serving area* (to-go box or serving plate) and activates the *done trigger* (closes the to-go box or hits the service bell)
  
* The serving area matches the ingredient combination against the currently waiting orders and picks which order it matched
  
* The ingredients are switched out for the finished meal and the order is graded on quality based on how well the preparation matched the recipe
  
* The player is awarded a tip amount based on the quality check

* This repeats until the predefined number of orders have been made and completed (or missed)

## Recipes

Each recipe in the game is broken down into 3 parts.

1. One Main Ingredient
2. Toppings
3. Finished Meal

These are set up in the `Recipe Manager` object. The main ingredient also needs to have its preparation steps specified. Each preparation step contains the type of cooking (Grill, Deepfry, Boil), also how long it takes to reach the *cooked* state and how long it takes to reach the *overcooked/burnt* state.

The recipe manager also connects each recipe to its order ticket.

## Orders

The order sequence is predefined for each level. The defined sequence can be found in the `Game Manager` object in each level. There are 2 kinds of sequences:

1. Ordered
2. Random

These sequences can be combined in any order. The rest of the parameters for the orders are defined in the script `OrderSpawnerv5`.

Total service time can be capped, but it was decided to not have a time cap so this value is usually left very high. The frequency of orders (time between orders) is also specified here.

Orders can also be VIP. VIP orders have half the time for its corresponding recipe as a usual order, but it pays twice as much in tips.

## Order tickets

Once service starts, the order spawner picks an order and sends it to the `TicketManager`. The ticket manager contains multiple `TicketPointv2` objects. Each ticket point contains a ticket timer and a ticket attach point.

When the order is made, the ticket manager picks one of the empty ticket points and spawns in the associated order ticket for that recipe. The order ticket is attached to the point and the timer is started.

When the order is fulfilled/missed, the ticket manager despawns the ticket and frees up the ticket point to receive a new order.

## Chopping

Items which can be chopped have a `Choppable` script attached to them. The number of chops it takes to cut the item into its parts is specified on this script.

When a choppable ingredient is touched by a knife blade, it adds 1 to its chop count. A chop ui pops up to show how many chops have been made and how many still need to be made to cut the item. When the total number of chops is made, on the final chop the item is despawned, and its chopped pieces are spawned in at the same spot.

The chopped items are separate prefabs which have been lined up with the unchopped item so that when they're spawned in it appears seamless.

## Cooking

Items which can be cooked have the `Cookablev2` script on them. This script tracks the item's cook states. It also specifies the different materials the item should use at different cook states.

There are 3 kinds of cooking. Each has an associated cooktop.

1. Grill - Pan
2. Deepfry - Fryer
3. Boil - Pot

The game is designed to introduce a new cook type in each level. The player cooks an item by placing it on/inside the correspending cooktop.

Each cooktop has a `CookTop` script that configures what kind of cooking it does. A cook top also needs a heat source. When the heat source is on, the cook top can cook food, otherwise it can't. Each heat source has a `HeatSource` script on it which manages how the heat source is turned on and off.

There are 2 kinds of heat source:

1. Stove
2. Fryer Oil

Each stove has a few burners. Each burner has a corresponding turner handle. The player can turn the handle to its on/off positions to control whether the burner is on or off. The pans and pots (grilling and boiling) use the stove as their heat source.

The fryer oil has an oil meter. Oil is depleted when there is food cooking in the deepfryer. When the oil meter reaches 0, the deepfryer stops cooking. The player must use oil refills to put oil back in the fryer to keep cooking. The oil meter is used by the frying basket (deepfrying).

## Serving

There are 2 kinds of service areas:

1. Styrofoam Box
2. Serving Plate
   
Each service area has a *food area* where it recognizes the ingredients that the player has placed. Each service area also has a *trigger* which tells it to combine the ingredients to create the finished meal. The serving area uses a `MealSpawner` script to configure these. The styrofoam box's trigger is closing the box. The plate's trigger is the player touching the service bell.

When the trigger is activated, the meal spawner first determines what recipe the player has tried to make. In each level, each recipe has a unique main ingredient, so the meal spawner determines the recipe off of that. 

Then, it looks at all current orders to see if there is an order for that recipe. In the case of multiple orders for that recipe, it prioritizes the older one. VIP tickets are higher priority than non-VIP.

If a matching order is found, the meal spawner de-spawns all the ingredients and spawns in the correct finished meal. It also informs the order spawner that the order was completed.

If no matching recipe is found, then a *Dubious Food* item is spawned.

## Tips

The quality of the order is determined by checking the cookstate of the main ingredient against the order, and the presence of every topping specified in the recipe.

Every mismatched cooking step on the main ingredient (wrong order, over/under cooked) is penalized. Missing toppings or toppings added that are not in the recipe are also penalized.

The tip amount is calculated based on how accurately the player made the recipe. The range of tips for a normal order ranges from **0** (for dubious food) to **3** (for a perfect preparation). VIP order tips are doubled.

The tips go into a piggy bank that's present on each level. The piggy bank shows how much money the player has earned during that service.

## Rating

Each level has a grade scale. The grades the player can get, from highest to lowest, are:

* A
* B
* C
* F

The grades are based on how many tips the player accumulated over the course of the service. The amount of tips needed for each grade is specified in advance in the `RatingCardSpawner` script in the game manager. The player can see these thresholds in-game on the grade poster that's present on each level.

At the end of service, the player's grade is shown to them on a *rating card* that pops up where the orders usually are.

Getting an F counts as failing the level. The player must pass a level in order to unlock the next one. If they fail the level, they're sent back to the apartment. If they pass, the next kitchen level is loaded.

