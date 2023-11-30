using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace HoverPet
{

    public class PetModule : ItemModule
    {
        public override void OnItemLoaded(Item item)
        {
            // adds the petfollow component to the item on spawn
            base.OnItemLoaded(item);
            item.gameObject.AddComponent<PetFollow>();
            Debug.Log("PetFollow added to" + item);
        }
    }
    public class PetFollow : PetBase
    {
        Vector3 vecPlayerPos;
        Vector3 vecItemPos;

        [ModOptionCategory("Pet Movement", 2)]
        [ModOptionTooltip("How fast your pet will follow you")]
        [ModOption("Follow Speed", defaultValueIndex = 1)]
        [ModOptionOrder(1)]
        [ModOptionSave]
        [ModOptionSlider]
        public static float floSpeed;

        [ModOptionCategory("Pet Movement", 2)]
        [ModOptionTooltip("How fast your pet will follow you")]
        [ModOption("Follow Speed", defaultValueIndex = 1)]
        [ModOptionOrder(2)]
        [ModOptionSave]
        [ModOptionSlider]
        public static float maxDistance;

        bool booAllowedtoFollow;

        public float Kp, Ki, Kd;

        Vector3 totalError = new Vector3(0, 0, 0);
        Vector3 lastError = new Vector3(0, 0, 0);
        Vector3 targetHeight;
        Rigidbody petRB;
        
        public override void Item_OnHeldActionEvent(RagdollHand ragdollHand, Handle handle, Interactable.Action action)
        {
            //toggles the pets ability to follow
            base.Item_OnHeldActionEvent(ragdollHand, handle, action);
            if (action == Interactable.Action.AlternateUseStart)
            {
                if (booAllowedtoFollow == true)
                {
                    booAllowedtoFollow = false;
                    Debug.Log(pet + "not allowed to follow");
                }
                else
                {
                    booAllowedtoFollow = true;
                    Debug.Log(pet + "allowed to follow");
                }
            }
    
        }
        public override void Update()
        {
            maxDistance = 0.3f;

            vecPlayerPos = Player.local.transform.position;
            vecItemPos = pet.transform.position;
            if (booAllowedtoFollow == true)
            {

                Vector3.MoveTowards(vecItemPos, vecPlayerPos, floSpeed);

                if ((vecItemPos - vecPlayerPos).sqrMagnitude > maxDistance * maxDistance)
                {
                    pet.transform.position = Vector3.Lerp(vecPlayerPos, vecItemPos, maxDistance);
                }
            }

        }

      public override void FixedUpdate()
        {
            //sets the corrective proportion P, I the increase/decrease of P, and the duration of those corrections D,   
            Kp = 0.2f;
            Ki = 0.1f;
            Kd = 1f;
            //sets the target height to 0 x 2 y 0 z 
            targetHeight = new Vector3(0.0f, 2.0f, 0.0f);
            //sets the petRB to the items rigidbody
            petRB = pet.gameObject.GetComponent<Rigidbody>();
            //sets a raycast to go down
            RaycastHit hit;
            Ray downRay = new Ray(transform.position, -Vector3.up);
            //sends the raycast down and reports the hit
            if(booAllowedtoFollow == true)
            {
                Physics.Raycast(downRay, out hit);
                //current error is the targetheight (2Y)  - the position the raycast returns the item at. 
                var currentError = targetHeight - hit.transform.position;
                //sets the Active correction to the current error * the Kp value
                Vector3 ap = currentError * Kp;
                // active duration is Kd * the previous frames error - the current frames error / the total time the script has run. 
                Vector3 ad = Kd * (lastError - currentError) / Time.deltaTime;
                //active increase is the totalError * the set Ki 0.1 * the total time
                Vector3 ai = totalError * Ki * Time.deltaTime;
                //sets last error to be the previous current error.
                lastError = currentError;
                //totalerror = totalerror + currenterror
                totalError += currentError;
                //limits the force so the pet doesn't go shooting off.  100 might be too high.
                var navigationForce = Vector3.ClampMagnitude(ap + ai + ad, 100);

                var standardForce = petRB.useGravity ? (Physics.gravity * petRB.mass * -1) : new Vector3(0, 0, 0);

                var finalForce = standardForce - navigationForce;
                petRB.AddForce(finalForce);
            }
        }
    }
}
