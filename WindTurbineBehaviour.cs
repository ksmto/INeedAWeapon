using System;
using System.Collections;
using System.Collections.Generic;
using ThunderRoad;
using UnityEngine;

namespace INeedAWeapon
{
    public class WindTurbineBehaviour : ThunderBehaviour
    {
        public Transform windTurbine;

        protected override void ManagedOnEnable()
        {
            base.ManagedOnEnable();
            windTurbine = GetComponent<Transform>();
        }

        protected override void ManagedUpdate()
        {
            base.ManagedUpdate();
            if (windTurbine != null)
            {
                windTurbine.Rotate(Vector3.up, 0.5f * Time.deltaTime);
            }
        }
    }
}