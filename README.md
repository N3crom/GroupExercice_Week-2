# GroupExercice

### Requirements Done:
- A top-down game
- The player moves tile by tile using simple button controls (up / down / left / right) in a dungeon crawler style.
- "Wall" tiles are impassable.
- The player can pick objects (any kind).
- To complete a level, the player must collect all objects and reach the exit tile.
- Create a first fixed level to test the mechanics.

## [17-03-2025] Update Functionality
### Requirements:
- Player can see only visited tiles (work for neighboorhood tiles), by enable/disable gameobject
- Ghost PowerUp (with Interface IPowerUp), give ability to walk throught walls (work with array tile type)
- Menu Start (Start and Quit)
- Add one level (copy), when finished go to 2nd ..., reach last level go back to main menu
- Player have max moevment points per level, when reach zero, player die
- When Player die, restart level
### Refactor:
- TileManager contain Dict with pos x,0,z of gameobject parent for key
- Dict Values are now data with gameobject tile and if they are visited
