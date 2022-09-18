using System;
using System.Collections;
using UnityEngine;
using Utilities;
using Utilities.Constants;

namespace Ghosts.GhostMood
{
    public class AttackChecker : MonoBehaviour
    {
        private const int MinAttackDurationDivider = 6;
        private const int MaxAttackDurationDivider = 3;
        [SerializeField]
        private GhostInfo _ghostInfo;
        [SerializeField]
        private GhostMood _ghostMood;

        [SerializeField]
        private float _ghostAttackCheckCD = 15f;

        private bool _attackInCD = false;
        private GhostDataSO _ghostData;

        private LevelSizeConst.LevelSize _levelSize;

        private float _minHuntDuration;
        private float _maxHuntDuration;

        private SanityHandler _playerSanity;
        private WaitForSeconds GhostAttackCheckCD;
        void Start()
        {
            if (_ghostInfo.SetedUp) SetUp();
            else _ghostInfo.GhostSetedUp += SetUp;

            GhostAttackCheckCD = new WaitForSeconds(_ghostAttackCheckCD);
        }

        public void MakeGhostHunt()
        {
            StartHunting(CalculateAttackTime());
        }


        private IEnumerator CheckForAttackInum()
        {
            while (true)
            {
                yield return GhostAttackCheckCD;
                CheckForAttack();
            }
        }

        private void CheckForAttack()
        {
            if (_attackInCD) return;

            if (ShouldHunt())
            {
                StartHunting(CalculateAttackTime());
            }
        }

        private float CalculateAttackTime()
        {
            return UnityEngine.Random.Range(_minHuntDuration + _ghostInfo.FinalGhostAnger / MinAttackDurationDivider, _maxHuntDuration + _ghostInfo.FinalGhostAnger / MaxAttackDurationDivider);
        }

        private void StartHunting(float attackTime)
        {
            _attackInCD = true;
            _ghostMood.StartHunting(attackTime);
            Invoke(nameof(ReloadAttackCD), _ghostData.MinAttackCD);
        }

        private void ReloadAttackCD()
        {
            _attackInCD = false;
        }

        private bool ShouldHunt()
        {
            if (_playerSanity.Sanity < _ghostInfo.GhostData.MinSanityToAttack)
            {
                float attackChance = _ghostInfo.FinalGhostAnger * _ghostData.AttackChanceCoef;

                if (RandomGenerator.CalculateChance(attackChance) == true) return true;
            }
            return false;
        }

        private void SetUp()
        {
            _playerSanity = _ghostInfo.PlayerSanity;
            _levelSize = _ghostInfo.LevelSize;
            _ghostData = _ghostInfo.GhostData;

            _minHuntDuration = LevelSizeConst.MapMinHuntDuration(_levelSize.ToString());
            _maxHuntDuration = LevelSizeConst.MapMaxHuntDuration(_levelSize.ToString());

            StartCoroutine(nameof(CheckForAttackInum));
        }


    }
}