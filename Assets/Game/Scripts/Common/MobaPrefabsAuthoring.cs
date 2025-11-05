using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class MobaPrefabsAuthoring : MonoBehaviour
{
    [Header("Entities")]
    public GameObject ChampionPrefab;

    [Header("GameObject")]
    public GameObject HealthBarPrefab;

    public class Baker : Baker<MobaPrefabsAuthoring>
    {
        public override void Bake(MobaPrefabsAuthoring authoring)
        {
            var prefabContainerEntity = GetEntity(TransformUsageFlags.None);
            AddComponent(prefabContainerEntity, new MobaPrefabs
            {
                Champion = GetEntity(authoring.ChampionPrefab, TransformUsageFlags.Dynamic),
            });

            AddComponentObject(prefabContainerEntity, new UIPrefabs
            {
                HealthBar = authoring.HealthBarPrefab
            });
        }
    }
}
