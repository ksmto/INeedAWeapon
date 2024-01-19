using System;
using ThunderRoad;
using UnityEngine;using Random = System.Random;

namespace INeedAWeapon
{
    public class HaloEvents : ThunderScript
    {
        int intQuoteRoll;
        Random random = new Random();
        public override void ScriptEnable()
        {
            EventManager.onCreatureKill += OnCreatureKill;
            EventManager.onLevelLoad += OnMapChange;
            EventManager.onCatalogRefresh += OnCatalogRefresh;
            EventManager.onCreatureHit += OnCreatureHit;
            EventManager.onCreatureSpawn += OnCreatureSpawn;
            EventManager.onCreatureHeal += OnCreatureHeal;
        }

        public virtual void OnCreatureHeal(Creature creature, float heal, Creature healer, EventTime eventTime){}
        public virtual void OnCreatureSpawn(Creature creature) { }
        public virtual void OnCreatureHit(Creature creature, CollisionInstance collisionInstance, EventTime eventTime) { }
        public virtual void OnCreatureKill(Creature creature, Player player, CollisionInstance collisionInstance, EventTime eventTime) { }
        public virtual void OnMapChange(LevelData levelData, EventTime eventTime) { }
        public virtual void OnCatalogRefresh(EventTime eventTime) { }


        public bool AiRandom()
        {
            intQuoteRoll = random.Next(100 + 1);
            if (ModOptions.quoteChance <= intQuoteRoll)
            {
                return true;
            }
            else return false;
        }
    }
}
