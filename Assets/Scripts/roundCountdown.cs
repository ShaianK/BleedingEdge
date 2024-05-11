using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roundCountdown : MonoBehaviour
{
    public GameObject numberOne;
    public GameObject numberTwo;
    public GameObject numberThree;
    public GameObject mainCount;

    // Start is called before the first frame update
    void Start()
    {
        // Start the countdown
        StartCoroutine(countDownTimer());
    }

    IEnumerator countDownTimer()
    {
        // Each number will be displayed for 1 second
        numberThree.SetActive(true);
        yield return new WaitForSeconds(1);
        numberThree.SetActive(false);
        numberTwo.SetActive(true);
        yield return new WaitForSeconds(1);
        numberTwo.SetActive(false);
        numberOne.SetActive(true);
        yield return new WaitForSeconds(1);
        numberOne.SetActive(true);
        yield return new WaitForSeconds(1);
        numberOne.SetActive(false);
        mainCount.SetActive(false);
    }
}
