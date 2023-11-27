using System;
using System.Collections;
using System.Collections.Generic;
using ThunderRoad;
using UnityEngine;

namespace INeedAWeapon {
    public class ItemModuleGravityHammer : ItemModule {
        // When the item is loaded
        public override void OnItemLoaded(Item item) {
            base.OnItemLoaded(item);
            // Add the "GravityHammerBehavior" component to the item
            item.gameObject.AddComponent<GravityHammerBehavior>();
        }
    }
}