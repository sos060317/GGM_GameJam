using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public Player curPlayer;

    public int gold;

    public bool isStop;

    [SerializeField] private SkillPanel qSkillPanel;
    [SerializeField] private SkillPanel eSkillPanel;

    [SerializeField] private CameraShake cameraShake;

    [SerializeField] private Image effectImage;

    [SerializeField] private Image healthImage;
    [SerializeField] private TextMeshProUGUI healthText;

    public PlayerLevelData[] playerLevelDatas;

    public float playerHealthMultiply;

    #region 게임오버 관련

    [Space(10)]
    [Header("게임오버 관련")]
    [SerializeField] private Image gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI restartButton;
    [SerializeField] private TextMeshProUGUI titleButton;

    #endregion

    [HideInInspector] public int curPlayerLevel = 0;

    private SkillBase qSkill;
    private SkillBase eSkill;

    private SkillDetails qSkillDetails;
    private SkillDetails eSkillDetails;

    public PlayableDirector playableDirector;

    private float qSkillTimer;
    private float eSkillTimer;

    [HideInInspector] public bool isCanESkill = false;       // E 스킬 해금 관련 변수
    private bool isEmptyQSkill = true;
    private bool isEmptyESkill = true;

    [HideInInspector] public Oxygen oxygen = null;

    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        qSkillPanel.Init(null);
        eSkillPanel.Init(null);

        eSkillPanel.gameObject.SetActive(false);

        gameOverPanel.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);

        restartButton.gameObject.SetActive(false);
        titleButton.gameObject.SetActive(false);

        playableDirector = GetComponent<PlayableDirector>();
    }

    private void Update()
    {
        SkillInputUpdate();
        SkillCooltimeUpdate();
    }

    private void SkillInputUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q) && qSkill != null && qSkillTimer > qSkillDetails.coolTime)
        {
            qSkill.UseSkill(qSkillDetails);
            qSkillTimer = 0;

            if (oxygen)
                oxygen.AmountDown(15.0f);
        }

        if (Input.GetKeyDown(KeyCode.E) && eSkill != null && eSkillTimer > eSkillDetails.coolTime)
        {
            eSkill.UseSkill(eSkillDetails);
            eSkillTimer = 0;

            if (oxygen)
                oxygen.AmountDown(15.0f);
        }
    }

    private void SkillCooltimeUpdate()
    {
        if (qSkill != null)
        {
            qSkillPanel.UpdateFillAmount(qSkillTimer / qSkillDetails.coolTime);

            qSkillTimer += Time.deltaTime;
        }

        if (eSkill != null)
        {
            eSkillPanel.UpdateFillAmount(eSkillTimer / eSkillDetails.coolTime);

            eSkillTimer += Time.deltaTime;
        }
    }

    public void GetSkillItem(SkillDetails skillDetails, GameObject item)
    {
        // Q 스킬과 E 스킬이 꽉 차있다면 리턴
        if (!isEmptyESkill && (!isEmptyQSkill || !isCanESkill))
        {
            Debug.Log("꽉 참");

            return;
        }

        // Q 스킬이 비어있다면
        if (isEmptyQSkill)
        {
            qSkillDetails = skillDetails;
            qSkill = Instantiate(qSkillDetails.skillPrefab);
            qSkillPanel.Init(qSkillDetails);
            qSkillTimer = qSkillDetails.coolTime;
            isEmptyQSkill = false;
            Destroy(item);
            return;
        }

        // E 스킬이 비어있다면
        if (isEmptyESkill && isCanESkill)
        {
            eSkillDetails = skillDetails;
            eSkill = Instantiate(eSkillDetails.skillPrefab);
            eSkillPanel.Init(eSkillDetails);
            eSkillTimer = eSkillDetails.coolTime;
            isEmptyESkill = false;
            Destroy(item);
            return;
        }
    }

    public void CameraShake(float intensity, float time)
    {
        cameraShake.ShakeCamera(intensity, time);
    }

    Coroutine effectImageRoutine;
    public void ShowEffectImage(float time, float fadeAmount)
    {
        if (effectImageRoutine != null)
        {
            StopCoroutine(effectImageRoutine);
        }

        effectImageRoutine = StartCoroutine(FadeOutObjectEffect(effectImage, time, fadeAmount));
    }

    private IEnumerator FadeOutObjectEffect(Image _image, float time, float fadeAmount)
    {
        if (time == 0)
        {
            yield return null;
        }

        float targetAlpha = fadeAmount;
        float curAlpha = 0;
        float temp = 0;

        float fadeInOutTime = time / 2;

        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, curAlpha);

        while (temp <= fadeInOutTime)
        {
            curAlpha += Time.deltaTime * targetAlpha / fadeInOutTime;

            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, curAlpha);

            temp += Time.deltaTime;

            yield return null;
        }

        curAlpha = fadeAmount;

        temp = 0;

        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, curAlpha);

        while (temp <= fadeInOutTime)
        {
            curAlpha -= Time.deltaTime / fadeInOutTime;

            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, curAlpha);

            temp += Time.deltaTime;

            yield return null;
        }
    }

    public void SkillCooltimeReset(SkillBase skillBase)
    {
        if (qSkill == skillBase)
        {
            qSkillTimer = qSkillDetails.coolTime;
        }
        if (eSkill == skillBase)
        {
            eSkillTimer = eSkillDetails.coolTime;
        }
    }

    public void OxygenDown(float amount)
    {
        if (oxygen == null)
        {
            return;
        }

        oxygen.AmountDown(amount);
    }

    public void PlayerLevelUp()
    {
        curPlayer.Init(playerLevelDatas[curPlayerLevel]);

        curPlayerLevel++;
    }

    public void SkillSlotUpgrade()
    {
        if (isCanESkill)
        {
            return;
        }

        isCanESkill = true;

        eSkillPanel.gameObject.SetActive(true);

        eSkillPanel.Init(null);
    }

    public void SetHealthUI(float curHealth, float maxHealth)
    {
        healthText.text = Mathf.Ceil(curHealth).ToString();

        healthImage.fillAmount = curHealth / maxHealth;
    }

    public void GameOver()
    {
        isStop = true;

        StartCoroutine(GameOverRoutine());
    }

    private IEnumerator GameOverRoutine()
    {
        // 게임 오버 텍스트 띄우기
        gameOverPanel.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);

        StartCoroutine(FadeOutObject(gameOverPanel, 1));
        yield return FadeOutObject(gameOverText, 1);

        //재시작 버튼 활성화
        restartButton.gameObject.SetActive(true);

        yield return FadeOutObject(restartButton, 1);

        //타이틀 버튼 활성화
        titleButton.gameObject.SetActive(true);

        yield return FadeOutObject(titleButton, 1);
    }

    private IEnumerator FadeOutObject(Image _image, float time)
    {
        if (time == 0)
        {
            yield return null;
        }

        float targetAlpha = _image.color.a;
        float curAlpha = 0;
        float temp = 0;

        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, curAlpha);

        while (temp <= time)
        {
            curAlpha += Time.deltaTime * targetAlpha / time;

            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, curAlpha);

            temp += Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator FadeOutObject(Image _image, float time, float fadeAmount)
    {
        if (time == 0)
        {
            yield return null;
        }

        float targetAlpha = fadeAmount;
        float curAlpha = 0;
        float temp = 0;

        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, curAlpha);

        while (temp <= time)
        {
            curAlpha += Time.deltaTime * targetAlpha / time;

            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, curAlpha);

            temp += Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator FadeOutObject(TextMeshProUGUI _text, float time)
    {
        if (time == 0)
        {
            yield return null;
        }

        float targetAlpha = _text.color.a;
        float curAlpha = 0;
        float temp = 0;

        _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, curAlpha);

        while (temp <= time)
        {
            curAlpha += Time.deltaTime * targetAlpha / time;

            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, curAlpha);

            temp += Time.deltaTime;

            yield return null;
        }
    }

    public void PlayBossCutscene()
    {
        playableDirector.Play();
    }
}