﻿using Breakout.Hazard.Activators;
using Breakout.PowerUps;
using Breakout.PowerUps.Activators;
using Breakout.States.GameRunning;
using DIKUArcade.Graphics;

namespace Breakout.Hazard;

public class LoseLifeHazard : IHazard
{
    public IBaseImage GetImage()
    {
        return new Image(Path.Combine("Assets", "Images", "LoseLife.png"));
    }

    public IHazardActivator Activator() =>
        new LoseLifeHzActivator(GameRunningState.GetInstance().EntityManager.PlayerEntity);

}