using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    QuizData loadedQuiz;

    void OnEnable()
    {
        string jsonString = CreateJSONFiles();
        loadedQuiz = JsonUtility.FromJson<QuizData>(jsonString);
    }

    public QuizData GetData()
    {
        return loadedQuiz;
    }

    public void SaveData(QuizData quiz)
    {
        // save answer data to a text file

        // format: date of the save, question: picked options //
    }

    private string CreateJSONFiles()
    {
        QuizData newQuiz = new QuizData("Терафлю", 1);

        uint questionNum = 0;

        newQuiz.AddQuestion("Терафлю представлен на рынке РФ", questionNum, 4);
        newQuiz.AddOption(questionNum, "Терафлю Экстра Лимон", 0, true);
        newQuiz.AddOption(questionNum, "Терафлю Лесные Ягоды", 1, true);
        newQuiz.AddOption(questionNum, "Терафлю Макс", 2, true);
        newQuiz.AddOption(questionNum, "Терафлю Малина", 3, false);
        newQuiz.AddOption(questionNum, "Терафлю Лимон", 4, true);
        newQuiz.AddOption(questionNum, "Терафлю Ментол", 5, false);

        questionNum++;

        newQuiz.AddQuestion("Терафлю Макс содержит следующую дозировку парацетамола в одном пакетике", questionNum, 1);
        newQuiz.AddOption(questionNum, "1000 мг", 0, true);
        newQuiz.AddOption(questionNum, "650 мг", 1, false);
        newQuiz.AddOption(questionNum, "325 мг", 2, false);
        newQuiz.AddOption(questionNum, "1200 мг", 3, false);

        questionNum++;

        newQuiz.AddQuestion("В состав Терафлю Макс входят действующие вещества", questionNum, 3);
        newQuiz.AddOption(questionNum, "Ацетилсалициловая кислота", 0, false);
        newQuiz.AddOption(questionNum, "Парацетамол", 1, true);
        newQuiz.AddOption(questionNum, "Аскорбиновая кислота", 2, true);
        newQuiz.AddOption(questionNum, "Фенилэфрина гидрохлорид", 3, true);
        newQuiz.AddOption(questionNum, "Фенирамин", 4, false);
        newQuiz.AddOption(questionNum, "Кофеин", 5, false);

        questionNum++; 

        newQuiz.AddQuestion("Самая высокая дозировка парацетамола в линейке Терафлю содержится в", questionNum, 1);
        newQuiz.AddOption(questionNum, "Терафлю Экстра", 0, false);
        newQuiz.AddOption(questionNum, "Терафлю Экстратаб", 1, false);
        newQuiz.AddOption(questionNum, "Терафлю Макс", 2, true);
        newQuiz.AddOption(questionNum, "Терафлю Лесные Ягоды", 3, false);
        newQuiz.AddOption(questionNum, "Терафлю Лимон", 4, false);

        questionNum++;

        newQuiz.AddQuestion("ТераФлю Макс применяется для устранения симптомов простудных заболеваний и гриппа, таких как", questionNum, 1);
        newQuiz.AddOption(questionNum, "повышенная температура, головная боль, озноб, боль в суставах и мышцах, ощущение заложенности носа, боли в пазухах носа и в горле", 0, true);
        newQuiz.AddOption(questionNum, "повышенная температура, озноб, боль в суставах и мышцах", 1, false);
        newQuiz.AddOption(questionNum, "повышенная температура, головная боль, озноб", 2, false);
        newQuiz.AddOption(questionNum, "боль в суставах и мышцах, ощущение заложенности носа, боли в пазухах носа и в горле", 3, false);

        string json = JsonUtility.ToJson(newQuiz);

        Debug.Log(json);

        return json;
    }
}
