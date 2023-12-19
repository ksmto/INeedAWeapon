using ThunderRoad;
using UnityEngine;
using UnityEngine.UI;

namespace INeedAWeapon
{
    public class ScopeBehaviour : ThunderBehaviour
    {
        private Item item;
        private Image reticle;

        protected override void ManagedOnEnable()
        {
            base.ManagedOnEnable();
            item = GetComponent<Item>();

            if (item != null)
            {
                Transform scopeReticleTransform = FindScopeReticleTransform();
                Transform scopeCameraTransform = FindScopeCameraTransform();

                if (scopeReticleTransform != null)
                {
                    reticle = scopeReticleTransform.GetComponent<Image>();
                }
                else
                {
                    Debug.LogError("Scope Reticle not found in children.");
                }

                if (scopeCameraTransform != null)
                {
                    scopeCameraTransform.gameObject.SetActive(true);
                }
                else
                {
                    Debug.LogError("Scope Camera not found in children.");
                }
            }
        }

        protected override void ManagedUpdate()
        {
            base.ManagedUpdate();
            UpdateReticleColor();
        }

        private void UpdateReticleColor()
        {
            if (item != null && reticle != null)
            {
                // Default color to blue
                Color reticleColor = Color.blue;

                if (Physics.SphereCast(item.transform.position, 3.0f, item.transform.right, out RaycastHit hit))
                {
                    if (hit.collider.TryGetComponentInParent(out Creature creature))
                    {
                        if (creature != null && !creature.isPlayer)
                        {
                            // Set color to red if a non-player creature is found
                            reticleColor = Color.red;

                            // Break out of the loop since we found a relevant creature
                            // break;
                        }
                    }
                }

                // Set the reticle color after iterating through all colliders
                // reticle.material.color = reticleColor;
            }
        }


        /*/// <summary>
        /// Sets the reticles color to red if is active, otherwise, the reticle will be blue.
        /// </summary>
        /// <param name="active">Whether or not if the reticle is red, true meaning is red and vise virsa.</param>
        private void SetReticleColor(bool active)
        {
            if (reticle != null)
            {
                var material = reticle.material;
                if (material != null)
                {
                    if (active)
                    {
                        material.color = Color.red;
                    }
                    else
                    {
                        material.color = Color.blue;
                    }
                }
            }
        }*/

        private Transform FindScopeCameraTransform()
        {
            Transform cameraTransform = item?.transform?.Find("Battle Rifle")?.Find("Scope")?.Find("ScopeCamera");
            return cameraTransform;
        }

        private Transform FindScopeReticleTransform()
        {
            Transform reticleTransform = FindScopeCameraTransform()?.Find("Canvas")?.Find("ScopeBackground")?.Find("ScopeImage")?.Find("ScopeReticle");
            return reticleTransform;
        }
    }
}