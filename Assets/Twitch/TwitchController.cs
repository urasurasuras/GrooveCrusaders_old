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
    public InputField userName;
    public InputField authkey;

    public Text login_status;
    [SerializeField] bool got_response=false;

    //VOTES
    public GameObject guio_heal, guio_damage;   //O stands for "Object"
    public Text voteT_heal, voteT_damage;
    //public Text voteT_heal, voteT_damage;

    [SerializeField]
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
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            //If connection is refused this caused an IOexception
            
            if (twitchClient != null)
            {
                print("twitchclient not null");
                ReadChat(); //Only this does the readline

                if (!twitchClient.Connected)    //connect if not
                {
                    Connect();
                }

                else if (twitchClient.Connected)
                {
                   
                }                
            }
        }
        catch(IOException e)
        {
            print("failed connection1: " + e);
            got_response = false;
        }
        
    }

    public void Connect()
    {
        username = userName.text;
        channelname = channelName.text;
        password = authkey.text;

        try
        {
            twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);

            reader = new StreamReader(twitchClient.GetStream());
            writer = new StreamWriter(twitchClient.GetStream());

            writer.WriteLine("PASS " + password);
            writer.WriteLine("NICK " + username);
            writer.WriteLine("USER " + username + " 8 * :" + username);
            writer.WriteLine("JOIN #" + channelname);
            writer.Flush();
        }
        catch (IOException e){
            print("Failed connection2: " + e);
            got_response = false;
        }
    }

    private void ReadChat()
    {
        if (twitchClient.Available > 0)
        {
            //got_response = true;
            var message = reader.ReadLine();    //reads line and caches
            print(message);
            if (message.Contains("NOTICE")){

                if (message.Contains("Invalid NICK"))
                {
                    login_status.text = "Invalid nick";      
                }
                else if (message.Contains("Improperly formatted auth"))
                {
                    login_status.text = "Improperly formatted auth key";      
                }
                else if (message.Contains("Login authentication failed"))
                {
                    login_status.text = "Login authentication failed";      
                }
            }
            else if (message.Contains("Welcome, GLHF!"))
            {
                login_status.text = "Logged in as "+username+". \nInvalid channel.";
            }else if (message.Contains("Your host is tmi.twitch.tv"))
            {
                login_status.text = "Logged in as " + username +". \nConnected to "+channelname+"'s chat.";
            }
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

                if (GameManager.Instance != null)
                {
                    if (message.StartsWith("!heal"))
                        GameManager.Instance.vote_heal += 1;
                    else if (message.StartsWith("!damage"))
                        GameManager.Instance.vote_damage += 1;
                }
            }
            else if (message.Contains("PING "))
            {
                if (writer != null)
                    writer.WriteLine("PONG tmi.twitch.tv\r\n");
            }
            if (GameObject.Find("Heal Votes") && GameObject.Find("Damage Votes"))
            {
                voteT_heal = GameObject.Find("Heal Votes").GetComponent<Text>();
                voteT_heal.text = "Healing x" + GameManager.Instance.mult_heal;

                voteT_damage = GameObject.Find("Damage Votes").GetComponent<Text>();
                voteT_damage.text = "Damage x" + GameManager.Instance.mult_damage;
            }
        }
    }
}
