using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField] GameObject quizTable;
    [SerializeField] TextMeshProUGUI textMeshQuestion;
    [SerializeField] TextMeshProUGUI textMeshA;
    [SerializeField] TextMeshProUGUI textMeshB;
    [SerializeField] TextMeshProUGUI textMeshC;
    [SerializeField] TextMeshProUGUI textMeshD;
    [SerializeField] Button answer1Button;
    [SerializeField] Button answer2Button;
    [SerializeField] Button answer3Button;
    [SerializeField] Button answer4Button;
    [SerializeField] int timeToAnswer = 15;
    private string result = "A";
    private DateTime FailAnswer = DateTime.Now;
    private bool status = true;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (status)
        {
            ToggleQuiz();
        }
        else
        {
            GameManager.instance.nofification.Show($"Please come back later here ({FailAnswer.ToString("HH:mm")})");
        }
    }
    void Update()
    {
        if (DateTime.UtcNow == FailAnswer)
        {
            status = true;
        }
    }
    public void ToggleQuiz()
    {
        if (quizTable != null)
        {
            if (!quizTable.activeSelf)
            {
                GameManager.instance.player.SetMove(false);
                quizTable.SetActive(true);
                GetQuestion();
                answer1Button.onClick.AddListener(Answer1);
                answer2Button.onClick.AddListener(Answer2);
                answer3Button.onClick.AddListener(Answer3);
                answer4Button.onClick.AddListener(Answer4);
                StartCoroutine(AnswerQuestionDelay());
            }
            else
            {
                quizTable.SetActive(false);
            }
        }
    }

    private IEnumerator AnswerQuestionDelay()
    {
        yield return new WaitForSeconds(timeToAnswer);
        FailAnswer.AddMinutes(5);
        status = false;
        quizTable.SetActive(false);
        GameManager.instance.player.SetMove(true);
    }

    void GetQuestion()
    {
        textMeshQuestion.text = "Con j ngu hat";
        textMeshA.text = "ngu1";
        textMeshB.text = "ngu2";
        textMeshC.text = "ngu3";
        textMeshD.text = "ngu4";
        result = "ngu1";
    }
    public void Answer1()
    {
        if (textMeshA.text == result)
        {
            GameManager.instance.player.SellItemStore(UnityEngine.Random.Range(0, 100));
        }
        else
        {
            GameManager.instance.nofification.Show("Incorrect answer!");
        }
        FailAnswer.AddMinutes(5);
        status = false;
        quizTable.SetActive(false);
        GameManager.instance.player.SetMove(true);
    }
    public void Answer2()
    {
        if (textMeshB.text == result)
        {
            GameManager.instance.player.SellItemStore(UnityEngine.Random.Range(0, 100));
        }
        else
        {
            GameManager.instance.nofification.Show("Incorrect answer!");
        }
        FailAnswer.AddMinutes(5);
        status = false;
        quizTable.SetActive(false);
        GameManager.instance.player.SetMove(true);
    }
    public void Answer3()
    {
        if (textMeshC.text == result)
        {
            GameManager.instance.player.SellItemStore(UnityEngine.Random.Range(0, 100));
        }
        else
        {
            GameManager.instance.nofification.Show("Incorrect answer!");
        }
        FailAnswer.AddMinutes(5);
        status = false;
        quizTable.SetActive(false);
        GameManager.instance.player.SetMove(true);
    }
    public void Answer4()
    {
        if (textMeshD.text == result)
        {
            GameManager.instance.player.SellItemStore(UnityEngine.Random.Range(0, 100));
        }
        else
        {
            GameManager.instance.nofification.Show("Incorrect answer!");
        }
        FailAnswer.AddMinutes(5);
        status = false;
        quizTable.SetActive(false);
        GameManager.instance.player.SetMove(true);
    }
}
