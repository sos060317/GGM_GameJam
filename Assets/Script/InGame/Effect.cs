using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Effect : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private float sec;
    [SerializeField] private bool isRoop;

    [SerializeField] private float shakeAmount = 0;
    [SerializeField] private float shakeTime = 0;

    private SpriteRenderer sr;
    private WaitForSeconds waitTime;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        waitTime = new WaitForSeconds(sec / sprites.Count);

        if (shakeTime > 0)
        {
            GameManager.Instance.CameraShake(shakeAmount, shakeTime);
        }
        

        if (isRoop)
        {
            StartCoroutine(EffectRooping());
        }
        else
        {
            StartCoroutine(EffectStart());
        }
    }

    private IEnumerator EffectRooping()
    {
        int i = 0;

        while (true)
        {
            sr.sprite = sprites[i];
            i++;

            yield return waitTime;

            if (i >= sprites.Count)
            {
                i = 0;
            }
        }
    }

    private IEnumerator EffectStart()
    {
        for (int i = 0; i < sprites.Count; i++)
        {
            sr.sprite = sprites[i];

            yield return waitTime;
        }

        Destroy(gameObject);
    }
}