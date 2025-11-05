using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.NetCode;
using UnityEngine;

public class AbilityAuthoring : MonoBehaviour
{
    public GameObject AoeAbility;
    public GameObject SkillShotAbility;

    public float AoeAbilityCooldown;
    public float SkillShotAbilityCooldown;

    public NetCodeConfig netCodeConfig;
    private int SimulationTickRate => netCodeConfig.ClientServerTickRate.SimulationTickRate;

    public class Baker : Baker<AbilityAuthoring>
    {
        public override void Bake(AbilityAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new AbilityPrefabs
            {
                AoeAbility = GetEntity(authoring.AoeAbility, TransformUsageFlags.Dynamic),
                SkillShotAbility = GetEntity(authoring.SkillShotAbility, TransformUsageFlags.Dynamic),
            });
            AddComponent(entity, new AbilityCooldownTicks
            {
                AoeAbility = (uint)(authoring.AoeAbilityCooldown * authoring.SimulationTickRate),
                SkillShotAbility = (uint)(authoring.SkillShotAbilityCooldown * authoring.SimulationTickRate)
            });
            AddBuffer<AbilityCooldownTargetTicks>(entity);
        }
    }
}
