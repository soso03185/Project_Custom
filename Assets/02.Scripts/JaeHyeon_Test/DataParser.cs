using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataParser : MonoBehaviour
{
    public string fileName = "TestFile";

    void Start()
    {
        TextAsset csvFile = Resources.Load(fileName) as TextAsset;

        if(csvFile != null)
        {
            string fileContent = csvFile.text;

            // csv ���� ���� ��ü ���
            Debug.Log(fileContent);

            string[] lines = fileContent.Split('\n');

            foreach(string line in lines)
            {
                // csv ���� �ؽ�Ʈ �� �پ� ���
                Debug.Log(line);

                string[] values = line.Split(',');

                foreach(string value in values)
                {
                    // �� ���� ���
                    Debug.Log(value);
                }
            }
        }
    }




}
