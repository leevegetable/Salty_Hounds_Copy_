using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Choice_Needs : MonoBehaviour
{
    public Image[] image = new Image[2];
    public TMP_Text[] value = new TMP_Text[2];

    public void init(Choice option)
    {
        for(int i = 0; i < image.Length; i++)
        {
            //이미지 컨테이너 혹은 파일 규격화
            //image[i].sprite
            //value[i].text = option.RewardsValue[i].ToString();
        }
    }
}
