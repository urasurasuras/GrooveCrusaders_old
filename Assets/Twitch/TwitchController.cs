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

    //VOTES
    public GameObject guio_heal, guio_damage;   //O stands for "Object"
    public Text voteT_heal, voteT_damage;
    //public Text voteT_heal, voteT_damage;

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
        if (GameObject.Find("Heal Votes"))
        {
            guio_heal = GameObject.Find("Heal Votes");
            Text voteT_heal = guio_heal.GetComponent<Text>();
        }
        if (GameObject.Find("Damage Votes"))
        {
            guio_damage = GameObject.Find("Damage Votes");
            Text voteT_damage = guio_damage.GetComponent<Text>();
        }
        //Connect();
    }

    // Update is called once per frame
    void Update()
    {
        if(writer!=null)
        writer.WriteLine("PONG tmi.twitch.tv\r\n");

        if (loggedIn)
        {
            if (twitchClient !=null && !twitchClient.Connected)    //connect if not
            {
                print("Lost connection, reconnecting...");
                Connect();
            }
            ReadChat();
            if (GameObject.Find("Heal Votes") && GameObject.Find("Damage Votes")) {
                print("found");
                voteT_heal = GameObject.Find("Heal Votes").GetComponent<Text>();
                voteT_heal.text = "H: " + GameManager.Instance.vote_heal;

                voteT_damage = GameObject.Find("Damage Votes").GetComponent<Text>();
                voteT_damage.text = "D: " + GameManager.Instance.vote_damage;
            }
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

                if (message.StartsWith("!heal"))
                    GameManager.Instance.vote_heal += 1;
                else if (message.StartsWith("!damage"))
                    GameManager.Instance.vote_damage += 1;
            }
        }
    }
}
