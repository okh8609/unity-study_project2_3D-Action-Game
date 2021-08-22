using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ScoreBehaviourScript : MonoBehaviour
{
    public Text Score;
    int _score = 0;


    // Start is called before the first frame update
    void Start()
    {
        Score.text = _score.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        Score.text = _score.ToString();
    }

    public void AddScore(int s)
    {
        _score += s;
    }
}
