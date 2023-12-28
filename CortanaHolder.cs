using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;

namespace INeedAWeapon
{
    public class CortanaHolderEquip: ThunderScript
    {
        public override void ScriptLoaded(ModManager.ModData modData)
        {
            base.ScriptLoaded(modData);
            EventManager.onCreatureSpawn += EventManager_onCreatureSpawn;
        }

        private void EventManager_onCreatureSpawn(Creature creature)
        {
            if (creature.isPlayer)
            {
                Catalog.InstantiateAsync("Cortana.Port", Vector3.zero, Quaternion.identity, creature.ragdoll.headPart.transform, inter =>
                {
                    if (creature.data.gender == CreatureData.Gender.Male)
                    {
                        inter.transform.localPosition = new Vector3(-0.012427764f, 0.0732002184f, -0.00174463412f);
                    }
                    else if (creature.data.gender == CreatureData.Gender.Female)
                    {
                        inter.transform.localPosition = new Vector3(-0.0258000009f, 0.0680000037f, -0.0115999999f);
                    }
                    inter.transform.localEulerAngles = new Vector3(350.016968f, 271.12146f, 8.66899143e-07f);

                    inter.gameObject.AddComponent<CortanaHolder>();
                }, "Cortana.Port");
            }

        }
    }
    public class CortanaHolder: MonoBehaviour
    {
        Creature creature;
        Holder holder;

        void Awake()
        {
            creature = transform.root.GetComponent<Creature>();
            holder = GetComponent<Holder>();

            holder.Load(Catalog.GetData<InteractableData>("CortanaHolder"));
            holder.data.forceAllowTouchOnPlayer = false;
            holder.data.disableTouch = false;
            holder.RefreshChildAndParentHolder();

            Debug.Log($"Cortana Holder Active");
        }
    }
}
