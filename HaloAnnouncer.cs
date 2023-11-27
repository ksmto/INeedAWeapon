using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;

namespace INeedAWeapon
{
    public class HaloAnnouncer : HaloEvents
    {
        int killSpreeCounter = 0;

        int killCounter = 0;

        [ModOptionCategory("Announcer", 1)]
        [ModOptionTooltip("Toggle MultiKills")]
        [ModOptionOrder(3)]
        [ModOption("MultiKills", defaultValueIndex = 1)]
        [ModOptionButton]
        public static bool boolMultiKills;

        [ModOptionCategory("Announcer", 1)]
        [ModOptionTooltip("Toggle Killing Sprees")]
        [ModOptionOrder(2)]
        [ModOption("Killing Sprees", defaultValueIndex = 1)]
        [ModOptionButton]
        public static bool boolKillingSprees;

        public static ModOptionInt[] killTimeOut =
{
            new ModOptionInt("4 Seconds", 4),
            new ModOptionInt("8 Seconds", 8),
            new ModOptionInt("16 Seconds", 16),
            new ModOptionInt("32 Seconds", 32)
        };

        [ModOption(name: "MultiKill Timer", tooltip: "How long between kills to get a multi", nameof(killTimeOut))]
        [ModOptionCategory("Announcer", 1)]
        [ModOptionArrows]
        [ModOptionOrder(3)]
        public static int killTime = 4;




        public EffectData HaloKillingSpreeEffectData, HaloKillingFrenzyEffectData, HaloRunningRiotEffectData, HaloRampageEffectData, HaloUntouchableEffectData, HaloInvincibleEffectData;
        public EffectInstance HaloKillingSpreeEffect, HaloKillingFrenzyEffect, HaloRunningRiotEffect, HaloRampageEffect, HaloUntouchableEffect, HaloInvincibleEffect;
        public EffectData HaloDoubleKillEffectData, HaloTripleKillEffectData, HaloOverKillEffectData, HaloKilltacularEffectData, HaloKilltrocityEffectData, HaloKillimanjaroEffectData, HaloKilltastropheEffectData, HaloKillpocalypseEffectData, HaloKillionaireEffectData;
        public EffectInstance HaloDoubleKilleffect, HaloTripleKillEffect, HaloOverKillEffect, HaloKilltacularEffect, HaloKilltrocityEffect, HaloKillimanjaroEffect, HaloKilltastropheEffect, HaloKillpocalypseEffect, HaloKillionaireEffect;

        public override void EventManager_onCatalogRefresh(EventTime eventTime)
        {
            base.EventManager_onCatalogRefresh(eventTime);

            //Killing spree effects 
            HaloKillingSpreeEffectData = Catalog.GetData<EffectData>("HaloKillingSpreeQuote");
            HaloKillingFrenzyEffectData = Catalog.GetData<EffectData>("HaloKillingFrenzyQuote");
            HaloRunningRiotEffectData = Catalog.GetData<EffectData>("HaloRunningRiotQuote");
            HaloRampageEffectData = Catalog.GetData<EffectData>("HaloRampageQuote");
            HaloUntouchableEffectData = Catalog.GetData<EffectData>("HaloUntouchableQuote");
            HaloInvincibleEffectData = Catalog.GetData<EffectData>("HaloInvincibleQuote");

            //Multi Kill effects
            HaloDoubleKillEffectData = Catalog.GetData<EffectData>("HaloDoubleKillQuote");
            HaloTripleKillEffectData = Catalog.GetData<EffectData>("HaloTripleKillQuote");
            HaloOverKillEffectData = Catalog.GetData<EffectData>("HaloOverKillQuote");
            HaloKilltacularEffectData = Catalog.GetData<EffectData>("HaloKilltacularQuote");
            HaloKilltrocityEffectData = Catalog.GetData<EffectData>("HaloKilltrocityQuote");
            HaloKillimanjaroEffectData = Catalog.GetData<EffectData>("HaloKillimanjaroQuote");
            HaloKilltastropheEffectData = Catalog.GetData<EffectData>("HaloKilltastropheQuote");
            HaloKillpocalypseEffectData = Catalog.GetData<EffectData>("HaloKillpocalypseQuote");
            HaloKillionaireEffectData = Catalog.GetData<EffectData>("HaloKillionaireQuote");

        }
        public override void HaloMapChange(LevelData levelData, EventTime eventTime)
        {
            killSpreeCounter = 0;
        }
        public override void HaloKill(Creature creature, Player player, CollisionInstance collisionInstance, EventTime eventTime)
        {

            if (boolKillingSprees)
            {
                KillStreaks(creature);
            }

            if (boolMultiKills)
            {
                MultiKills(creature);
            }
        }
        void KillStreaks(Creature creature)
        {
            killSpreeCounter += 1;
            Debug.Log("KillSpree increased to" + killSpreeCounter);
            if (!creature.isPlayer)
            {
                if (killSpreeCounter == 5)
                {
                    GameManager.local.StartCoroutine(KillingSpree());
                }

                if (killSpreeCounter == 10)
                {
                    GameManager.local.StartCoroutine(KillingFrenzy());
                }
                if (killSpreeCounter == 15)
                {
                    GameManager.local.StartCoroutine(RunningRiot());
                }
                if (killSpreeCounter == 20)
                {
                    GameManager.local.StartCoroutine(Rampage());
                }
                if (killSpreeCounter == 25)
                {
                    GameManager.local.StartCoroutine(Untouchable());
                }
                if (killSpreeCounter == 30)
                {
                    GameManager.local.StartCoroutine(Invincible());
                    killSpreeCounter = 0;
                }
            }
            else
            {
                Debug.Log("Player died resetting killspree");
                killSpreeCounter = 0;
            }
        }

        void MultiKills(Creature creature)
        {
            if (killCounter == 0)
            {
                GameManager.local.StartCoroutine(Killtimer());
                killCounter += 1;
                Debug.Log("MultiKill increased to " + killCounter);
            }

            if (killCounter == 1)
            {
                HaloDoubleKilleffect.Play();
                killCounter += 1;
                Debug.Log("MultiKill increased to " + killCounter);

            }
            else if (killCounter == 2)
            {
                HaloTripleKillEffect.Play();
                killCounter += 1;
                Debug.Log("MultiKill increased to " + killCounter);

            }
            else if (killCounter == 3)
            {
                HaloOverKillEffect.Play();
                killCounter += 1;
                Debug.Log("MultiKill increased to " + killCounter);

            }
            else if (killCounter == 4)
            {
                HaloKilltacularEffect.Play();
                killCounter += 1;
                Debug.Log("MultiKill increased to " + killCounter);

            }
            else if (killCounter == 5)
            {
                HaloKilltrocityEffect.Play();
                killCounter += 1;
                Debug.Log("MultiKill increased to " + killCounter);

            }
            else if (killCounter == 6)
            {
                HaloKillimanjaroEffect.Play();
                killCounter += 1;
                Debug.Log("MultiKill increased to " + killCounter);

            }
            else if (killCounter == 7)
            {
                HaloKilltastropheEffect.Play();
                killCounter += 1;
                Debug.Log("MultiKill increased to " + killCounter);

            }
            else if (killCounter == 8)
            {
                HaloKillpocalypseEffect.Play();
                killCounter += 1;
                Debug.Log("MultiKill increased to " + killCounter);

            }
            else if (killCounter == 9)
            {
                HaloKillionaireEffect.Play();
                killCounter = 0;
                Debug.Log("MultiKill reset to " + killCounter);

            }
        }
        public IEnumerator KillingSpree()
        {
           
            HaloKillingSpreeEffect = HaloKillingSpreeEffectData.Spawn(Player.local.creature.transform);
            {
                HaloKillingSpreeEffect.Play();
                yield return null;
            }
        }
        public IEnumerator KillingFrenzy()
        {
           
            HaloKillingFrenzyEffect = HaloKillingFrenzyEffectData.Spawn(Player.local.creature.transform);
            {
                HaloKillingFrenzyEffect.Play();
                yield return null;
            }
        }
        public IEnumerator RunningRiot()
        {
            
            HaloRunningRiotEffect = HaloRunningRiotEffectData.Spawn(Player.local.creature.transform);
            {
                HaloRunningRiotEffect.Play();
                yield return null;
            }
        }
        public IEnumerator Rampage()
        {
            
            HaloRampageEffect = HaloRampageEffectData.Spawn(Player.local.creature.transform);
            {
                HaloRampageEffect.Play();
                yield return null;
            }
        }
        public IEnumerator Untouchable()
        {
            
            HaloUntouchableEffect = HaloUntouchableEffectData.Spawn(Player.local.creature.transform);
            {
                HaloUntouchableEffect.Play();
                yield return null;
            }
        }
        public IEnumerator Invincible()
        {
            
            HaloInvincibleEffect = HaloInvincibleEffectData.Spawn(Player.local.creature.transform); ;
            {
                HaloInvincibleEffect.Play();
                yield return null;
            }
        }
        public IEnumerator DoubleKill()
        {
            HaloDoubleKilleffect = HaloDoubleKillEffectData.Spawn(Player.local.creature.transform);
            {
                HaloDoubleKilleffect.Play();
                yield return null;
            }
        }
        public IEnumerator TripleKill()
        {
            
            HaloTripleKillEffect = HaloTripleKillEffectData.Spawn(Player.local.creature.transform);
            {
                HaloTripleKillEffect.Play();
                yield return null;
            }
        }
        public IEnumerator OverKill()
        {
            
            HaloOverKillEffect = HaloOverKillEffectData.Spawn(Player.local.creature.transform);
            {
                HaloOverKillEffect.Play();
                yield return null;
            }
        }
        public IEnumerator Killtacular()
        {
            
            HaloKilltacularEffect = HaloKilltacularEffectData.Spawn(Player.local.creature.transform);
            {
                HaloKilltacularEffect.Play();
                yield return null;
            }
        }
        public IEnumerator Killtrocity()
        {
            
            HaloKilltrocityEffect = HaloKilltrocityEffectData.Spawn(Player.local.creature.transform);
            {
                HaloKilltrocityEffect.Play();
                yield return null;
            }
        }
        public IEnumerator Killimanjaro()
        {
            
            HaloKillimanjaroEffect = HaloKillimanjaroEffectData.Spawn(Player.local.creature.transform);
            {
                HaloKillimanjaroEffect.Play();
                yield return null;
            }
        }
        public IEnumerator Killtastrophe()
        {
            
            HaloKilltastropheEffect = HaloKilltastropheEffectData.Spawn(Player.local.creature.transform);
            {
                HaloKilltastropheEffect.Play();
                yield return null;
            }
        }
        public IEnumerator Killpocalypse()
        {
            
            HaloKillpocalypseEffect = HaloKillpocalypseEffectData.Spawn(Player.local.creature.transform);
            {
                HaloKillpocalypseEffect.Play();
                yield return null;
            }
        }
        public IEnumerator Killionaire()
        {
           
            HaloKillionaireEffect = HaloKillionaireEffectData.Spawn(Player.local.creature.transform);

            {
                HaloKillionaireEffect.Play();
                yield return null;
            }
        }
        public IEnumerator Killtimer()
        {
            yield return new WaitForSeconds(killTime);
            killCounter = 0;
            Debug.Log("MultiKill Timed out.");
            yield return null;
        }
    }
}

