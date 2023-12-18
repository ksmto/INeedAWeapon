using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ThunderRoad;
using System.Collections;

namespace INeedAWeapon
{

    public class DoTZoneWeaponModule : ItemModule
    {

        public override void OnItemLoaded(Item item)
        {
            base.OnItemLoaded(item);
            item.gameObject.AddComponent<DoTZone>();
        }
        public class DoTZone : ThunderBehaviour
        {

            public void OnTriggerEnter(Collider other)
            {
                //if the thing entering the trigger zone is a ragdoll
                if (other.attachedRigidbody?.GetComponent<RagdollPart>() is RagdollPart part)
                {
                    //if the ragdoll is the player and player blights is false the script does nothing.
                    if (part.ragdoll.creature.isPlayer && !ModOptions.playerAOE)
                    {
                        return;
                    }
                    //if the ragdoll is anything other than the player and the ragdolls state isn't dead runs.  Prevents the script from running on dead bodies.  It wouldn't do anything to them, but
                    //its better to to prevent to save memory.
                    else if (!part.ragdoll.creature.isKilled)
                    {
                        part.ragdoll.creature.gameObject.AddComponent<BlightDamageComponent>();
                        return;
                    }
                }
            }
        }
    }
    public class BlightDamageComponent : ThunderBehaviour
    {
        public float blightTick = 0f;
        public float blightFrequency = 1f;
        public float blightTickTotal = 0f;

        private Creature creature;
        //sets the damage as a collision that hurts for energy damage equal to the blightDamage option
        CollisionInstance blightinstance = new CollisionInstance(new DamageStruct(DamageType.Energy, ModOptions.aoeDamage));
        //the player takes half damage from blight to give time to heal
        CollisionInstance playerblightinstance = new CollisionInstance(new DamageStruct(DamageType.Energy, ModOptions.aoeDamage / 2));

        public override ManagedLoops EnabledManagedLoops => ManagedLoops.Update;

        protected override void ManagedOnEnable()
        {
            base.ManagedOnEnable();
            creature = GetComponent<Creature>();
        }

        private void BlightCreature(CollisionInstance collisionInstance)
        {
            //if the total number of ticks is less or equal to the duration continues the script
            if (blightTickTotal <= ModOptions.aoeDuration)
            {
                //if the current tick is greater than or equal to the frequency continues 
                if (blightTick >= blightFrequency)
                {
                    //damages the creature with the collision instance
                    creature.Damage(collisionInstance);
                    blightTickTotal = blightTick;
                    blightTick = 0;
                }
            }
            else
            {
                Destroy(this);
            }
        }

        protected override void ManagedUpdate()
        {
            base.ManagedUpdate();
            //increases the tick 
            blightTick += Time.deltaTime;
            if (creature.isKilled)
            {
                Destroy(this);
            }
            if (!creature.isKilled)
            {
                BlightCreature(creature.isPlayer ? playerblightinstance : blightinstance);
            }
        }
    }
}
