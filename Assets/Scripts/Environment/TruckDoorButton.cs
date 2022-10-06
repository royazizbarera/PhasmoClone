using UnityEngine;
using Items.Logic;

public class TruckDoorButton : MonoBehaviour, IClickable
{
    [SerializeField] private Animator _anim;

    [SerializeField] private AudioSource _doorAudio;
    [SerializeField] private AudioSource _engineAudio;

    private bool _isDoorsOpened = false;
    public void OnClick()
    {
        if (!_isDoorsOpened && !_anim.GetCurrentAnimatorStateInfo(0).IsName("ClosingDoors"))
        {
            PlayDoorsSound();
            _anim.SetTrigger("OpenDoors");
            _isDoorsOpened = true;
        }
        else if (_isDoorsOpened && !_anim.GetCurrentAnimatorStateInfo(0).IsName("OpeningDoors"))
        {
            PlayDoorsSound();
            Invoke(nameof(PlayEngineSound), 2f);
            _anim.SetTrigger("CloseDoors");
            _isDoorsOpened = false;
        }      
    }

    private void PlayDoorsSound()
    {
        _doorAudio.Play();
    }
    private void PlayEngineSound()
    {
        _engineAudio.Play();
    }
}
