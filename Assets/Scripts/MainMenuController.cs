using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    // camera moving position
    public Transform mainMenuPosition;
    public Transform functionMenuPosition;
    public Transform levelMenuPosition;

    public float transitionSpeed = 2f; // camera transition speed

    bool isTransitioning = false; // check if camera still moving
    public void MoveToMainMenu()
    {
        if (!isTransitioning)
        {
            StartCoroutine(MoveCameraCoroutine1());
        }
    }
    public void MoveToFunctionMenu()
    {
        if (!isTransitioning)
        {
            StartCoroutine(MoveCameraCoroutine2());
        }
    }
    public void MoveToLevelSelectMenu()
    {
        if (!isTransitioning)
        {
            StartCoroutine(MoveCameraCoroutine3());
        }
    }


    IEnumerator MoveCameraCoroutine1()
    {
        isTransitioning = true;

        while (Vector3.Distance(transform.position, mainMenuPosition.position) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, mainMenuPosition.position, transitionSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = mainMenuPosition.position;
        isTransitioning = false;
    }
    IEnumerator MoveCameraCoroutine2()
    {
        isTransitioning = true;

        while (Vector3.Distance(transform.position, functionMenuPosition.position) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, functionMenuPosition.position, transitionSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = functionMenuPosition.position;
        isTransitioning = false;
    }
    IEnumerator MoveCameraCoroutine3()
    {
        isTransitioning = true;

        while (Vector3.Distance(transform.position, levelMenuPosition.position) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, levelMenuPosition.position, transitionSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = levelMenuPosition.position;
        isTransitioning = false;
    }
}
