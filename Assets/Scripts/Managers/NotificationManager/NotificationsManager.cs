using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.Notifications.Android;

//Create a empty gameobject for this script
public class NotificationsManager : MonoBehaviour
{
    //public Text DataText;
    public void SendSimpleNotif()
    {
        string notif_title = "Simple Notif";

        string notif_Message = "This is a message from your body. Take care of it";

        DateTime fireTime = DateTime.Now.AddSeconds(10);

        AndroidNotification notif = new AndroidNotification(notif_title, notif_Message, fireTime);

        AndroidNotificationCenter.SendNotification(notif, "default");
    }
    public void SendRepeatingNotif()
    {
        string notif_title = "Repeating Notif";

        string notif_Message = "Where are you? The bacteria are spreading!";

        DateTime fireTime = DateTime.Now.AddSeconds(10);

        //some phones can block frequent notif, so 10 mins is default
        TimeSpan interval = new TimeSpan(0, 10, 0);

        AndroidNotification notif = new AndroidNotification(notif_title, notif_Message, fireTime, interval);

        AndroidNotificationCenter.SendNotification(notif, "repeat");
    }
    /*
    public void SendDataNotif()
    {
        string notif_title = "Data Notif";

        string notif_Message = "We have new info on the bacteria";

        DateTime fireTime = DateTime.Now.AddSeconds(10);

        AndroidNotification notif = new AndroidNotification(notif_title, notif_Message, fireTime);

        notif.IntentData = "We're screwed!!";

        AndroidNotificationCenter.SendNotification(notif, "default");
    }
    */

    /*
    private void CheckIntentData()
    {
        AndroidNotificationIntentData data = AndroidNotificationCenter.GetLastNotificationIntent();

        //If app is opened normally- data should be null
        if (data == null)
        {
            DataText.gameObject.SetActive(false);
        }
        else
        {
            DataText.gameObject.SetActive(true);
            //Gets the string data tied to the notif
            DataText.text = data.Notification.IntentData;
        }
    }
    */

    private void SetupDefaultChannel()
    {
        string channel_id = "default";

        string channel_title = "Default Channel";

        Importance importance = Importance.Default;

        string channel_description = "Default channel for the game";

        AndroidNotificationChannel defaultChannel = new AndroidNotificationChannel(channel_id,
                                                                                   channel_title, 
                                                                                   channel_description, 
                                                                                   importance);

        //pass the channel to android notif center
        AndroidNotificationCenter.RegisterNotificationChannel(defaultChannel);

    }

    private void SetupRepeatingChannel()
    {
        string channel_id = "repeat";

        string channel_title = "Repeating Channel";

        Importance importance = Importance.Default;

        string channel_description = "Repeating channel for the game";

        AndroidNotificationChannel defaultChannel = new AndroidNotificationChannel(channel_id,
                                                                                   channel_title, 
                                                                                   channel_description, 
                                                                                   importance);

        //pass the channel to android notif center
        AndroidNotificationCenter.RegisterNotificationChannel(defaultChannel);

    }

    private void Awake()
    {
        SetupDefaultChannel();
        SetupRepeatingChannel();

        AndroidNotificationCenter.CancelAllNotifications();
    }

    private void Start()
    {
        //CheckIntentData();
    }
}