using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PawnButton : MonoBehaviour
{

    [SerializeField, HideInInspector]
    public GameObject pawn;

    [SerializeField, HideInInspector]
    private TMP_Text priorityText;

    // Start is called before the first frame update
    void Start()
    {
        var textObj = GameObject.Find("Text");
        priorityText = textObj.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
}
