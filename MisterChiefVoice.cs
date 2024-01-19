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
    public class MisterChiefModule : ItemModule
    {
        public override void OnItemLoaded(Item item)
        {
            base.OnItemLoaded(item);
            item.gameObject.AddComponent<MisterChiefChip>();
        }
    }
    public class MisterChiefChip : ThunderBehaviour
    {


        private Item item;
        EffectInstance misterchief_SpawnGreet;
        EffectData misterchief_SpawnGreetEffect;
        protected override void ManagedOnEnable()
        {
            base.ManagedOnEnable();
            item = GetComponent<Item>();
            misterchief_SpawnGreetEffect = Catalog.GetData<EffectData>("INAWMisterChiefGreet");



            if (item != null)
            {
                item.OnSnapEvent += MisterChiefSnap;
                item.OnUnSnapEvent += MisterChiefUnSnap;
            }

        }

        private void MisterChiefUnSnap(Holder holder)
        {
            ModOptions.misterchiefActive = false;
        }

        private void MisterChiefSnap(Holder holder)
        {
            MisterChiefGreeting();
            ModOptions.butlrActive = false;
            ModOptions.cortanaActive = false;
            ModOptions.circActive = false;
            ModOptions.fretActive = false;
            ModOptions.lumuActive = false;
            ModOptions.misterchiefActive = true;
            ModOptions.matiActive = false;
            ModOptions.vergilActive = false;
        }
        public void MisterChiefGreeting()
        {

                GameManager.local.StartCoroutine(MisterChiefGreetRoutine());
        }
        public IEnumerator MisterChiefGreetRoutine()
        {
            misterchief_SpawnGreet = misterchief_SpawnGreetEffect?.Spawn(Player.local.creature.transform);
            misterchief_SpawnGreet.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            yield return null;
        }
    }

    public class MisterChiefAnnouncer : HaloEvents
    {

        bool isPlaying;
        private EffectData misterchief_PlayerDeathEffect, misterchief_LowHPEffect, misterchief_EnemyWarnEffect, misterchief_KillEffect, misterchief_KillHeadshotEffect, misterchief_PlayerHealEffect;
        private EffectInstance misterchief_PlayerDeath, misterchief_LowHP, misterchief_EnemyWarn, misterchief_Kill, misterchief_Headshot, misterchief_PlayerHeal;
        public override void OnCatalogRefresh(EventTime eventTime)
        {
            misterchief_PlayerHealEffect = Catalog.GetData<EffectData>("INAWMisterChiefHeal");
            misterchief_PlayerDeathEffect = Catalog.GetData<EffectData>("INAWMisterChiefDeath");
            misterchief_LowHPEffect = Catalog.GetData<EffectData>("INAWMisterChiefLowHp");
            misterchief_EnemyWarnEffect = Catalog.GetData<EffectData>("INAWMisterChiefEnemyWarn");
            misterchief_KillEffect = Catalog.GetData<EffectData>("INAWMisterChiefKill");
            misterchief_KillHeadshotEffect = Catalog.GetData<EffectData>("INAWMisterChiefKillHeadshot");
        }


        /* EVENTS */

        public override void OnCreatureHeal(Creature creature, float heal, Creature healer, EventTime eventTime)
        {
            if (creature.isPlayer)
            {
                if (ModOptions.misterchiefActive && eventTime == EventTime.OnStart)
                {
                    if (AiRandom())
                    {
                        MisterChiefPlayerHeal();
                    }
                }
            }

           
        }
        public override void OnCreatureHit(Creature creature, CollisionInstance collisionInstance, EventTime eventTime)
        {
            if (ModOptions.misterchiefActive && eventTime == EventTime.OnStart)
            {
                if (creature.isPlayer)
                {
                    if (AiRandom())
                    {
                        {
                            MisterChiefPlayerHurt();
                        }
                    }
                }
            }
        }
        public override void OnCreatureKill(Creature creature, Player player, CollisionInstance collisionInstance, EventTime eventTime)
        {
            {
                if (ModOptions.misterchiefActive && eventTime == EventTime.OnStart)
                {
                    {
                        if (AiRandom())
                        {
                            MisterChiefKills(collisionInstance, creature);
                        }
                    }

                }

            }
        }
        public override void OnCreatureSpawn(Creature creature)
        {
            if (ModOptions.misterchiefActive && !creature.isPlayer)
            {
                if (AiRandom())
                {

                    MisterChiefEnemyWarning();
                }

            }
        }

        /* METHODS */

        public void MisterChiefPlayerHeal()
        {
            if (!isPlaying)
            {
                GameManager.local.StartCoroutine(PlayerHealRoutine());
            }
        }

        public void MisterChiefPlayerHurt()
        {
            if (!isPlaying)
            {
                if (Player.currentCreature.currentHealth / Player.currentCreature.maxHealth * 100 <= 25f)
                {
                    GameManager.local.StartCoroutine(PlayerCriticalHurtRoutine());

                }
            }

        }
        public void MisterChiefKills(CollisionInstance collisionInstance, Creature creature)
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

        public void MisterChiefEnemyWarning()
        {
            if (!isPlaying)
            {
                GameManager.local.StartCoroutine(EnemySpawnWarningRoutine());
            }

        }

        public IEnumerator PlayerHealRoutine()
        {
            isPlaying = true;
            misterchief_PlayerHeal = misterchief_PlayerHealEffect?.Spawn(Player.local.creature.transform);
            misterchief_PlayerHeal.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }

        public IEnumerator PlayerCriticalHurtRoutine()
        {
            isPlaying = true;
            misterchief_LowHP = misterchief_LowHPEffect?.Spawn(Player.local.creature.transform);
            misterchief_LowHP. Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }

        public IEnumerator CreatureKillRoutine()
        {
            isPlaying = true;
            misterchief_Kill = misterchief_KillEffect?.Spawn(Player.local.creature.transform);
            misterchief_Kill.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }
        public IEnumerator CreatureHeadShotRoutine()
        {
            isPlaying = true;
            misterchief_Headshot = misterchief_KillHeadshotEffect?.Spawn(Player.local.creature.transform);
            misterchief_Headshot.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }
        public IEnumerator PlayerKilledRoutine()
        {
            isPlaying = true;
            misterchief_PlayerDeath = misterchief_PlayerDeathEffect?.Spawn(Player.local.creature.transform);
            misterchief_PlayerDeath.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }

        public IEnumerator EnemySpawnWarningRoutine()
        {
            isPlaying = true;
            misterchief_EnemyWarn = misterchief_EnemyWarnEffect?.Spawn(Player.local.creature.transform);
            misterchief_EnemyWarn.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }
    }
}
