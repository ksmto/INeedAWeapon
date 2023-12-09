using ThunderRoad;

namespace INeedAWeapon
{
    internal class ItemModuleScope : ItemModule
    {
        public override void OnItemLoaded(Item item)
        {
            base.OnItemLoaded(item);
            item.gameObject.AddComponent<ScopeBehaviour>();
        }
    }
}