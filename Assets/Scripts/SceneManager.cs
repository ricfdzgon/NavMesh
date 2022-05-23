using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public MoveTo basico;
    public MoveToFat seguimientoNormal;
    public MoveToFatLimited seguimientoPro;

    public void ButtonBasicoOnClick()
    {
        if (!basico.enabled)
        {
            basico.enabled = true;
            seguimientoNormal.enabled = false;
            seguimientoPro.enabled = false;
        }
    }

    public void ButtonNormalOnClick()
    {
        if (!seguimientoNormal.enabled)
        {
            basico.enabled = false;
            seguimientoNormal.enabled = true;
            seguimientoPro.enabled = false;
        }
    }

    public void ButtonProOnClick()
    {
        if (!seguimientoPro.enabled)
        {
            basico.enabled = false;
            seguimientoNormal.enabled = false;
            seguimientoPro.enabled = true;
        }
    }
}
