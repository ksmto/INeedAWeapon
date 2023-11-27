using ThunderRoad;

namespace INeedAWeapon {
    public class HaloEvents : ThunderScript {

        public override void ScriptLoaded(ModManager.ModData modData) {
            base.ScriptLoaded(modData);
        }

        public override void ScriptEnable() {
            EventManager.onCreatureKill += OnCreatureKill;
            EventManager.onLevelLoad += OnMapChange;
            EventManager.onCatalogRefresh += OnCatalogRefresh;
        }

        public virtual void OnCreatureKill(Creature creature, Player player, CollisionInstance collisionInstance, EventTime eventTime) { }
        public virtual void OnMapChange(LevelData levelData, EventTime eventTime) { }
        public virtual void OnCatalogRefresh(EventTime eventTime) { }
    }
}