using System;
using UnityEngine;

public class ExceptionHandling
{
    public ExceptionHandling()
    {
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(Application.persistentDataPath + "/Exception.txt", true))
        {
            System.IO.File.Delete(Application.persistentDataPath + "/Exception.txt");
        }
    }

    public static void ExceptionHandler(Exception e)
    {
        if (e != null)
        {
            Debug.Log(e.Data);
            Debug.Log(e.Message);

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Application.persistentDataPath + "/Exception.txt", true))
            {
                file.WriteLine("Exception :" + e.ToString());
                file.WriteLine("Data :" + e.Data);
                file.WriteLine("Message :" + e.Message);
            }
        }
    }
}
