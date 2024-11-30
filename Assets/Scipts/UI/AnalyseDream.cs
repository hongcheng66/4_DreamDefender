using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class AnalyseDream : MonoBehaviour
{
    public string firstLevelName;

    public TextMeshProUGUI textComponent; // 引用Text组件
    public float fadeDuration = 2.0f; // 渐入持续时间

    private bool isFadingIn = false; // 标记是否正在渐入
    private Color initialColor; // 初始颜色（透明）
    private Color finalColor; // 最终颜色（不透明）

    void Start()
    {
        initialColor = textComponent.color;
        initialColor.a = 0.0f; // 设置初始透明度为0
        finalColor = textComponent.color;
        finalColor.a = 1.0f; // 设置最终透明度为1

        // 开始渐入动画
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        isFadingIn = true;
        float elapsed = 0.0f;

        while (elapsed < fadeDuration && isFadingIn)
        {
            elapsed += Time.deltaTime;
            float fadeFactor = elapsed / fadeDuration;
            textComponent.color = Color.Lerp(initialColor, finalColor, fadeFactor);

            yield return null; // 等待下一帧
        }

        // 确保颜色完全设置为最终颜色（防止浮点误差）
        textComponent.color = finalColor;
    }

    public void ChooseWeapon1()
    {
        PlayerPrefs.SetInt("SavedValue", 0);
        SceneManager.LoadScene(firstLevelName);

    }

    public void ChooseWeapon2()
    {
        PlayerPrefs.SetInt("SavedValue", 1);
        SceneManager.LoadScene(firstLevelName);

    }
    public void ChooseWeapon3()
    {
        PlayerPrefs.SetInt("SavedValue", 2);
        SceneManager.LoadScene(firstLevelName);
    }

    public void ChooseWeapon4()
    {
        PlayerPrefs.SetInt("SavedValue", 3);
        SceneManager.LoadScene(firstLevelName);
    }
}
