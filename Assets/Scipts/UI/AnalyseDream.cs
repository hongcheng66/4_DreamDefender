using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class AnalyseDream : MonoBehaviour
{
    public string firstLevelName;

    public TextMeshProUGUI textComponent; // ����Text���
    public float fadeDuration = 2.0f; // �������ʱ��

    private bool isFadingIn = false; // ����Ƿ����ڽ���
    private Color initialColor; // ��ʼ��ɫ��͸����
    private Color finalColor; // ������ɫ����͸����

    void Start()
    {
        initialColor = textComponent.color;
        initialColor.a = 0.0f; // ���ó�ʼ͸����Ϊ0
        finalColor = textComponent.color;
        finalColor.a = 1.0f; // ��������͸����Ϊ1

        // ��ʼ���붯��
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

            yield return null; // �ȴ���һ֡
        }

        // ȷ����ɫ��ȫ����Ϊ������ɫ����ֹ������
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
