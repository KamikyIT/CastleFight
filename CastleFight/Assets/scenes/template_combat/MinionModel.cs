using System;
using UnityEngine;

namespace Assets.scenes.template_combat
{
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

        public DefenceType DefenceType;

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

        public int BonusSpeedMultiplier;
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

    public enum DefenceType
    {
        Physical,
        Magic,

    }

    public enum MinionActionState
    {
        Borning,
        Moving,
        Attacking,
        Dying,
    }
}