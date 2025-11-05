using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

/*
[UpdateInGroup(typeof(PhysicsSystemGroup))]
[UpdateAfter(typeof(PhysicsSimulationGroup))]
public partial struct DamageOnTriggerSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();

        var damageOnTriggerJob = new DamageOnTriggerJob
        {
            DamageOnTriggerLookup = SystemAPI.GetComponentLookup<DamageOnTrigger>(true),
            TeamLookup = SystemAPI.GetComponentLookup<MobaTeam>(true),
            AlreadyDamagedLookup = SystemAPI.GetComponentLookup<AlreadyDamagedEntity>(true),
            DamageBufferLookup = SystemAPI.GetComponentLookup<DamageBufferElement>(true),
            ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
        };
        var simulationSingleton = SystemAPI.GetSingleton<SimulationSingleton>();
        state.Dependency = damageOnTriggerJob.Schedule(simulationSingleton, state.Dependency);
    }
}
*/

public struct DamageOnTriggerJob : ITriggerEventsJob
{
    public void Execute(TriggerEvent triggerEvent)
    {
        throw new System.NotImplementedException();
    }
}
/*
public struct DamageOnTriggerJob : ITriggerEventsJob
{
    [ReadOnly] public ComponentLookup<DamageOnTrigger> DamageOnTriggerLookup;
    [ReadOnly] public ComponentLookup<MobaTeam> TeamLookup;
    [ReadOnly] public ComponentLookup<AlreadyDamagedEntity> AlreadyDamagedLookup;
    [ReadOnly] public ComponentLookup<DamageBufferElement> DamageBufferLookup;

    public EntityCommandBuffer ECB;

    public void Execute(TriggerEvent triggerEvent)
    {
        Entity damageDealingEntity;
        Entity damageReceivingEntity;

        if (DamageBufferLookup.HasBuffer(triggerEvent.EntityA) &&
            DamageOnTriggerLookup.HasComponent(triggerEvent.EntityB))
        {
            damageReceivingEntity = triggerEvent.EntityA;
            damageDealingEntity = triggerEvent.EntityB;
        }
        else if (DamageBufferLookup.HasBuffer(triggerEvent.EntityB) &&
            DamageOnTriggerLookup.HasComponent(triggerEvent.EntityA))
        {
            damageReceivingEntity = triggerEvent.EntityB;
            damageDealingEntity = triggerEvent.EntityA;
        }
        else
        {
            return;
        }

        // Don't apply damage multiple times
        var alreadyDamagedBuffer = AlreadyDamagedLookup[damageDealingEntity];
        foreach (var AlreadyDamagedEntity in alreadyDamagedBuffer)
        {
            if (AlreadyDamagedEntity.Value.Equals(damageReceivingEntity))
            {
                return;
            }
        }

        // Ignore friendly fire
        if (TeamLookup.TryGetComponent(damageDealingEntity, out var damageDealingTeam) &&
            TeamLookup.TryGetComponent(damageReceivingEntity, out var damageReceivingTeam))
        {
            if (damageDealingTeam.Value == damageReceivingTeam.Value)
                return;
        }

        var DamageOnTrigger = DamageOnTriggerLookup[damageDealingEntity];
        ECB.AppendToBuffer(damageReceivingEntity, new DamageBufferElement { Value = DamageOnTrigger.Value });
        ECB.AppendToBuffer(damageDealingEntity, new AlreadyDamagedEntity { Value = damageReceivingEntity });
    }
}
*/