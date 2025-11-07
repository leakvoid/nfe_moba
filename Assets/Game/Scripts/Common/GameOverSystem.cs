using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using Unity.Entities;
using UnityEngine;

public partial class GameOverSystem : SystemBase
{
    public Action<TeamType> OnGameOver;

    protected override void OnCreate()
    {
        RequireForUpdate<GameOverTag>();
        RequireForUpdate<GamePlayingTag>();
    }

    protected override void OnUpdate()
    {
        var gameOverEntity = SystemAPI.GetSingletonEntity<GameOverTag>();
        var winningTeam = SystemAPI.GetComponent<WinningTeam>(gameOverEntity).Value;
        OnGameOver?.Invoke(winningTeam);

        var GamePlayingEntity = SystemAPI.GetSingletonEntity<GamePlayingTag>();
        EntityManager.DestroyEntity(GamePlayingEntity);

        Enabled = false;
    }
}
