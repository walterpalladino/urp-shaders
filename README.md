# urp-shaders
 Unity URP Shaders

I'm using on this examples assets from the FREE package from the Unity Asset Store:

- Low Poly Tree Pack from Broken Vector (https://assetstore.unity.com/packages/3d/vegetation/trees/low-poly-tree-pack-57866)
- FREE assets from Quaternius site (https://quaternius.com/)
- textures from https://3dtextures.me/
- Free folliage textures from the Unity Asset Store: https://assetstore.unity.com/packages/3d/vegetation/yughues-free-stylized-foliages-13392
- Unity Cat from: https://assetstore.unity.com/packages/essentials/tutorial-projects/endless-runner-sample-game-87901

## URP Shaders for
- Water
  - Low Poly Water
  - Normal textured Water
  - Avoid water inside boats
- Vegetation  
  - Wind on Vegetation
  - Wind on Clothes like Flags and Sails
  - Vegetation Wind Shader (Using billboard like techniques and double side polygons)
- Snow cover on surfaces
- TV noise
- Terrain
  - Triplanar texture for terrain
  - Terrain Blending textures
  - Big Terrain setting up to 4 vertical levels 
- Planar Reflections
- Overlayed Texture (Like a grid over a textured mapped used world coordinates)
- Transparent Objects
- low Poly Models special shaders
  - Low Poly Flat Shader
  - Low Poly Shader with Halftones (dithering)
- Toon shader implementing diffuse + specular + rim
- Monotone Shader using dithering
- Sky Shaders
  - Skydome sahder to be applied to dome meshes using textured noise and internal noise functions (The textured versions was created to avoid problems with Unity noise implementation on some GPUs)
  - Custom SkyBox including gradient color for sky, clouds, sun, moon and stars
