using ThunderRoad;

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
            private AnimatorParamController animatorParamController;

            protected override void ManagedOnEnable()
            {
                base.ManagedOnEnable();
                item = GetComponent<Item>();
                if (item != null)
                {
                    animatorParamController = item.GetComponent<AnimatorParamController>();
                    item.OnGrabEvent += Item_OnGrabEvent;
                }
            }

            private void Item_OnGrabEvent(Handle handle, RagdollHand ragdollHand)
            {
                if (ragdollHand != null)
                {
                    var creature = ragdollHand.creature;
                    if (creature != null && !creature.isPlayer)
                    {
                        animatorParamController.SetTrigger("npcSword");
                    }
                }
            }
        }
    }
}