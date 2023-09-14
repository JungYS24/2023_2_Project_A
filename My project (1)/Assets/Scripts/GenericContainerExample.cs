using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericContainerExample : MonoBehaviour
{
    private GenericContainer<int> intContainer;
    private GenericContainer<string> stringContainer;
    // Start is called before the first frame update
    void Start()
    {
        intContainer = new GenericContainer<int>(10);
        stringContainer = new GenericContainer<string>(5);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            intContainer.Add(Random.Range(0, 100));
            DisplayContainerItems(intContainer);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            string randomString = "Item" + Random.Range(0, 100);
            DisplayContainerItems(stringContainer);
        }
    }

    private void DisplayContainerItems<T>(GenericContainer<T>container)
    {
        T[] item = container.GetItem();
        string temp = "";
        for (int i = 0; i < item.Length; i++)
        {
            if(item[i] != null)
            {
                temp += item[i].ToString() + "/";
            }
            else
            {
                temp += "Empty";
            }
        }
        Debug.Log(temp);
    }

    private class GenericContainer<T>
    {
        public GenericContainer(int v)
        {
        }

        internal void Add(int v)
        {
            throw new System.NotImplementedException();
        }

        internal T[] GetItem()
        {
            throw new System.NotImplementedException();
        }
    }
}

