using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class AbilityMoveSpeedAuthoring : MonoBehaviour
{
    public float AbilityMoveSpeed;

    public class Baker : Baker<AbilityMoveSpeedAuthoring>
    {
        public override void Bake(AbilityMoveSpeedAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new AbilityMoveSpeed { Value = authoring.AbilityMoveSpeed });
        }
    }
}
