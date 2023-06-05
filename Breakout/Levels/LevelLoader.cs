﻿using Breakout.Entities;
using Breakout.Entities.BlockTypes;
using Breakout.Factories;
using Breakout.IO;
using Breakout.PowerUps;
using Breakout.Hazard;
using Breakout.Storage;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;

namespace Breakout.Levels;

/// <summary>
/// A LevelLoader class implementation that follows the Dependency Inversion Principle (DIP) by
/// depending on abstractions rather than concrete implementations.
/// Makes it more flexible and easily interchangeable with new LevelLoaders.
/// follows 
/// </summary>
public class LevelLoader
{
    private readonly LevelStorage _levelStorage;
    private readonly IModelFactory<Level> _levelFactory;

    public int NumberOfLevels => _levelStorage.LevelPaths.Count;

    public LevelLoader()
    {
        _levelFactory = new LevelFactory();
        _levelStorage = new LevelStorage();
    }

    public Level LoadLevel(int levelNum)
    {
        string filePath = _levelStorage.LevelPaths[levelNum];
        FileReader.ReadFileFromPath(Path.Combine("Assets", "Levels", filePath), out string? data);
        var level = _levelFactory.Parse(data);
        return level;
    }
    
    public EntityContainer<BlockEntity> ConstructBlockEntities(Level level)
    {
        EntityContainer<BlockEntity> blockEntities = new();
        for (int row = 0; row < level.Map.Length; row++)
        {
            for (int column = 0; column < level.Map[row].Length; column++)
            {
                char key = level.Map[row][column];
                if (key == '-') continue;

                const float offsetY = 0.1f;

                int rowLength = level.Map[0].Length;
                float posX = 100f / rowLength / 100f * column;

                int columnLength = level.Map.Length;
                float posY = offsetY + 90f / columnLength / 100f * row;

                Vec2F pos = new(posX, posY);

                string imgPath = level.Legends.TryGetValue(key, out string? image) ? image : "error-block.png";
                string imgDmgPath = imgPath.Replace(".png", "-damaged.png");
                
                IBlockType blockType = key switch
                {
                    _ when key == level.Meta.PowerUp =>
                        new PowerUpBlockType(),
                    _ when key == level.Meta.Hardened =>
                        new HardenedBlockType(),
                    _ when key == level.Meta.Unbreakable =>
                        new UnbreakableBlockType(),
                    _ => new StandardBlockType()
                };
                
                var powerUp = key switch
                {
                    _ when key == level.Meta.PowerUp => PowerUpStorage.GetRandomPowerUp(),
                    _ => null
                };
                
                var hazard = blockType is StandardBlockType ? HazardStorage.GetRandomHazard() : null;
                
                var blockEntity = BlockEntity.Create(pos,
                    new Image(Path.Combine("Assets",
                        "Images",
                        imgPath)),
                    new Image(Path.Combine("Assets",
                        "Images",
                        imgDmgPath)), 
                    blockType, 
                    powerUp,
                    hazard);

                blockEntities.AddEntity(blockEntity);
            }
        }
        return blockEntities;
    }
}