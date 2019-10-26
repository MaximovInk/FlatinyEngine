# FlatinyEngine
2D engine made in OpenTK/C# Inspired by Unity,Monogame and GMS2

# Futures
- [ ] GUI
- [ ] Plugin system
- [x] Effects/Shaders
- [ ] Modern OpenGL
- [x] Component system
- [x] Custom meshes
- [x] Text rendering
- [x] Texture rendering
- [x] Sprite rendering
- [ ] Atlas packing
- [ ] Animation 
- [ ] Tilemaps
- [ ] Physics
- [ ] Editor

# TODO
- More fast and functional text rendering
- Reimplementation GUI
- Plugins
- Tilemaps
- Joysticks and gamepads

# Log

Version 0.4
- Modern OpenGL
- Work Shaders / Effects
- Removed ColoredVertex, now it is just Vertex 
- Removed GUISystem
- Code refactoring and cleanup
- Added ScreenRenderer and ScreenProjection property for Screen
- Moved IRender,IUpdate,IStart,IEnd from Core.Components to Core namespace
- Moved Renderer from Core.Components to Core.Graphics namespace
- Removed deprecated OpenGL code

Version 0.3
- Refactoring of code
- New working component: TilemapRenderer
- ImageRect splits to two rects: GUIImage for sprites and GUIRawImage for textures
- Graphics gui rects now must be implement interface IGraphics
- Added interfaces: IStart,IEnd,IUpdate,IRender for components
- Renderer now is not a component
- GUIButton now is empty rect and has reference to graphics rect
- Optimization chunks mesh by dirty flag pattern
- Some utilites added to Utilites.cs class

Version 0.2
- GUI Interactions(MouseOver,Dragging)
- Renderer now is base component of graphic components
- MeshRenderer now implements Renderer
- Created tilemaps namespace and classes Tilemap,Tile
- Mesh now contains method to get new grid mesh Mesh.Grid(width,height)

Version 0.1
- Basic rendering


