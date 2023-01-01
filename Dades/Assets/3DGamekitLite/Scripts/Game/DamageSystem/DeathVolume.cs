using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamekit3D
{
    [RequireComponent(typeof(Collider))]
    public class DeathVolume : MonoBehaviour
    {
        public new AudioSource audio;
        private Our_Code ourCode;
        private bool once;
        private void Awake()
        {
            ourCode = GameObject.Find("Our_Code").GetComponent<Our_Code>();
            once = false;
        }

        void OnTriggerEnter(Collider other)
        {
            var pc = other.GetComponent<PlayerController>();
            if (pc != null)
            {
                if (!once)
                {
                    ourCode.GetDeathPositionByLava(Time.time);
                    once = true;
                }
                StartCoroutine(Delay());
                pc.Die(new Damageable.DamageMessage());
            }
            if (audio != null)
            {
                audio.transform.position = other.transform.position;
                if (!audio.isPlaying)
                    audio.Play();
            }
        }

        void Reset()
        {
            if (LayerMask.LayerToName(gameObject.layer) == "Default")
                gameObject.layer = LayerMask.NameToLayer("Environment");
            var c = GetComponent<Collider>();
            if (c != null)
                c.isTrigger = true;
        }

        public IEnumerator Delay()
        {
            yield return new WaitForSeconds(0.2f);
            once = false;
        }

    }
}
