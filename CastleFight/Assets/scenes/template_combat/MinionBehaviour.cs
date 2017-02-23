using System;
using UnityEngine;

namespace Assets.scenes.template_combat
{
    public class MinionBehaviour : MonoBehaviour
    {
        public PlayerIngame PlayerIngame;

        // Use this for initialization
        void Start ()
        {
		
        }
	
        // Update is called once per frame
        void Update ()
        {
		
        }
    }

    /// <summary>
    /// Minion model for Unity's
    /// </summary>
    [Serializable]
    public class MinionModel
    {
        [Range(0f, 10f)]
        public float MoveSpeed;

        [Range(1, 100)]
        public int AttackDamage;

        [Range(1, 300)]
        public int Health;

        public AttackRangeType AttackRange;

        public AttackType AttackType;

        public MinionHomModel BaseStats;
    }

    /// <summary>
    /// Model of minion's stats to improve attack, defence and movespeed.
    /// </summary>
    [Serializable]
    public class MinionHomModel
    {
        public int Attack;

        public int Defence;

        public int BonusSpeed;
    }

    public enum AttackRangeType
    {
        Melee,
        Ranged,
    }

    public enum AttackType
    {
        Physical,
        Magic,
        Godlike,
    }
}
