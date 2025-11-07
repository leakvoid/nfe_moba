using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class MobaPrefabsAuthoring : MonoBehaviour
{
    [Header("Entities")]
    public GameObject ChampionPrefab;
    public GameObject MinionPrefab;
    public GameObject GameOverEntity;
    public GameObject RespawnEntity;

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
                Minion = GetEntity(authoring.MinionPrefab, TransformUsageFlags.Dynamic),
                GameOverEntity = GetEntity(authoring.GameOverEntity, TransformUsageFlags.None),
                RespawnEntity = GetEntity(authoring.RespawnEntity, TransformUsageFlags.None)
            });

            AddComponentObject(prefabContainerEntity, new UIPrefabs
            {
                HealthBar = authoring.HealthBarPrefab,
                SkillShot = authoring.SkillShotAimPrefab
            });
        }
    }
}
