using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Rendering;
using UnityEngine;

public class MinionAuthoring : MonoBehaviour
{
    public float MoveSpeed;

    public class Baker : Baker<MinionAuthoring>
    {
        public override void Bake(MinionAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<MinionTag>(entity);
            AddComponent<NewMinionTag>(entity);
            AddComponent(entity, new CharacterMoveSpeed { Value = authoring.MoveSpeed });
            AddComponent<MinionPathIndex>(entity);
            AddComponent<MinionPathPosition>(entity);
            AddComponent<MobaTeam>(entity);
            AddComponent<URPMaterialPropertyBaseColor>(entity);
        }
    }
}
