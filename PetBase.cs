using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace INeedAWeapon {
    public class PetBase : ThunderBehaviour
    {
        public Item pet;
        public Player player;

        protected override void ManagedOnEnable()
        {
            base.ManagedOnEnable();
            pet = GetComponent<Item>();
            if (pet != null) {
                pet.OnHeldActionEvent += OnHeldActionEvent;
            }

            EventManager.onCreatureKill += OnPetKill;
        }

        protected override void ManagedUpdate() {
            base.ManagedUpdate();
            Update();
        }

        protected override void ManagedFixedUpdate() {
            base.ManagedFixedUpdate();
            FixedUpdate();
        }

        public virtual void OnPetKill(Creature creature, Player player, CollisionInstance collisionInstance, EventTime eventTime) { }
        public virtual void OnHeldActionEvent(RagdollHand ragdollHand, Handle handle, Interactable.Action action) { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }

        protected override void ManagedOnDisable()
        {
            base.ManagedOnDisable();
            pet.OnHeldActionEvent -= OnHeldActionEvent;
            EventManager.onCreatureKill -= OnPetKill;
        }
    }
}
