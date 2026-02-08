# Sun Haven mods

# How to create mods for Unity games (with BepInEx and Harmony)

1. Use Visual Studio

2. New Project = C#, Windows, Library => Class Library .Net framework

3. Add references from `game/game_data/managed/`

- Assembly-Csharp.dll
- UnityEngine.dll (optional)
- UnityEngine.CoreModule.dll (optional)

4. Add reference to `game/BepInEx/core/` (BepInEx for this game must be present)

- BepInEx.dll
- 0Harmony.dll

5. Set `Copy Local` for each dependency to `false` (Under references in project window, right click, choose properties)

6. These five references cover most of what you need. You can add other references from the `managed` folder if necessary.

7. copy gitignore from existing project to new project so dll files don't commit.
