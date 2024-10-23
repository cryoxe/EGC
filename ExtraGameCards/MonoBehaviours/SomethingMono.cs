using System;
using EGC.AssetsEmbedded;
using ModdingUtils.MonoBehaviours;
using Photon.Pun;
using Sonigon;
using UnboundLib;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

namespace EGC.MonoBehaviours
{
    internal class SomethingMono : MonoBehaviour
    {
        public SoundEvent soundUpgradeChargeLoop;

        private SoundParameterIntensity soundParameterIntensity =
            new SoundParameterIntensity(0f, UpdateMode.Continuous);

        [Range(0f, 1f)] public float counter;
        public float timeToFill = 5f;
        public float duration = 1;

        public int numberOfSomething = 0;
        public ProceduralImage outerRing;
        public ProceduralImage fill;
        public Transform rotator;
        public Transform still;
        private CharacterData data;
        private float remainingDuration;
        private bool isDeathAuraComplete;
        private float startCounter;
        public CharacterStatModifiers characterStats;

        private DeathMark? deathMark = null;
        private bool isPlayerMarked = false;
        private bool shouldBeDying = false;

        public float defaultRLTime;
        public Player player;

        private AudioSource? somethingNoise;


        public void Start()
        {
            data = GetComponentInParent<CharacterData>();
            HealthHandler healthHandler = data.healthHandler;
            healthHandler.reviveAction = (Action)Delegate.Combine(healthHandler.reviveAction, new Action(ResetStuff));
            GetComponentInParent<ChildRPC>().childRPCs.Add("DeathAura", new Action(RPCA_Activate));
            somethingNoise = gameObject.GetOrAddComponent<AudioSource>();
        }

        public void OnDestroy()
        {
            HealthHandler healthHandler = data.healthHandler;
            healthHandler.reviveAction = (Action)Delegate.Remove(healthHandler.reviveAction, new Action(ResetStuff));
            GetComponentInParent<ChildRPC>().childRPCs.Remove("DeathAura");
            somethingNoise = gameObject.GetOrAddComponent<AudioSource>();
            shouldBeDying = false;
        }

        private void ResetStuff()
        {
            shouldBeDying = false;
            remainingDuration = 0f;
            counter = 0f;
            if (isDeathAuraComplete)
            {
                isDeathAuraComplete = false;
                isDying(false);
            }

            somethingNoise = gameObject.GetOrAddComponent<AudioSource>();
        }

        private void isDying(bool enable)
        {
            if (enable)
            {
                deathMark = player.gameObject.AddComponent<DeathMark>();
                if (somethingNoise != null)
                {
                    somethingNoise.PlayOneShot(Assets.SomethingNoise,
                        0.7f * Optionshandler.vol_Master * Optionshandler.vol_Sfx);
                    somethingNoise = null;
                }

                Unbound.Instance.ExecuteAfterSeconds(4f, delegate
                {
                    if (shouldBeDying)
                    {
                        player.data.view.RPC("RPCA_Die", RpcTarget.All, new object[] { new Vector2(0, 1) });
                    }
                });
            }
            else
            {
                if (deathMark != null)
                {
                    Destroy(deathMark);
                    deathMark = null;
                }
            }
        }

        private void RPCA_Activate()
        {
            remainingDuration = duration;
        }

        private void Update()
        {
            soundParameterIntensity.intensity = counter;
            outerRing.fillAmount = counter;
            fill.fillAmount = counter;
            rotator.transform.localEulerAngles = new Vector3(0f, 0f, -Mathf.Lerp(0f, 360f, counter));

            if (!((bool)data.playerVel.GetFieldValue("simulated")))
            {
                startCounter = 1f;
                return;
            }

            startCounter -= TimeHandler.deltaTime;

            if (startCounter > 0f)
            {
                return;
            }

            if (remainingDuration > 0f)
            {
                if (!isDeathAuraComplete)
                {
                    shouldBeDying = true;
                    isDying(true);
                }

                remainingDuration -= TimeHandler.deltaTime;
                counter = remainingDuration / duration;
                return; //Breaks before futher conditionals
            }

            if (isDeathAuraComplete)
            {
                isDying(false);
            }

            try
            {
                if (data.input.direction == Vector3.zero || data.input.direction == Vector3.down ||
                    (data.input.direction == Vector3.up & data.isGrounded))
                {
                    counter += TimeHandler.deltaTime / timeToFill;
                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log("First Catch");
                UnityEngine.Debug.LogException(e);
            }

            try
            {
                counter = Mathf.Clamp(counter, -0.1f / timeToFill, 1f);
                if (counter >= 1f && data.view.IsMine)
                {
                    remainingDuration = duration;
                    GetComponentInParent<ChildRPC>().CallFunction("DeathAura");
                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log("Second Catch");
                UnityEngine.Debug.LogException(e);
            }

            try
            {
                if (counter <= 0f)
                {
                    rotator.gameObject.SetActive(false);
                    still.gameObject.SetActive(false);
                    return;
                }

                rotator.gameObject.SetActive(true);
                still.gameObject.SetActive(true);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log("Last Catch");
                UnityEngine.Debug.LogException(e);
            }
        }

        public void Destroy()
        {
            ResetStuff();
            Destroy(this);
        }
    }

    public class DeathMark : ReversibleEffect //Thanks and Thanks Pykess for this Utility
    {
        private readonly Color color = Color.black;
        private ReversibleColorEffect colorEffect = null;

        public override void OnOnEnable()
        {
            if (colorEffect != null)
            {
                colorEffect.Destroy();
            }
        }

        public override void OnStart()
        {
            colorEffect = player.gameObject.AddComponent<ReversibleColorEffect>();
            colorEffect.SetColor(color);
            colorEffect.SetLivesToEffect(1);
        }

        public override void OnOnDisable()
        {
            if (colorEffect != null)
            {
                colorEffect.Destroy();
            }
        }

        public override void OnOnDestroy()
        {
            if (colorEffect != null)
            {
                colorEffect.Destroy();
            }
        }
    }
}