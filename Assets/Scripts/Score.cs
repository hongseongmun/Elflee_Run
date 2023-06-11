using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    public string playerName = null;

    public TextMeshProUGUI scoreTmp;
    public TextMeshProUGUI[] bestNameTexts ;
    public TextMeshProUGUI[] bestScoreTexts;

    float[] bestScore = new float[10];
    string[] bestName = new string[10];

    public GameObject nameField;
    public GameObject RankingMenu;

    bool ok;

    private void Start()
    {
        ok = true;
    }

    public void SetName()
    {
        if (ok)
        {
            nameField.SetActive(true);
        }
        ok = false;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            InputName();
        }
    }

    public void InputName()
    {
        playerName = playerNameInput.text;
        PlayerPrefs.SetString("CurrentPlayerName", playerName);
        ScoreSet(GameManager.score, playerName);
    }

    public void ScoreTmp()
    {
        if (GameManager.isTitle)
        {
            scoreTmp.text = " ";
        }
        else if (GameManager.isPlay)
        {
            scoreTmp.text = "Score : " + (int)GameManager.score;
        }

    }

    //플레이어 점수와 이름
    public void ScoreSet(float currentScore, string currentName)
    {
        //일단 플레이어 점수와 이름을 저장
        PlayerPrefs.SetString("CurrentPlayerName", currentName);
        PlayerPrefs.SetFloat("CurrentPlayerScore", currentScore);

        float tmpScore = 0f;
        string tmpName = "";

        for(int i =0; i<10; i++)
        {
            //점수와 이름 불러오기
            bestScore[i] = PlayerPrefs.GetFloat(i + "BestScore");
            bestName[i] = PlayerPrefs.GetString(i + "BestName");

            //랭킹에 변동이 있을경우
            while (bestScore[i] < currentScore)
            {
                //배열에 위치 변경
                tmpScore = bestScore[i];
                tmpName = bestName[i];
                bestScore[i] = currentScore;
                bestName[i] = currentName;

                //랭킹 저장
                PlayerPrefs.SetFloat(i + "BestScore", currentScore);
                PlayerPrefs.SetString(i.ToString() + "BestName", currentName);

                //다음 반복을 위한 셋팅
                currentScore = tmpScore;
                currentName = tmpName;
            }
        }
        //랭킹에 맞게 정렬해주는 부분
        for(int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetFloat(i + "BestScore", bestScore[i]);
            PlayerPrefs.SetString(i.ToString() + "BestName", bestName[i]);
        }
        nameField.SetActive(false);
        RankingOn();

    }

    public void RankingOn()
    {
        for(int i = 0; i < 10; i++)
        {
            bestScore[i] = PlayerPrefs.GetFloat(i + "BestScore");
            bestScoreTexts[i].text = string.Format("{0:N3}cm", bestScore[i]);
            bestName[i] = PlayerPrefs.GetString(i.ToString() + "BestName");
            bestNameTexts[i].text = string.Format(bestName[i]);
        }
        RankingMenu.SetActive(true);
    }
}