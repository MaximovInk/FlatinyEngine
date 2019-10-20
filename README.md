# FlatinyEngine
2D engine made in OpenTK/C# Inspired by Unity,Monogame and GMS2

# Futures
- [ ] GUI
- [ ] Plugin system
- [ ] Effects/Shaders
- [ ] Modern OpenGL
- [ ] Component system
- [ ] Custom meshes
- [ ] Text rendering
- [ ] Texture rendering
- [ ] Sprite rendering
- [ ] Atlas packing
- [ ] Animation 
- [ ] Tilemaps
- [ ] Physics
- [ ] Editor

# TODO
- More fast and functional text rendering
- Reimplementation GUI
- Change fixed opengl to modern opengl (50%)
- Plugins
- Tilemaps


# Log

Version 0.4
- Big change : Moving to Modern OpenGL (WIP)
- Removed ColoredVertex, now it is just Vertex 

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


