using ThunderRoad;

namespace INeedAWeapon
{
    public class HaloEvents : ThunderScript
    {
        public override void ScriptEnable()
        {
            EventManager.onCreatureSpawn += OnCreatureSpawn;
            EventManager.onCreatureHit += OnCreatureHit;
            EventManager.onCreatureKill += OnCreatureKill;
            EventManager.onLevelLoad += OnMapChange;
            EventManager.onCatalogRefresh += OnCatalogRefresh;
        }

        public virtual void OnCreatureSpawn(Creature creature) { }
        public virtual void OnCreatureHit(Creature creature, CollisionInstance collisionInstance, EventTime eventTime) { }
        public virtual void OnCreatureKill(Creature creature, Player player, CollisionInstance collisionInstance, EventTime eventTime) { }
        public virtual void OnMapChange(LevelData levelData, EventTime eventTime) { }
        public virtual void OnCatalogRefresh(EventTime eventTime) { }
    }
}