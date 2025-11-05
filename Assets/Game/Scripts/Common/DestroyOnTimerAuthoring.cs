using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class DestroyOnTimerAuthoring : MonoBehaviour
{
    public float DestroyOnTimer;

    public class Baker : Baker<DestroyOnTimerAuthoring>
    {
        public override void Bake(DestroyOnTimerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new DestroyOnTimer { Value = authoring.DestroyOnTimer });
        }
    }
}
