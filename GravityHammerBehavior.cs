using System;
using System.Collections;
using System.Collections.Generic;
using ThunderRoad;
using UnityEngine;

namespace INeedAWeapon {
    public class GravityHammerBehavior : MonoBehaviour { // MonoBehavior is a Unity component
        private Item item;
        private AnimatorParamController animatorParamController;

        private bool active;

        // When the item is initalized
        private void Start() {
            // Get the Item component
            item = GetComponent<Item>();

            // Set actice to false so the weapon doesn't potentially have it's abilities on spawn
            active = false;

            if (item != null) {
                // Get the Param Controller
                animatorParamController = item.GetComponent<AnimatorParamController>();

                // Subscribe to the "OnHeldActionEvent" with the "Item_OnHeldActionEvent" method
                item.OnHeldActionEvent += Item_OnHeldActionEvent; // "+=" + "Tab" key, to auto complete

                // Iterates over every collider group
                foreach (ColliderGroup colliderGroup in item.colliderGroups) {
                    // If there is a collider group
                    if (colliderGroup != null) {
                        // If there is a "Hammer" collider group, or a "Bottom" collider group or a "Spear" collider group
                        if (colliderGroup.name == "Hammer") {
                            // Subscribe to the "OnCollisionStartEvent" with the "ShockwaveMono_OnCollisionStartEvent" method
                            colliderGroup.collisionHandler.OnCollisionStartEvent += OnCollisionStartEvent; // "+=" + "Tab" key, to auto complete
                        }
                    }
                }
            }
        }

        private void Item_OnHeldActionEvent(RagdollHand ragdollHand, Handle handle, Interactable.Action action) {
            // If alt-use was pressed
            if (action == Interactable.Action.AlternateUseStart) {
                // Sets active to the oppisite of what the bool value it is
                active = !active;

                // ^^^^^
                // Equal to this statement, but in one line
                // vvvvv
                // if (active == true)
                // {
                //     active = false;
                // }
                // if (active == false)
                // {
                //     active = true;
                // }
            }
        }

        // Method triggered when a collision starts.
        private void OnCollisionStartEvent(CollisionInstance collisionInstance) {
            if (active) {
                // Check if the impact velocity is sufficient to trigger the shockwave effect.
                if (collisionInstance.impactVelocity.sqrMagnitude >= 7.5f * 7.5f) {
                    animatorParamController.SetTrigger("Shockwave");
                }
            }
        }
    }
}