using Items.Logic;
using System.Collections;
using UnityEngine;

namespace Items.ItemsLogic
{
    public class SmudgeStick : MonoBehaviour, IMainUsable
    {
        [SerializeField]
        private ParticleSystem _smokeParts;
        public void OnMainUse()
        {
            
        }
    }
}