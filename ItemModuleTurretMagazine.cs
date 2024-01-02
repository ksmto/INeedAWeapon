using System;
using System.Collections;
using System.Collections.Generic;
using ThunderRoad;
using UnityEngine;

namespace INeedAWeapon
{
    public class ItemModuleTurretMagazine : ItemModule
    {
        public override void OnItemLoaded(Item item)
        {
            base.OnItemLoaded(item);
            item.gameObject.TryGetOrAddComponent(out TurretMagazineBehaviour tmBehaviour);
        }
    }
}