using ThunderRoad;
using UnityEngine;
using UnityEngine.UI;

namespace INeedAWeapon
{
    public class ScopeBehaviour : MonoBehaviour
    {
        private Item item;
        private Image blueReticle;
        private Color originalColor;

        private bool changedColor;

        private void Start()
        {
            InitializeComponents();
        }

        private void Update()
        {
            UpdateReticleColor();
        }

        private void InitializeComponents()
        {
            item = GetComponent<Item>();

            if (item != null)
            {
                Transform scopeReticleTransform = FindScopeReticleTransform();

                if (scopeReticleTransform != null)
                {
                    blueReticle = scopeReticleTransform.GetComponent<Image>();

                    if (blueReticle != null)
                    {
                        originalColor = blueReticle.color;
                    }
                    else
                    {
                        Debug.LogError("ScopeReticle does not have Image component.");
                    }
                }
                else
                {
                    Debug.LogError("ScopeReticle not found in children.");
                }
            }
        }

        private Transform FindScopeReticleTransform()
        {
            Transform scopeTransform = item.transform?.Find("Battle Rifle")?.Find("Scope")?.Find("ScopeCamera")?.Find("Canvas")?.Find("ScopeBackground")?.Find("ScopeImage")?.Find("ScopeReticle");
            return scopeTransform;
        }

        private void UpdateReticleColor()
        {
            if (item != null)
            {
                if (Physics.Raycast(item.transform.position, item.transform.forward, out RaycastHit hit))
                {
                    HandleHitCreature(hit);
                    changedColor = true;
                }
                else
                {
                    if (changedColor)
                    {
                        ResetReticleColor();
                    }
                }
            }
        }

        private void HandleHitCreature(RaycastHit hit)
        {
            Creature creature = hit.collider.GetComponentInParent<Creature>();
            if (creature != null)
            {
                Debug.Log("Hit creature");
                blueReticle.material.color = Color.red;
                Debug.Log("Changed color to red");
            }
        }

        private void ResetReticleColor()
        {
            if (blueReticle != null && blueReticle.material != null)
            {
                blueReticle.material.color = originalColor;
                Debug.Log("Changed color to original");
                changedColor = false;
            }
        }
    }
}
