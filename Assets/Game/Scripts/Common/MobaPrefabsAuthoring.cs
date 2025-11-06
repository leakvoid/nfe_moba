using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class MobaPrefabsAuthoring : MonoBehaviour
{
    [Header("Entities")]
    public GameObject ChampionPrefab;
    public GameObject MinionPrefab;

    [Header("GameObject")]
    public GameObject HealthBarPrefab;
    public GameObject SkillShotAimPrefab;

    public class Baker : Baker<MobaPrefabsAuthoring>
    {
        public override void Bake(MobaPrefabsAuthoring authoring)
        {
            var prefabContainerEntity = GetEntity(TransformUsageFlags.None);
            AddComponent(prefabContainerEntity, new MobaPrefabs
            {
                Champion = GetEntity(authoring.ChampionPrefab, TransformUsageFlags.Dynamic),
                Minion = GetEntity(authoring.MinionPrefab, TransformUsageFlags.Dynamic)
            });

            AddComponentObject(prefabContainerEntity, new UIPrefabs
            {
                HealthBar = authoring.HealthBarPrefab,
                SkillShot = authoring.SkillShotAimPrefab
            });
        }
    }
}
