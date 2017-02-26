using System;
using System.Collections;
using System.Net;
using Assets.scenes.common_scripts;
using UnityEngine;

namespace Assets.scenes.template_combat
{
    public class MinionBehaviour : MonoBehaviour
    {
        public PlayerIngame PlayerIngame;

        public MinionModel Model;

        public LayerMask CreepsLayer;

        public MinionActionState state;
        public MinionActionState State
        {
            get { return state; }
            set
            {
                state = value;
                switch (state)
                {
                    case MinionActionState.Borning:
                        Action = Borning;
                        break;
                    case MinionActionState.Moving:
                        Action = Moving;
                        break;
                    case MinionActionState.Attacking:
                        Action = Attacking;
                        break;
                    case MinionActionState.Dying:
                        Action = Dying;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        // Use this for initialization
        void Awake()
        {
            State = MinionActionState.Borning;

            busy = false;

            attack_distance = Model.AttackRange == AttackRangeType.Melee ? 2f : 10f;

            damageCameraScript = GameObject.FindObjectOfType<DamageCameraScript>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Action != null) Action();
        }

        public void Damaged(MinionBehaviour attacker)
        {
            this.GetDamage(attacker.Model.AttackDamage);
        }


        private void Borning()
        {
            if (busy)
                return;

            if (anim != null) anim.Play(AnimationProtocol.GetName(AnimationStatesEnum.Borning));

            StartCoroutine(BorningCor());
        }

        public IEnumerator BorningCor()
        {
            yield return new WaitForSeconds(AnimationProtocol.GetTimeDuration(AnimationStatesEnum.Borning));

            Action = Moving;
        }

        private void Moving()
        {
            if (anim != null) anim.Play("moving");

            // if find enemy, attack him, and dont move.
            if (CheckAttack())
            {
                State = MinionActionState.Attacking;

                return;
            }
            // else go forward.

            var dMove = Model.MoveSpeed;

            var bonusSpeedMultiplier = 1f + Model.BaseStats.BonusSpeedMultiplier;

            dMove *= bonusSpeedMultiplier;

            dMove *= Time.deltaTime;

            var dir = Vector3.right * (transform.localScale.x > 0? 1f: -1f);

            transform.Translate(dir * dMove, Space.Self);

            Debug.Log(gameObject.name + " moving");
        }

        /// <summary>
        /// Check enemy is in attack range.
        /// </summary>
        /// <returns>Returns true, if found enemy to attack.</returns>
        private bool CheckAttack()
        {
            var dir = Vector3.right * (transform.localScale.x > 0 ? 1f : -1f);

            Debug.Log(name + " lc " + dir * attack_distance);

            var lc = Physics2D.LinecastAll(transform.position, transform.position + dir*attack_distance,
                CreepsLayer);

            Debug.DrawLine(transform.position, transform.position + dir * attack_distance, Color.red, 0.3f);

            // if no enemies detected
            if (lc == null || lc.Length == 0)
            {
                Debug.Log("lc == null");
                return false;
            }
            else
            {
                Debug.Log("lc FOUND");
            }

            foreach (var minion in lc)
            {
                if (minion.collider == null || minion.collider.gameObject == null)
                    continue;
                
                var enemyMinionBehaviour = minion.collider.gameObject.GetComponent<MinionBehaviour>();

                if (enemyMinionBehaviour == null)
                    continue;

                if (enemyMinionBehaviour.PlayerIngame == PlayerIngame)
                    continue;

                Target = enemyMinionBehaviour;

                //TODO: Recalculation of damage, based on 1) attacker's attack factor; 2) victim's defence; 3) crits, stuns, dodge etc.
                //var damageMade = this.Model.AttackDamage;
                //enemyMinionBehaviour.GetDamage(damageMade);
                
                return true;
                // TODO: Check for barrier
            }

            return false;
        }

        private void Attacking()
        {
            if (busy_attacking)
            {
                return;
            }

            busy_attacking = true;

            if (anim != null) anim.Play(AnimationProtocol.GetName(AnimationStatesEnum.Attacking));

            // Melee attack
            if (Model.AttackRange == AttackRangeType.Melee)
            {
                if (Target)
                {
                    Target.Damaged(this);
                }
            }
            // Range attack, create bullet/arrow at fire it.
            else
            {
                
            }

            StartCoroutine(AttackingCor());

            Debug.Log(gameObject.name + " attacking");
        }

        private IEnumerator AttackingCor()
        {
            yield return new WaitForSeconds(AnimationProtocol.GetTimeDuration(AnimationStatesEnum.Attacking));

            busy_attacking = false;

            Action = Moving;
        }

        private void Dying()
        {
            if (busy)
            {
                return;
            }

            if (anim != null) anim.Play(AnimationProtocol.GetName(AnimationStatesEnum.Dying));

            StartCoroutine(DyingCor());

            busy = true;


            GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }

        private IEnumerator DyingCor()
        {
            yield return new WaitForSeconds(AnimationProtocol.GetTimeDuration(AnimationStatesEnum.Dying));

            Destroy(gameObject);
        }

        private void GetDamage(int damageMade)
        {
            Model.Health -= damageMade;

            if (Model.Health <= 0)
            {
                State = MinionActionState.Dying;
            }

            damageCameraScript.CreateDamageText(gameObject, damageMade);
        }


        private Action Action;
        private Animator anim;
        private bool busy;
        private bool busy_attacking;

        private DamageCameraScript damageCameraScript;

        private MinionBehaviour Target;

        private float attack_distance;
    }
}
