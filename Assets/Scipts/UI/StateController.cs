using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateController : MonoBehaviour
{
    public static StateController instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private Image awakeCooldownImage;
    [SerializeField] private Image sleepwalkCooldownImage;

    // Update is called once per frame
    void Update()
    {
        CheckCooldownOf(awakeCooldownImage,PlayerController.instance.switchCooldown);
        CheckCooldownOf(sleepwalkCooldownImage,PlayerController.instance.switchCooldown);
    }

    public void SetCooldownOf()
    {
        if (awakeCooldownImage.fillAmount <= 0)
            awakeCooldownImage.fillAmount = 1;
        if (sleepwalkCooldownImage.fillAmount <= 0)
            sleepwalkCooldownImage.fillAmount = 1;
    }

    private void CheckCooldownOf(Image _image, float _cooldown)
    {
        if (_image.fillAmount > 0)
        {
            _image.fillAmount -= 1 / _cooldown * Time.deltaTime;
        }
    }
}
