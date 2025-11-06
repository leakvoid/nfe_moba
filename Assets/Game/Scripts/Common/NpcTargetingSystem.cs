using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(PhysicsSystemGroup))]
[UpdateAfter(typeof(PhysicsSimulationGroup))]
[UpdateBefore(typeof(ExportPhysicsWorld))]
public partial struct NpcTargetingSystem : ISystem
{
    private CollisionFilter _npcAttackFilter;

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PhysicsWorldSingleton>();
        _npcAttackFilter = new CollisionFilter
        {
            BelongsTo = 1 << 6, // Target Cast
            CollidesWith = 1 << 1 | 1 << 2 | 1 << 4 // Champions, Minions, Structures 
        };
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Dependency = new NpcTargetingJob
        {
            collisionWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>().CollisionWorld,
            collisionFilter = _npcAttackFilter,
            mobaTeamLookup = SystemAPI.GetComponentLookup<MobaTeam>(true)
        }.ScheduleParallel(state.Dependency);
    }

    [BurstCompile]
    [WithAll(typeof(Simulate))]
    public partial struct NpcTargetingJob : IJobEntity
    {
        [ReadOnly] public CollisionWorld collisionWorld;
        [ReadOnly] public CollisionFilter collisionFilter;
        [ReadOnly] public ComponentLookup<MobaTeam> mobaTeamLookup;

        private void Execute(Entity npcEntity, ref NpcTargetEntity targetEntity, in LocalTransform transform, in NpcTargetRadius targetRadius)
        {
            var hits = new NativeList<DistanceHit>(Allocator.TempJob);
            if (collisionWorld.OverlapSphere(transform.Position, targetRadius.Value, ref hits, collisionFilter))
            {
                var closestDistance = float.MaxValue;
                var closestEntity = Entity.Null;

                foreach (var hit in hits)
                {
                    if (!mobaTeamLookup.TryGetComponent(hit.Entity, out var mobaTeam))
                    {
                        continue;
                    }

                    if (mobaTeam.Value == mobaTeamLookup[npcEntity].Value)
                    {
                        continue;
                    }

                    if (hit.Distance < closestDistance)
                    {
                        closestDistance = hit.Distance;
                        closestEntity = hit.Entity;
                    }
                }

                targetEntity.Value = closestEntity;
            }
            else
            {
                targetEntity.Value = Entity.Null;
            }

            hits.Dispose();
        }
    }
}
