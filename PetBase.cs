using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace HoverPet
{
    public class PetBase : ThunderBehaviour
    {

        protected Item pet;
        protected Player player;
        protected override void ManagedOnEnable()
        {
            pet = GetComponent<Item>();
            base.ManagedOnEnable();
            pet.OnHeldActionEvent += Item_OnHeldActionEvent;
            EventManager.onCreatureKill += PetKillReact;
        }

        public virtual void PetKillReact(Creature creature, Player player, CollisionInstance collisionInstance, EventTime eventTime)
        {
        }

        public virtual void Item_OnHeldActionEvent(RagdollHand ragdollHand, Handle handle, Interactable.Action action)
        {
        }

        public virtual void Update()
        {
        }
        
        public virtual void FixedUpdate()
        {
        }

        protected override void ManagedOnDisable()
        {
            base.ManagedOnDisable();
            pet.OnHeldActionEvent -= Item_OnHeldActionEvent;
            EventManager.onCreatureKill -= PetKillReact;
        }
    }
}
