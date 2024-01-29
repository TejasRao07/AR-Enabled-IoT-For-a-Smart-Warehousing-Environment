using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;


public class Env_monitor : MonoBehaviour
{
	public Text Temp_val ;
	public Text Humi_val ;
	public Text AirQ_val ;
	public string URL ;
	wrapper DataObject = new wrapper() ;
	JSONNode data ;

	int index;
	public GameObject[] Buttons = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WebQuery()) ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WebQuery()
    {
    	UnityWebRequest Request = UnityWebRequest.Get(URL) ;
    	yield return Request.SendWebRequest() ;

    	if(Request.isNetworkError || Request.isHttpError)
    	{
    		UnityEngine.Debug.Log("Network Error") ;
    		yield break ;
    	}

    	DataObject = (wrapper)JsonUtility.FromJson<wrapper>(Request.downloadHandler.text) ;

    	Temp_val.text = DataObject.feed[0].last_value ;
    	Humi_val.text = DataObject.feed[1].last_value ;
    	AirQ_val.text = DataObject.feed[2].last_value ;

    	int tval ; int Hval ; float Aval ;

    	int.TryParse(data[0]["last_value"], out tval) ;
    	int.TryParse(data[1]["last_value"], out Hval) ;
    	float.TryParse(data[2]["last_value"], out Aval) ;

    	if(tval > 40)
    	{
    		orangeButtonColor(0) ;
    	}
    	else if(tval <= 10)
    	{
    		blueButtonColor(0) ;
    	}
    	else
    	{
    		greenButtonColor(0) ;
    	}

    	if(Hval > 80)
    	{
    		orangeButtonColor(1) ;
    	}
    	else if(Hval <= 10)
    	{
    		blueButtonColor(1) ;
    	}
    	else
    	{
    		greenButtonColor(1) ;
    	}

    	if(Aval > 900)
    	{
    		orangeButtonColor(2) ;
    	}
    	else
    	{
    		greenButtonColor(2) ;
    	}

    	yield return new WaitForSeconds(1) ;
        StartCoroutine(WebQuery()) ;

    }


    private void greenButtonColor (int i)
    { 
		UnityEngine.Debug.Log ("Green");
		Color greenColor = new Color (0.00f, 0.94f, 0.12f, 1.0f);
		ColorBlock cb = b.color;
		cb.normalColor = greenColor;
		b.colors = cb;
		EffectColor.GetComponent<LineRenderer> ().material.SetColor ("_TintColor",greenColor);
	}

	private void orangeButtonColor (int i) 
	{
		//Debug.Log ("Orange"); // yellow
		Color redColor = new Color (1.0f, 0.48f, 0.16f, 1.0f);
		Button b = Buttons[i].GetComponent<Button>(); 
		ColorBlock cb = b.colors;
		cb.normalColor = redColor;
		b.colors = cb;
	}

	private void blueButtonColor (int i) 
	{
		//Debug.Log ("Blue");
		Color blueColor = new Color (0.27f, 0.43f, 1.0f, 1.0f);
		Button b = Buttons[i].GetComponent<Button> (); 
		ColorBlock cb = b.colors;
		cb.normalColor = blueColor;
		b.colors = cb;
	}


    [Serializable]
	public class group
	{
    	public int id { get; set; }
    	public string key { get; set; }
    	public string name { get; set; }
    	public string description { get; set; }
    	public string created_at { get; set; }
	}
	
	[Serializable]
	public class Feed
	{
    	public int id { get; set; }
    	public string name {get; set;}
    	public string description { get; set; }
    	public string last_value { get; set; }
    	public string last_value_at { get; set; }
    	public string license { get; set; }
    	public string created_at { get; set; }
    	public string key { get; set; }
    	public group group { get; set; }
	}

	[Serializable]
	public class wrapper{
		public Feed[] feed { get; set; } 
	}

}
