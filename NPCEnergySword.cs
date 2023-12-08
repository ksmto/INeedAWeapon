using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using ThunderRoad.AI.Action;
using UnityEngine;

namespace INeedAWeapon
{
    public class NPCEnergySwordModule : ItemModule
    {
        public override void OnItemLoaded(Item item)
        {
            base.OnItemLoaded(item);
            item.gameObject.AddComponent<NPCEnergySword>();
        }


        public class NPCEnergySword : ThunderBehaviour
        {
            private Item item;
            AnimatorParamController anim;
            public void Start()
            {
                EventManager.onItemEquip += NpcHandCheck;
                if(item != null )
                {
                    anim = item.GetComponent<AnimatorParamController>();
                }
            }

            private void NpcHandCheck(Item item)
            {
                if (item.mainHandler != null)
                {
                    if (!item.mainHandler.creature.isPlayer)
                    {
                        anim.SetTrigger("npcSword");
                    }
                }
            }
        }
    }
}