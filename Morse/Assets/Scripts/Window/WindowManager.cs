using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : Singleton<WindowManager>
{
    [HideInInspector] public Transform Parent;

    public Stack<GameObject> OpenWindowStack;

    private void Start()
    {
        Parent = gameObject.transform;

        OpenWindowStack = new Stack<GameObject>();
    }

    private void Update()
    {
        if (OpenWindowStack.Count > 0 && Input.GetKeyUp(KeyCode.Escape))
        {
            GameObject window = OpenWindowStack.Peek();
            Destroy(window);
            OpenWindowStack.Pop();
        }
    }
}
