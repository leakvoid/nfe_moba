
using Unity.Entities;
using UnityEngine;

public class MainCameraAuthoring : MonoBehaviour
{
    public class Baker : Baker<MainCameraAuthoring>
    {
        public override void Bake(MainCameraAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponentObject(entity, new MainCamera());
            AddComponent<MainCameraTag>(entity);
        }
    }
}

public class MainCamera : IComponentData
{
    public Camera Value;
}

public struct MainCameraTag : IComponentData
{
    
}