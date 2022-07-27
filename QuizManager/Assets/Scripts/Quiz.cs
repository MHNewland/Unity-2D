using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] QuestionSO[] questions;
    [SerializeField] TextMeshProUGUI questionText;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    [SerializeField] Sprite selectedAnswerSprite;
    [SerializeField] Sprite CorrectAnswerSprite;
    [SerializeField] Sprite DefaultAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerCircle;
    Timer timer;

    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;

    int correctAnswerIndex;
    int questionIndex;

    [SerializeField]
    float numberCorrect;

    public float finalScore { get; private set; }

    GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        DisplayQuestions();
        questionIndex = 0;
        numberCorrect = 0;
        timer = FindObjectOfType<Timer>();
        timer.isAnsweringQuestion = true;
        timer.ResetTimer();
    }

    private void Update()
    {
        timerCircle.fillAmount = timer.timerFraction;
        if (timer.timerValue < 0)
        {
            if (timer.isAnsweringQuestion)
            {
                ShowCorrectAnswer(-1, null);
            }
            else
            {
                GetNextQuestion();
            }
        }
    }

    public void AnswerSelect(int index)
    {
        Image buttonImage = answerButtons[index].GetComponent<Image>();
        ShowCorrectAnswer(index, buttonImage);

    }

    private void ShowCorrectAnswer(int index, Image buttonImage)
    {
        correctAnswerIndex = questions[questionIndex].GetCorrectAnswerIndex();
        timer.isAnsweringQuestion = false;
        timer.ResetTimer();
        SetButtonState(false);

        if (index == correctAnswerIndex)
        {
            questionText.text = "Correct!";
            if(buttonImage!=null)buttonImage.sprite = CorrectAnswerSprite;
            numberCorrect++;
        }
        else
        {
            questionText.text = "Sorry, the correct answer was: " + questions[questionIndex].GetCorrectAnswer();
            if(buttonImage!=null) buttonImage.sprite = selectedAnswerSprite;
            answerButtons[correctAnswerIndex].GetComponent<Image>().sprite = CorrectAnswerSprite;
        }
    }

    void GetNextQuestion()
    {
        if (questionIndex < questions.Length - 1)
        {
            questionIndex++;
            DisplayQuestions();
            foreach (GameObject button in answerButtons)
            {
                button.GetComponent<Image>().sprite = DefaultAnswerSprite;
            }
            SetButtonState(true);
            timer.isAnsweringQuestion = true;
            timer.ResetTimer();
        }
        else
        {
            timer.isAnsweringQuestion = false;
            timer.CancelTimer();
            FinalTally();
        }

    }

    private void FinalTally()
    {
        finalScore = (numberCorrect / questions.Length);
        gm.EndGame(finalScore);
    }

    void DisplayQuestions()
    {
        progressBar.value = (float)(questionIndex + 1) / questions.Length;
        questionText.text = questions[questionIndex].GetQuestion();
        for (int i = 0; i < 4; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = questions[questionIndex].GetAnswer(i);
        }
    }

    void SetButtonState(bool state)
    {
        foreach(GameObject button in answerButtons)
        {
            button.GetComponent<Button>().interactable = state;
        }
    }
}
