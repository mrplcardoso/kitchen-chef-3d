using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    //Referência para o botão start
    [SerializeField] Button start;

    void Start()
    {
        TransitionScreen.instance.TransitionOff();
        //Adiciona o método NextScene()
        //como reação ao click do botão 'Start'
        start.onClick.
            AddListener(NextScene);
    }

    void NextScene()
    {
        TransitionScreen.instance.TransitionOn();
    }
}
