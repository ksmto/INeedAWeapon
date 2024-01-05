using System;
using System.Collections;
using System.Collections.Generic;
using ThunderRoad;
using UnityEngine;
using UnityEngine.UIElements;

namespace INeedAWeapon
{
    public class TurretBehaviourInitalizer : HaloEvents
    {
        public override void ScriptEnable()
        {
            base.ScriptEnable();
            EventManager.onItemSpawn += EventManager_onItemSpawn;
        }

        private void EventManager_onItemSpawn(Item item)
        {
            if (item != null && !string.IsNullOrEmpty(item.itemId) && item.itemId == "INAW.MapObjects.Turret")
            {
                item.gameObject.TryGetOrAddComponent(out TurretBehaviour behaviour);
            }
        }

        public override void ScriptDisable()
        {
            base.ScriptDisable();
            EventManager.onItemSpawn -= EventManager_onItemSpawn;
        }
    }
}