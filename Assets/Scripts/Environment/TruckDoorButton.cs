using UnityEngine;
using Items.Logic;

public class TruckDoorButton : MonoBehaviour, IClickable
{
    [SerializeField] private Animator _anim;

    private bool _isDoorsOpened = false;
    public void OnClick()
    {
        if (!_isDoorsOpened && !_anim.GetCurrentAnimatorStateInfo(0).IsName("ClosingDoors"))
        {
            _anim.SetTrigger("OpenDoors");
            _isDoorsOpened = true;
        }
        else if (_isDoorsOpened && !_anim.GetCurrentAnimatorStateInfo(0).IsName("OpeningDoors"))
        {
            _anim.SetTrigger("CloseDoors");
            _isDoorsOpened = false;
        }      
    }
}
