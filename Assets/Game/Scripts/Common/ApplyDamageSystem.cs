using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.NetCode;
using UnityEngine;

[UpdateInGroup(typeof(PredictedSimulationSystemGroup), OrderFirst = true)]
[UpdateAfter(typeof(CalculateFrameDamageSystem))]
public partial struct ApplyDamageSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        var currentTick = SystemAPI.GetSingleton<NetworkTime>().ServerTick;
        var ecb = new EntityCommandBuffer(Allocator.Temp);

        foreach (var (currentHitPoints, damageThisTickBuffer, entity) 
            in SystemAPI.Query<RefRW<CurrentHitPoints>, DynamicBuffer<DamageThisTick>>().WithAll<Simulate>().WithEntityAccess())
        {
            if (!damageThisTickBuffer.GetDataAtTick(currentTick, out var damageThisTick))
            {
                continue;
            }
            if (damageThisTick.Tick != currentTick)
            {
                continue;
            }

            currentHitPoints.ValueRW.Value -= damageThisTick.Value;

            if (currentHitPoints.ValueRO.Value <= 0)
            {
                ecb.AddComponent<DestroyEntityTag>(entity);
            }

            ecb.Playback(state.EntityManager);
        }
    }
}
