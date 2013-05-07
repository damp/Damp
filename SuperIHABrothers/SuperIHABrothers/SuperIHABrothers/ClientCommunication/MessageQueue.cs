///////////////////////////////////////////////////////////
//  MessageQueue.cs
//  Implementation of the Class MessageQueue
//  Generated by Enterprise Architect
//  Created on:      04-maj-2013 21:51:55
//  Original author: Filip
///////////////////////////////////////////////////////////


using System;
using System.Collections.Generic;
using System.Threading;
using SuperIHABrothers.ClientCommunication;


public class MessageQueue : IMessageQueueAdd, IMessageQueueRemove
{
    private Queue<string> queueString;
    private static readonly object lockObject = new object();

    public string GetMessage()
    {
        string receivedString;
        lock (lockObject)
        {
            try
            {
                receivedString = queueString.Dequeue();
            }
            catch (Exception e)
            {
                receivedString = null;
            }
        }
        return receivedString;
    }

    /// 
    /// <param name="message"></param>
    public void InsertMessage(string message)
    {
        lock (lockObject)
        {
            queueString.Enqueue(message);
        }
    }

}

//end MessageQueue