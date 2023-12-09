using System;
using ThunderRoad;

namespace INeedAWeapon
{
    public class CortanaModule : ItemModule
    {
        public override void OnItemLoaded(Item item)
        {
            base.OnItemLoaded(item);
            item.gameObject.AddComponent<CortanaChip>();
        }
    }

    public class CortanaChip : ThunderBehaviour
    {
        private Item chip;
        private CortanaAnnouncer announcer;

        protected override void ManagedOnEnable()
        {
            base.ManagedOnEnable();
            chip = GetComponent<Item>();
            if (chip != null)
            {
                CortanaAnnouncer announcer = new CortanaAnnouncer();
                EventManager.onEdibleConsumed += CortanaChipEaten;
                announcer.boolCortanaActive = false;
            }
        }

        private void CortanaChipEaten(Item edible, Creature consumer, EventTime eventTime)
        {
            if (consumer != null && consumer.isPlayer)
            {
                announcer.boolCortanaActive = true;
                announcer.CortanaGreeting();
            }
        }
    }

    public class CortanaAnnouncer : HaloEvents
    {
        public bool boolCortanaActive;
        private int intQuoteRoll;
        private Random random = new Random();
        private EffectData Cortana_DeathEffect, Cortana_LowHPEffect, Cortana_HurtEffect, Cortana_EnemyDamagedEffect, Cortana_EnemyWarnEffect, Cortana_KillEffect, Cortana_KillHeadshotEffect, Cortana_SpawnGreetEffect;
        private EffectInstance Cortana_Death, Cortana_LowHP, Cortana_Hurt, Cortana_EnemyDamaged, Cortana_EnemyWarn, Cortana_Kill, Cortana_KillHeadshot, Cortana_SpawnGreet;

        public override void OnCatalogRefresh(EventTime eventTime)
        {
            Cortana_DeathEffect = Catalog.GetData<EffectData>("INAWCortanaDeath");
            Cortana_LowHPEffect = Catalog.GetData<EffectData>("INAWCortanaLowHp");
            Cortana_HurtEffect = Catalog.GetData<EffectData>("INAWCortanaHurt");
            Cortana_EnemyDamagedEffect = Catalog.GetData<EffectData>("INAWCortanaEnemyDamage");
            Cortana_EnemyWarnEffect = Catalog.GetData<EffectData>("INAWCortanaEnemyWarn");
            Cortana_KillEffect = Catalog.GetData<EffectData>("INAWCortanaKill");
            Cortana_KillHeadshotEffect = Catalog.GetData<EffectData>("INAWCortanaKillHeadshot");
            Cortana_SpawnGreetEffect = Catalog.GetData<EffectData>("INAWCortanaGreet");
        }

        /* EVENTS */
        public override void OnCreatureHit(Creature creature, CollisionInstance collisionInstance, EventTime eventTime)
        {
            if (boolCortanaActive)
            {
                if (creature.isPlayer)
                {
                    if (CortanaRandom())
                    {
                        CortanaPlayerHurt();
                    }
                }
            }
        }

        public override void OnCreatureKill(Creature creature, Player player, CollisionInstance collisionInstance, EventTime eventTime)
        {
            if (boolCortanaActive)
            {
                if (CortanaRandom())
                {
                    CortanaKills(collisionInstance, creature);
                }
            }
        }

        public override void OnCreatureSpawn(Creature creature)
        {
            if (boolCortanaActive)
            {
                if (CortanaRandom())
                {
                    CortanaEnemyWarning();
                }
            }
        }

        /* METHODS */
        public bool CortanaRandom()
        {
            intQuoteRoll = random.Next(100 + 1);
            return UnityEngine.Random.Range(0, 10) >= intQuoteRoll;
            // return ModOptions.quoteChance >= intQuoteRoll;
        }

        public void CortanaEnemyDamage()
        {
            EffectInstance Cortana_EnemyDamage = Cortana_EnemyDamagedEffect?.Spawn(Player.local.creature.transform);
            Cortana_EnemyDamage.Play();
        }

        public void CortanaGreeting()
        {
            EffectInstance Cortana_SpawnGreetInstance = Cortana_SpawnGreetEffect?.Spawn(Player.local.creature.transform);
            Cortana_SpawnGreetInstance.Play();
        }

        public void CortanaPlayerHurt()
        {
            if (boolCortanaActive)
            {
                if (Player.currentCreature.currentHealth / Player.currentCreature.maxHealth * 100 <= 25f)
                {
                    EffectInstance CortanaPlayerHPWarn = Cortana_LowHPEffect?.Spawn(Player.local.creature.transform);
                    CortanaPlayerHPWarn.Play();
                }
                else
                {
                    EffectInstance CortanaPlayerHurt = Cortana_HurtEffect?.Spawn(Player.local.creature.transform);
                    CortanaPlayerHurt.Play();
                }
            }
        }

        public void CortanaKills(CollisionInstance collisionInstance, Creature creature)
        {
            if (creature.isPlayer)
            {
                EffectInstance CortanaPlayerDeath = Cortana_DeathEffect?.Spawn(Player.local.creature.transform);
                CortanaPlayerDeath.Play();
            }

            else if (collisionInstance.damageStruct.hitRagdollPart.ragdoll.headPart)
            {
                EffectInstance Cortana_Headshot = Cortana_KillHeadshotEffect?.Spawn(Player.local.creature.transform);
                Cortana_Headshot.Play();
            }
            else
            {
                EffectInstance Cortana_Kill = Cortana_KillHeadshotEffect?.Spawn(Player.local.creature.transform);
                Cortana_Kill.Play();
            }
        }

        public void CortanaEnemyWarning()
        {
            EffectInstance CortanaEnemyWarn = Cortana_EnemyWarnEffect?.Spawn(Player.local.creature.transform);
            CortanaEnemyWarn.Play();
        }
    }
}