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

            // csv 파일 내용 전체 출력
            Debug.Log(fileContent);

            string[] lines = fileContent.Split('\n');

            foreach(string line in lines)
            {
                // csv 파일 텍스트 한 줄씩 출력
                Debug.Log(line);

                string[] values = line.Split(',');

                foreach(string value in values)
                {
                    // 각 값을 출력
                    Debug.Log(value);
                }
            }
        }
    }




}
