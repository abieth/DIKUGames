﻿using Breakout.Entities;
using Breakout.Utility;

namespace Breakout.PowerUps.Activators;

public class PlayerSpeedPowerUpActivator : IPowerUpActivator
{
    private readonly PlayerEntity _playerEntity;

    public PlayerSpeedPowerUpActivator(PlayerEntity playerEntity)
    {
        _playerEntity = playerEntity;
    }
    
    public void Activate()
    {
        float currentSpeed = _playerEntity.GetPlayerMovementSpeed();
        _playerEntity.SetPlayerMovementSpeed(currentSpeed * GameUtil.PlayerSpeedFactor);
        Task.Delay(5000).ContinueWith(t => _playerEntity.SetPlayerMovementSpeed(currentSpeed));
    }
}