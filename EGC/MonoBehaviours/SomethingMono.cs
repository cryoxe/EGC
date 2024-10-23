using UnityEngine;
using ModdingUtils.MonoBehaviours;
using System;
using Sonigon;
using UnityEngine.UI.ProceduralImage;
using Sonigon.Internal;
using Photon.Pun;
using ExtraGameCards.AssetsEmbedded;
using UnboundLib;

namespace ExtraGameCards.MonoBehaviours
{
    internal class SomethingMono : MonoBehaviour
    {
        public SoundEvent soundUpgradeChargeLoop;

        private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0f, UpdateMode.Continuous);

        [Range(0f, 1f)]
        public float counter;
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
            this.data = base.GetComponentInParent<CharacterData>();
            HealthHandler healthHandler = this.data.healthHandler;
            healthHandler.reviveAction = (Action)Delegate.Combine(healthHandler.reviveAction, new Action(this.ResetStuff));
            base.GetComponentInParent<ChildRPC>().childRPCs.Add("DeathAura", new Action(this.RPCA_Activate));
            somethingNoise = gameObject.GetOrAddComponent<AudioSource>();
        }

        public void OnDestroy()
        {
            HealthHandler healthHandler = this.data.healthHandler;
            healthHandler.reviveAction = (Action)Delegate.Remove(healthHandler.reviveAction, new Action(this.ResetStuff));
            base.GetComponentInParent<ChildRPC>().childRPCs.Remove("DeathAura");
            somethingNoise = gameObject.GetOrAddComponent<AudioSource>();
            this.shouldBeDying = false;
        }

        private void ResetStuff()
        {
            this.shouldBeDying = false;
            this.remainingDuration = 0f;
            this.counter = 0f;
            if (this.isDeathAuraComplete)
            {
                this.isDeathAuraComplete = false;
                this.isDying(false);
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
                    somethingNoise.PlayOneShot(Assets.SomethingNoise, 0.7f * Optionshandler.vol_Master * Optionshandler.vol_Sfx);
                    somethingNoise = null;
                }
                Unbound.Instance.ExecuteAfterSeconds(4f, delegate
                {
                    if (shouldBeDying) { player.data.view.RPC("RPCA_Die", RpcTarget.All, new object[] {new Vector2(0, 1)}); }
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
            this.remainingDuration = this.duration;
        }

        private void Update()
        {
            this.soundParameterIntensity.intensity = this.counter;
            this.outerRing.fillAmount = this.counter;
            this.fill.fillAmount = this.counter;
            this.rotator.transform.localEulerAngles = new Vector3(0f, 0f, -Mathf.Lerp(0f, 360f, this.counter));

            if (!((bool)this.data.playerVel.GetFieldValue("simulated")))
            {
                this.startCounter = 1f;
                return;
            }

            this.startCounter -= TimeHandler.deltaTime;

            if (this.startCounter > 0f)
            {
                return;
            }

            if (this.remainingDuration > 0f)
            {
                if (!this.isDeathAuraComplete)
                {
                    shouldBeDying = true;
                    this.isDying(true);

                }
                this.remainingDuration -= TimeHandler.deltaTime;
                this.counter = this.remainingDuration / this.duration;
                return; //Breaks before futher conditionals
            }

            if (this.isDeathAuraComplete)
            {
                this.isDying(false);
            }

            try
            {
                if (this.data.input.direction == Vector3.zero || this.data.input.direction == Vector3.down || (this.data.input.direction == Vector3.up & this.data.isGrounded))
                {
                    this.counter += TimeHandler.deltaTime / this.timeToFill;
                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log("First Catch");
                UnityEngine.Debug.LogException(e);
            }

            try
            {
                this.counter = Mathf.Clamp(this.counter, -0.1f / this.timeToFill, 1f);
                if (this.counter >= 1f && this.data.view.IsMine)
                {
                    this.remainingDuration = this.duration;
                    base.GetComponentInParent<ChildRPC>().CallFunction("DeathAura");
                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log("Second Catch");
                UnityEngine.Debug.LogException(e);
            }

            try
            {
                if (this.counter <= 0f)
                {
                    this.rotator.gameObject.SetActive(false);
                    this.still.gameObject.SetActive(false);
                    return;
                }
                this.rotator.gameObject.SetActive(true);
                this.still.gameObject.SetActive(true);
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

            if (this.colorEffect != null)
            {
                this.colorEffect.Destroy();
            }
        }
        public override void OnStart()
        {
            this.colorEffect = base.player.gameObject.AddComponent<ReversibleColorEffect>();
            this.colorEffect.SetColor(this.color);
            this.colorEffect.SetLivesToEffect(1);
        }
        public override void OnOnDisable()
        {
            if (this.colorEffect != null)
            {
                this.colorEffect.Destroy();
            }
        }
        public override void OnOnDestroy()
        {
            if (this.colorEffect != null)
            {
                this.colorEffect.Destroy();
            }
        }


    }

}

