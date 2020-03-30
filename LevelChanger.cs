using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System;
using System.IO;
using System.Text;
using System.Threading;
using Assets.LSL4Unity.Scripts;


public class LevelChanger : MonoBehaviour {
	public Animator animator;
	
	// Use this for initialization
    TcpListener listener;
    String msg;
    // Start is called before the first frame update
	
	private LSLMarkerStream marker;
	private LSLTimeSync t;
	
	public double time_start;
	public double time_end;
	
	
	
	// update variable updateTimeStamp using object.Update(), then return updated time using double fun
    // UpdateTimeStamp
    void Start()
    {
		t = FindObjectOfType<LSLTimeSync>();
		t.Update();
		time_start = t.UpdateTimeStamp;
		print(time_start);
		marker = FindObjectOfType<LSLMarkerStream>();		// stream opens so labrecorder can listen
        
		listener=new TcpListener (IPAddress.Parse("127.0.0.1"), 55001);
        listener.Start ();
        print ("is listening");
		
		
		
		  
		  
		  
    }

    // Update is called once per frame
    void Update()
    {
        if (!listener.Pending ())
        {
			
			marker.Write("start of data rec " + this.name);		// perhaps write at the point of eeg? test
			
        } 
        else 
        {
			
			
            print ("socket comes");
            TcpClient client = listener.AcceptTcpClient ();
            NetworkStream ns = client.GetStream ();
            StreamReader reader = new StreamReader (ns);
            msg = reader.ReadToEnd();
            print (msg);
			FadeToBlack();
			
			
			// UnityEditor.EditorApplication.isPlaying = false;
			marker.Write("end of data rec ");
			t.Update();
			time_end = t.UpdateTimeStamp;
			print(time_end);
        }
    }
	
	
	public void FadeToBlack() {
		animator.SetTrigger("FadeOut");
		
	}
	
}