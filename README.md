# FlatinyEngine
2D engine made in OpenTK/C# Inspired by Unity,Monogame and GMS2
## Sub-modules
- Graphics<br/>
Core of rendering, include:
- - Mesh rendering
- - Shaders
- - Textures
- - Fonts
- GUI<br/>
Graphical User Interface, include:
- - Canvas
- - Empty rect
- - Image rendering
- - Text rendering
- ProcessManagment<br/>
Combines all modules into one system
- SceneManagment<br/>
Provides access and control of the game scene
- Components<br/>
Components of game objects,include:
- - Component(Base)
- - MeshRenderer
- - SpriteRenderer
- - TextRenderer
- - Transform
- Console logger<br/>
Logger for debug 
- Input<br/>
Handle input
- Mathf<br/>
Useful functions

Version 0.2
- GUI Interactions(MouseOver,Dragging)
- Renderer now is base component of graphic components
- MeshRenderer now implements Renderer
- Created tilemaps namespace and classes Tilemap,Tile
- Mesh now contains method to get new grid mesh Mesh.Grid(width,height)

Version 0.1
- Basic rendering


