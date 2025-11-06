using System.Collections;
using System.Collections.Generic;
using TMG.NFE_Tutorial;
using Unity.Entities;
using Unity.NetCode;
using UnityEngine;

[WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation)]
public partial struct AbilityCooldownUISystem : ISystem
{
    // Start is called before the first frame update
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<NetworkTime>();
    }

    // Update is called once per frame
    public void OnUpdate(ref SystemState state)
    {
        var currentTick = SystemAPI.GetSingleton<NetworkTime>().ServerTick;
        var abilityCooldownUIController = AbilityCooldownUIController.Instance;

        foreach (var (cooldownTargetTicks, abilityCooldownTicks) in SystemAPI.Query<DynamicBuffer<AbilityCooldownTargetTicks>, AbilityCooldownTicks>())
        {
            if (!cooldownTargetTicks.GetDataAtTick(currentTick, out var curTargetTicks))
            {
                curTargetTicks.AoeAbility = NetworkTick.Invalid;
                curTargetTicks.SkillShotAbility = NetworkTick.Invalid;
            }

            if (curTargetTicks.AoeAbility == NetworkTick.Invalid ||
                currentTick.IsNewerThan(curTargetTicks.AoeAbility))
            {
                abilityCooldownUIController.UpdateAoeMask(0f);
            }
            else
            {
                var aoeRemainTickCount = curTargetTicks.AoeAbility.TickIndexForValidTick - currentTick.TickIndexForValidTick;
                var fillAmount = (float)aoeRemainTickCount / abilityCooldownTicks.AoeAbility;
                abilityCooldownUIController.UpdateAoeMask(fillAmount);
            }

            if (curTargetTicks.SkillShotAbility == NetworkTick.Invalid ||
                currentTick.IsNewerThan(curTargetTicks.SkillShotAbility))
            {
                abilityCooldownUIController.UpdateSkillShotMask(0f);
            }
            else
            {
                var SkillShotRemainTickCount = curTargetTicks.SkillShotAbility.TickIndexForValidTick - currentTick.TickIndexForValidTick;
                var fillAmount = (float)SkillShotRemainTickCount / abilityCooldownTicks.SkillShotAbility;
                abilityCooldownUIController.UpdateAoeMask(fillAmount);
            }
        }
    }
}
