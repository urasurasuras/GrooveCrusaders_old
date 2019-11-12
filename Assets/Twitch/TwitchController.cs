using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.IO;

public class TwitchController : MonoBehaviour
{
    public static TwitchController Instance;

    private TcpClient twitchClient;
    private StreamReader reader;
    private StreamWriter writer;

    public InputField channelName;
    public InputField authkey;

    public static string username, password, channelname; //https://twitchapps.com/tmi/

    bool loggedIn = false;

    void Awake()
    {
        if (Instance != null)
        {
            GameObject.Destroy(Instance);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        //Connect();
    }

    // Update is called once per frame
    void Update()
    {
        if (loggedIn)
        {
            if (!twitchClient.Connected)    //connect if not
            {
                Connect();
            }
            ReadChat();
        }
    }

    public void setCred()
    {
        username = channelName.text;
        channelname = channelName.text;
        password = authkey.text;

        Connect();
        loggedIn = true;
    }
    void Connect()
    {
        twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);

        reader = new StreamReader(twitchClient.GetStream());
        writer = new StreamWriter(twitchClient.GetStream());

        writer.WriteLine("PASS " + password);
        writer.WriteLine("NICK " + username);
        writer.WriteLine("USER " + username + " 8 * :" + username);
        writer.WriteLine("JOIN #" + channelname);
        writer.Flush();
        //Debug.Log(twitchClient.Connected);
    }

    private void ReadChat()
    {
        if(twitchClient.Available > 0)
        {
            var message = reader.ReadLine();
            //Debug.Log(message);

            if (message.Contains("PRIVMSG"))
            {
                //strip name
                var splitPoint = message.IndexOf("!", 1);
                var chatName = message.Substring(0, splitPoint);
                chatName = chatName.Substring(1);

                //strip message
                splitPoint = message.IndexOf(":", 1);
                message = message.Substring(splitPoint + 1);
                print(String.Format("{0}: {1}", chatName, message));
            }
        }
    }
}
