using UnityEngine;
using UnityEngine.UI;

public class TextFlyUpController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Text _text;

    public void SetTextAndFlyUp(string txt)
    {
        _text.text = txt;
        _animator.CrossFade("FlyUp", 0.1f);
    }
}
