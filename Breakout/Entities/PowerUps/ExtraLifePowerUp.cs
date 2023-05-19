﻿using Breakout.Containers;
using Breakout.Utility;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Entities.PowerUps;

public class ExtraLifePowerUp : IPowerUpType
{
    public IBaseImage GetImage()
    {
        return new Image(Path.Combine("Assets", "Images", "LifePickUp.png"));
    }

    public void DropPowerUp(BlockEntity block)
    {
        float positionY = block.Shape.Position.Y - ConstantsUtil.PowerUpExtent.Y;
        float positionX = block.Shape.Position.X + (block.Shape.Extent.X / 2) - ConstantsUtil.PowerUpExtent.X / 2;

        var position = new Vec2F(positionX, positionY);
        EntityManager.PowerUps.AddEntity(PowerUpEntity.Create(position, "LifePickUp"));
    }
}