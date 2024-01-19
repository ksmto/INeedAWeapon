using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;
using Random = System.Random;


namespace INeedAWeapon
{
    public class MatiModule : ItemModule
    {
        public override void OnItemLoaded(Item item)
        {
            base.OnItemLoaded(item);
            item.gameObject.AddComponent<MatiChip>();
        }
    }
    public class MatiChip : ThunderBehaviour
    {


        private Item item;
        EffectInstance mati_SpawnGreet;
        EffectData mati_SpawnGreetEffect;
        protected override void ManagedOnEnable()
        {
            base.ManagedOnEnable();
            item = GetComponent<Item>();
            mati_SpawnGreetEffect = Catalog.GetData<EffectData>("INAWMatiGreet");



            if (item != null)
            {
                item.OnSnapEvent += MatiSnap;
                item.OnUnSnapEvent += MatiUnSnap;
            }

        }

        private void MatiUnSnap(Holder holder)
        {
            ModOptions.matiActive = false;
        }

        private void MatiSnap(Holder holder)
        {
            MatiGreeting();
            ModOptions.butlrActive = false;
            ModOptions.cortanaActive = false;
            ModOptions.matiActive = true;
            ModOptions.fretActive = false;
            ModOptions.lumuActive = false;
            ModOptions.misterchiefActive = false;
            ModOptions.matiActive = true;
            ModOptions.vergilActive = false;
        }
        public void MatiGreeting()
        {

                GameManager.local.StartCoroutine(MatiGreetRoutine());
        }
        public IEnumerator MatiGreetRoutine()
        {
            mati_SpawnGreet = mati_SpawnGreetEffect?.Spawn(Player.local.creature.transform);
            mati_SpawnGreet.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            yield return null;
        }
    }

    public class MatiAnnouncer : HaloEvents
    {

        bool isPlaying;
        private EffectData mati_PlayerDeathEffect, mati_LowHPEffect, mati_EnemyWarnEffect, mati_KillEffect, mati_KillHeadshotEffect, mati_PlayerHealEffect;
        private EffectInstance mati_PlayerDeath, mati_LowHP, mati_EnemyWarn, mati_Kill, mati_Headshot, mati_PlayerHeal;
        public override void OnCatalogRefresh(EventTime eventTime)
        {
            mati_PlayerHealEffect = Catalog.GetData<EffectData>("INAWMatiHeal");
            mati_PlayerDeathEffect = Catalog.GetData<EffectData>("INAWMatiDeath");
            mati_LowHPEffect = Catalog.GetData<EffectData>("INAWMatiLowHp");
            mati_EnemyWarnEffect = Catalog.GetData<EffectData>("INAWMatiEnemyWarn");
            mati_KillEffect = Catalog.GetData<EffectData>("INAWMatiKill");
            mati_KillHeadshotEffect = Catalog.GetData<EffectData>("INAWMatiKillHeadshot");
        }


        /* EVENTS */

        public override void OnCreatureHeal(Creature creature, float heal, Creature healer, EventTime eventTime)
        {
            if (creature.isPlayer)
            {
                if (ModOptions.matiActive && eventTime == EventTime.OnStart)
                {
                    if (AiRandom())
                    {
                        MatiPlayerHeal();
                    }
                }
            }

           
        }
        public override void OnCreatureHit(Creature creature, CollisionInstance collisionInstance, EventTime eventTime)
        {
            if (ModOptions.matiActive && eventTime == EventTime.OnStart)
            {
                if (creature.isPlayer)
                {
                    if (AiRandom())
                    {
                        {
                            MatiPlayerHurt();
                        }
                    }
                }
            }
        }
        public override void OnCreatureKill(Creature creature, Player player, CollisionInstance collisionInstance, EventTime eventTime)
        {
            {
                if (ModOptions.matiActive && eventTime == EventTime.OnStart)
                {
                    {
                        if (AiRandom())
                        {
                            MatiKills(collisionInstance, creature);
                        }
                    }

                }

            }
        }
        public override void OnCreatureSpawn(Creature creature)
        {
            if (ModOptions.matiActive && !creature.isPlayer)
            {
                if (AiRandom())
                {

                    MatiEnemyWarning();
                }

            }
        }

        /* METHODS */

        public void MatiPlayerHeal()
        {
            if (!isPlaying)
            {
                GameManager.local.StartCoroutine(PlayerHealRoutine());
            }
        }

        public void MatiPlayerHurt()
        {
            if (!isPlaying)
            {
                if (Player.currentCreature.currentHealth / Player.currentCreature.maxHealth * 100 <= 25f)
                {
                    GameManager.local.StartCoroutine(PlayerCriticalHurtRoutine());

                }
            }

        }
        public void MatiKills(CollisionInstance collisionInstance, Creature creature)
        {
            if (!isPlaying)
            {
                if (creature.isPlayer)
                {
                    GameManager.local.StartCoroutine(PlayerKilledRoutine());
                }

                else if (collisionInstance.damageStruct.hitRagdollPart == collisionInstance.damageStruct.hitRagdollPart.ragdoll.headPart)
                {
                    GameManager.local.StartCoroutine(CreatureHeadShotRoutine());
                }
                else
                {   
                    GameManager.local.StartCoroutine(CreatureKillRoutine());
                }
            }

        }

        public void MatiEnemyWarning()
        {
            if (!isPlaying)
            {
                GameManager.local.StartCoroutine(EnemySpawnWarningRoutine());
            }

        }

        public IEnumerator PlayerHealRoutine()
        {
            isPlaying = true;
            mati_PlayerHeal = mati_PlayerHealEffect?.Spawn(Player.local.creature.transform);
            mati_PlayerHeal.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }

        public IEnumerator PlayerCriticalHurtRoutine()
        {
            isPlaying = true;
            mati_LowHP = mati_LowHPEffect?.Spawn(Player.local.creature.transform);
            mati_LowHP. Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }

        public IEnumerator CreatureKillRoutine()
        {
            isPlaying = true;
            mati_Kill = mati_KillEffect?.Spawn(Player.local.creature.transform);
            mati_Kill.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }
        public IEnumerator CreatureHeadShotRoutine()
        {
            isPlaying = true;
            mati_Headshot = mati_KillHeadshotEffect?.Spawn(Player.local.creature.transform);
            mati_Headshot.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }
        public IEnumerator PlayerKilledRoutine()
        {
            isPlaying = true;
            mati_PlayerDeath = mati_PlayerDeathEffect?.Spawn(Player.local.creature.transform);
            mati_PlayerDeath.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }

        public IEnumerator EnemySpawnWarningRoutine()
        {
            isPlaying = true;
            mati_EnemyWarn = mati_EnemyWarnEffect?.Spawn(Player.local.creature.transform);
            mati_EnemyWarn.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }
    }
}
