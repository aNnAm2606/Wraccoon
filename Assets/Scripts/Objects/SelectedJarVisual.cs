using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleectedJarVisual : MonoBehaviour
{

    [SerializeField] private MashmallowJar jar;
    [SerializeField] private GameObject visualGameObject;

    private void Start()
    {
        Player.Instance.OnSelectedJarChanged += Player_OnSelectedJarChanged;
    }

    private void Player_OnSelectedJarChanged(object sender, Player.OnSelectedJarChangedEventArgs e)
    {
       if( e.selectedJar == jar)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        visualGameObject.SetActive(true);
    }

    private void Hide()
    {
        visualGameObject.SetActive(false);
    }
}
