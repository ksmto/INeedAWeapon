using System;
using System.Collections;
using System.Collections.Generic;
using ThunderRoad;
using UnityEngine;

namespace INeedAWeapon
{
    public class TurretMagazineBehaviour : MonoBehaviour
    {
        private Item magazine;
        public bool explosiveBulletsActivated;

        private void Start()
        {
            magazine = GetComponent<Item>();
            explosiveBulletsActivated = false;

            if (magazine != null)
            {
                foreach (var imbue in magazine.imbues)
                {
                    imbue.onImbueEnergyFilled += Imbue_onImbueEnergyFilled;
                    imbue.onImbueEnergyDrained += Imbue_onImbueEnergyDrained;
                }
            }
        }

        private void Imbue_onImbueEnergyFilled(SpellCastCharge spellData, float amount, float change, EventTime eventTime)
        {
            if (eventTime == EventTime.OnStart)
            {
                return;
            }

            if (amount > 0)
            {
                explosiveBulletsActivated = true;
                DisplayMessage.instance.ShowMessage(new DisplayMessage.MessageData($"Explosive Bullets: {explosiveBulletsActivated}", "", "", "", 1, 0, null, null, false, true, false, true, MessageAnchorType.Head, null, true, 2, null, true, null));
            }
        }

        private void Imbue_onImbueEnergyDrained(SpellCastCharge spellData, float amount, float change, EventTime eventTime)
        {
            if (eventTime == EventTime.OnEnd)
            {
                return;
            }

            if (amount <= 0)
            {
                explosiveBulletsActivated = false;
                DisplayMessage.instance.ShowMessage(new DisplayMessage.MessageData($"Explosive Bullets: {explosiveBulletsActivated}", "", "", "", 1, 0, null, null, false, true, false, true, MessageAnchorType.Head, null, true, 2, null, true, null));
            }
        }
    }
}