using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TableView : MonoBehaviour
{
    public TextMeshProUGUI communityCardText;

    public void ClearCommunityCardTextView()
    {
        communityCardText.text = string.Empty;
    }
}
