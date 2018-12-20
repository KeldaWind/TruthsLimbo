using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsTest : MonoBehaviour {
    [SerializeField] UnityEvent test1;

    public void Test1()
    {
        Debug.Log("Test1");
    }

    public void Test2()
    {
        Debug.Log("Test2");
    }

    public void Test3()
    {
        Debug.Log("Test3");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            test1.Invoke();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            test1.AddListener(Test1);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            test1.AddListener(Test2);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            test1.AddListener(Test3);

        if (Input.GetKeyDown(KeyCode.Alpha4))
            test1.RemoveListener(Test1);

        if (Input.GetKeyDown(KeyCode.Alpha5))
            test1.RemoveListener(Test2);

        if (Input.GetKeyDown(KeyCode.Alpha6))
            test1.RemoveListener(Test3);
    }

}
