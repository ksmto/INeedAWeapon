using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;

namespace INeedAWeapon
{
    public class HaloEvents : ThunderScript
    {

        public override void ScriptLoaded(ModManager.ModData modData)
        {
            base.ScriptLoaded(modData);
        }
        public override void ScriptEnable()
        {
            EventManager.onCreatureKill += HaloKill;
            EventManager.onLevelLoad += HaloMapChange;
            EventManager.onCatalogRefresh += EventManager_onCatalogRefresh;
        }

        public virtual void EventManager_onCatalogRefresh(EventTime eventTime)
        {
        }

        public virtual void HaloMapChange(LevelData levelData, EventTime eventTime)
        {
        }

        public virtual void HaloKill(Creature creature, Player player, CollisionInstance collisionInstance, EventTime eventTime)
        {

        }
    }
}
